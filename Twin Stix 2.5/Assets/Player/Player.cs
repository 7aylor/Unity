using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
        {
            print("w pressed");
        }
        if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
        {
            print("s pressed");
        }
        if (CrossPlatformInputManager.GetAxis("Horizontal") > 0)
        {
            print("d pressed");
        }
        if (CrossPlatformInputManager.GetAxis("Horizontal") < 0)
        {
            print("a pressed");
        }
    }
}
