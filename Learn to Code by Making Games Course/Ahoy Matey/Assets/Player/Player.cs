using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    private Vector3 inputValue;
    private Camera camera;

    // Update is called once per frame
    void Update () {
        if (isLocalPlayer)
        {
            inputValue.x = CrossPlatformInputManager.GetAxis("Horizontal");
            inputValue.y = 0f;
            inputValue.z = CrossPlatformInputManager.GetAxis("Vertical");

            transform.Translate(inputValue);
        }
    }

    public override void OnStartLocalPlayer()
    {

        camera = GetComponentInChildren<Camera>();

        if (camera != null)
        {
            camera.enabled = true;
        }
    }
}