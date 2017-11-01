using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VignetteController : MonoBehaviour {

    private PostProcessingBehaviour behaviour;
    private VignetteModel.Settings vSettings;
    public EndOfLevelMenu winMenu;
    public EndOfLevelMenu loseMenu;
    public AudioClip successSound;
    public AudioClip failureSound;
    public float vignetteStartIntensity;

    private float intensity;
    private float vignetteIntensityChangeRate = 0.01f;
    private float stage1 = 0.25f;
    private float stage2 = 0.35f;
    private float stage3 = 0.75f;
    private float stage4 = 1.5f;
    private float gameOver = 2.5f;
    private Timer timer;
    private bool endMenuSoundPlayed;

    // Use this for initialization
    void Start () {

        endMenuSoundPlayed = false;

        //read more of this https://github.com/Unity-Technologies/PostProcessing/wiki/(v1)-Runtime-post-processing-modification
        behaviour = GetComponent<PostProcessingBehaviour>();
        vSettings = behaviour.profile.vignette.settings;

        vSettings.intensity = vignetteStartIntensity;
        intensity = vSettings.intensity;

        timer = FindObjectOfType<Timer>();

        StartCoroutine("IncreaseVignetteIntensity");
    }
	
	// Update is called once per frame
	void Update () {
        SetVignetteIntensityChangeRate();
    }

    /// <summary>
    /// Updates the intensity of the vignette and applies to the Post Processing Behaviour
    /// </summary>
    /// <param name="newIntensity"></param>
    private IEnumerator IncreaseVignetteIntensity()
    {
        float newIntensity = intensity + vignetteIntensityChangeRate;
        while (vSettings.intensity < newIntensity) // initial is better than new intensity
        {
            vSettings.intensity += (vignetteIntensityChangeRate / 10);
            behaviour.profile.vignette.settings = vSettings;
            intensity = vSettings.intensity;
            newIntensity = intensity + vignetteIntensityChangeRate;
            yield return new WaitForSeconds(0.1f);
        }  
    } 

    /// <summary>
    /// This is triggered when two items are combined
    /// </summary>
    public void CombineItemsEvent()
    {
        if(vSettings.intensity > 0.1)
        {
            vSettings.intensity -= 0.1f;
        }
        else
        {
            vSettings.intensity = 0.05f;
        }
        behaviour.profile.vignette.settings = vSettings;
        intensity = vSettings.intensity;
    }

    /// <summary>
    /// Used to determine the rate of change based off of the intensity of the vignette
    /// </summary>
    private void SetVignetteIntensityChangeRate()
    {
        //win condition
        if(intensity < stage1)
        {
            vignetteIntensityChangeRate = 0f;
            timer.count = false;
            winMenu.UpdateWinMenuTimerText(timer.timeInSeconds);
            winMenu.EnableMenu(true);

            if (endMenuSoundPlayed == false)
            {
                AudioSource.PlayClipAtPoint(successSound, Vector3.zero);
            }

            endMenuSoundPlayed = true;

            //Get current scene name and next scene name to unlock next level
            string currentSceneName = LevelManager.instance.GetCurrentSceneName();
            string nextSceneName = currentSceneName.Substring(0, currentSceneName.Length - 1);
            nextSceneName += (char)(Convert.ToInt32(currentSceneName[currentSceneName.Length - 1]) + 1);

            //Enable the level for level select and check for high score
            PlayerPrefs.SetString(nextSceneName + "_Enabled", "True");

            if(PlayerPrefs.GetInt(currentSceneName + "_BestTime") != 0 &&
               PlayerPrefs.GetInt(currentSceneName + "_BestTime") > timer.timeInSeconds)
            {
                //enable record menu
                foreach(Transform obj in winMenu.transform)
                {
                    if (obj.gameObject.name == "Record Time")
                    {
                        obj.GetComponent<Text>().enabled = true;
                    }
                }

                //set high score
                PlayerPrefs.SetInt(currentSceneName + "_BestTime", timer.timeInSeconds);
            }
        }
        else if(intensity >= stage1 && intensity < stage2)
        {
            vignetteIntensityChangeRate = 0.005f; //0.00075f
        }
        else if(intensity >= stage2 && intensity < stage3)
        {
            vignetteIntensityChangeRate = 0.0075f; //0.001f
        }
        else if (intensity >= stage3 && intensity < stage4)
        {
            vignetteIntensityChangeRate = 0.01f; //0.005f
        }
        else if (intensity >= stage4 && intensity < gameOver)
        {
            vignetteIntensityChangeRate = 0.05f; //0.01f
        }
        else
        {
            vignetteIntensityChangeRate = 50f;
            timer.count = false;
            loseMenu.EnableMenu(true);

            if (endMenuSoundPlayed == false)
            {
                AudioSource.PlayClipAtPoint(failureSound, Vector3.zero);
            }

            endMenuSoundPlayed = true;
        }
    }

    /// <summary>
    /// Pauses the currently running Coroutine
    /// </summary>
    public void PauseVignetteIntensity()
    {
        Debug.Log("Paused CoRoutine");
        StopCoroutine("IncreaseVignetteIntensity");
    }

    /// <summary>
    /// Resumes the coroutine
    /// </summary>
    public void ResumeVignetteIntensity()
    {
        Debug.Log("Resumed CoRoutine");
        StartCoroutine("IncreaseVignetteIntensity");
    }

    /// <summary>
    /// Getter to see if player has beaten this level
    /// </summary>
    /// <returns>bool hasWon</returns>
    public bool HasWon()
    {
        return intensity < stage1;
    }

    /// <summary>
    /// Getter to see if the player has lost this level
    /// </summary>
    /// <returns>bool hasLost</returns>
    public bool HasLost()
    {
        return intensity >= gameOver;
    }
}
