using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    /// <summary>
    /// Using the Singleton Pattern to always only have one LevelManager
    /// </summary>
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        InitPlayerPrefs();
    }

    private static void InitPlayerPrefs()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            if (sceneName.Contains("Level_"))
            {
                if (!sceneName.Contains("1") && PlayerPrefs.HasKey(sceneName + "_Enabled") == false)
                {
                    PlayerPrefs.SetString(sceneName + "_Enabled", "False");
                }
                if (PlayerPrefs.HasKey(sceneName + "_BestTime") == false)
                {
                    PlayerPrefs.SetInt(sceneName + "_BestTime", Int32.MaxValue);
                }
            }
        }
    }

    /// <summary>
    /// Loads the scene based on the name of the scene
    /// </summary>
    /// <param name="levelName"></param>
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// Loads the scene based on the scene index number
    /// </summary>
    /// <param name="levelIndex"></param>
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Quits the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Allows outside access to get the current scene index
    /// </summary>
    /// <returns>index of the current scene in the build index</returns>
    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>name of the current scene</returns>
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

}
