using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawnManager : MonoBehaviour {

    [Range(1, 4)]
    public float spawnSpeed;
    private float timeSinceLastSpawn;

	// Use this for initialization
	void Start () {
        timeSinceLastSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeSinceLastSpawn >= spawnSpeed)
        {
            transform.GetChild(PickRandomSpawner()).GetComponent<SpawnKeys>().Spawn();
            timeSinceLastSpawn = 0;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }

    private int PickRandomSpawner()
    {
        return Random.Range(0, transform.childCount);
    }
}
