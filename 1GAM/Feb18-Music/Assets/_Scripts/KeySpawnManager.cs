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
            //transform.GetChild(PickRandomSpawner()).GetComponent<SpawnKeys>().Spawn();
            SpawnFromRandomSpawner();
            timeSinceLastSpawn = 0;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }

    private void SpawnFromRandomSpawner()
    {
        List<Transform> actives = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                actives.Add(child);
            }
        }

        actives[Random.Range(0, actives.Count)].GetComponent<SpawnKeys>().Spawn();
    }
}