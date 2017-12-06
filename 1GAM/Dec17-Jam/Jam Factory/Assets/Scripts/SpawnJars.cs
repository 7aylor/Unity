using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJars : MonoBehaviour {

    public GameObject jar;
    public bool CanSpawnJars { get; set; }

    private float timeSinceLastSpawn = 0;

	// Use this for initialization
	void Start () {
        CanSpawnJars = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(CanSpawnJars == true)
        {
            HandleSpawnJars();
        }
	}

    private void HandleSpawnJars()
    {
        if(timeSinceLastSpawn >= GameManager.instance.GetSpawnSpeed())
        {
            timeSinceLastSpawn = 0;
            Instantiate(jar, transform, false);
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }
}
