using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public enum level { Title, Overworld, Hills, Desert, Swamp, Mountains }

    public static LevelManager instance = null;

    private void Awake()
    {
        Input.backButtonLeavesApp = true;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Debug.Log(gameObject.name + " Destroyed on Load");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Load scene based off of the scene's build index
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Load scene based off of the scene's name
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        GameManager.instance.currentLevel = GetCurrentLevel();
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Gets the current level that is loaded based on the level enum
    /// </summary>
    /// <returns></returns>
    public level GetCurrentLevel()
    {
        return (level)SceneManager.GetActiveScene().buildIndex;
    }

    public void Exit()
    {
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        //    activity.Call<bool>("moveTaskToBack", true);
        //}
        //else
        //{
        //    Application.Quit();
        //}
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        #endif
    }
}
