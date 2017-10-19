using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyHUD : MonoBehaviour {

    private NetworkManager networkManager;

	// Use this for initialization
	void Start () {
        networkManager = GetComponent<NetworkManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MyStartHost()
    {
        networkManager.StartHost();
        Debug.Log("Starting Host at " + System.DateTime.Now);
    }

    void OnStartHost()
    {
        Debug.Log("Host started at " + System.DateTime.Now);
    }
}
