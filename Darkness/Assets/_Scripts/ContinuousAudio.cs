using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousAudio : MonoBehaviour {

    public static ContinuousAudio instance;

	// Use this for initialization
	void Start () {
		if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (LevelManager.instance.GetCurrentSceneName() == "MainMenu" ||
                                LevelManager.instance.GetCurrentSceneName() == "LevelSelect")
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Destroyed1");
            Destroy(instance);
        }
	}
}
