using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

    public bool recording = true;

    private void Start()
    {
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
    }

    public void SetRecordingStatus(bool isEnabled)
    {
        recording = isEnabled;
    }
}
