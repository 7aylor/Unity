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
    public float vignetteStartIntensity;

    private float intensity;
    private float vignetteIntensityChangeRate = 0.01f;
    private float stage1 = 0.1f;
    private float stage2 = 0.35f;
    private float stage3 = 0.75f;
    private float stage4 = 1.5f;
    private float gameOver = 2.5f;
    private Timer timer;

    // Use this for initialization
    void Start () {
        //read more of this https://github.com/Unity-Technologies/PostProcessing/wiki/(v1)-Runtime-post-processing-modification
        behaviour = GetComponent<PostProcessingBehaviour>();
        vSettings = behaviour.profile.vignette.settings;

        vSettings.intensity = vignetteStartIntensity;
        intensity = vSettings.intensity;

        timer = FindObjectOfType<Timer>();
    }
	
	// Update is called once per frame
	void Update () {
        SetVignetteIntensityChangeRate();
        StartCoroutine("IncreaseVignetteIntensity", intensity + vignetteIntensityChangeRate);
    }

    /// <summary>
    /// Updates the intensity of the vignette and applies to the Post Processing Behaviour
    /// </summary>
    /// <param name="newIntensity"></param>
    private IEnumerator IncreaseVignetteIntensity(float newIntensity)
    {
        while(vSettings.intensity < newIntensity) // initial is better than new intensity
        {
            vSettings.intensity += (vignetteIntensityChangeRate / 10);
            behaviour.profile.vignette.settings = vSettings;
            intensity = vSettings.intensity;
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

            string currentSceneName = LevelManager.instance.GetCurrentSceneName();

            //Enable the level for level select and check for high score
            PlayerPrefs.SetString(currentSceneName + "_Enabled", "True");

            if(PlayerPrefs.GetInt(currentSceneName + "_HighScore") != 0 &&
               PlayerPrefs.GetInt(currentSceneName + "_HighScore") > timer.timeInSeconds)
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
                PlayerPrefs.SetInt(currentSceneName + "_HighScore", timer.timeInSeconds);
            }
        }
        else if(intensity >= stage1 && intensity < stage2)
        {
            vignetteIntensityChangeRate = 0.00075f;
        }
        else if(intensity >= stage2 && intensity < stage3)
        {
            vignetteIntensityChangeRate = 0.001f;
        }
        else if (intensity >= stage3 && intensity < stage4)
        {
            vignetteIntensityChangeRate = 0.005f;
        }
        else if (intensity >= stage4 && intensity < gameOver)
        {
            vignetteIntensityChangeRate = 0.01f;
        }
        else
        {
            vignetteIntensityChangeRate = 5f;
            timer.count = false;
            loseMenu.EnableMenu(true);
        }
    }

}
