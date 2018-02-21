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
        Dictionary<int, GameObject> activeSpawners = new Dictionary<int, GameObject>();

        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject spawner = transform.GetChild(i).gameObject;
            if (spawner.activeInHierarchy)
            {
                activeSpawners.Add(i, spawner);
            }
        }


        ///This needs to be fixed
        activeSpawners[Random.Range(0, activeSpawners.Count)].GetComponent<SpawnKeys>().Spawn();

        Debug.Log(activeSpawners[Random.Range(0, activeSpawners.Count)]);

    }
}