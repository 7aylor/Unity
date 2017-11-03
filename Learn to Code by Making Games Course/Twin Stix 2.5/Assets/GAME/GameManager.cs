using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

    public bool recording = true;
    bool isPaused = false;
    float fixedDeltaTime;

    private void Start()
    {
        PlayerPrefsManager.UnlockLevel(2);
        Debug.Log(PlayerPrefsManager.IsLevelUnlocked(1));
        Debug.Log(PlayerPrefsManager.IsLevelUnlocked(2));

        fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update () {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            recording = false;
        }
        else
        {
            recording = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
        print("Update");
    }

    public void SetRecordingStatus(bool isEnabled)
    {
        recording = isEnabled;
    }

    void PauseGame()
    {
        if(isPaused == false)
        {
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = fixedDeltaTime;
            Time.fixedDeltaTime = 1;
            isPaused = false;
        }
        
    }

    private void OnApplicationPause(bool pause)
    {
        
    }
}
