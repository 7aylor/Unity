using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawnManager : MonoBehaviour {

    [Range(1, 4)]
    public float spawnSpeed;
    public GameObject roundCompleteMenu;
    private float timeSinceLastSpawn;
    private bool isEndOfRound = false;

    public int activeSpawners { get; set; }


	// Use this for initialization
	void Start () {
        timeSinceLastSpawn = 0;
        activeSpawners = 3;
	}
	
	// Update is called once per frame
	void Update () {
        if(activeSpawners <= 0 && isEndOfRound == false)
        {
            isEndOfRound = true;
            Time.timeScale = 0;
            roundCompleteMenu.SetActive(true);
            roundCompleteMenu.GetComponent<PlayRoundSong>().StartFullSong();
        }

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
            if (child.gameObject.activeSelf == true && child.GetComponent<SpawnKeys>().CanSpawnKeys == true)
            {
                actives.Add(child);
            }
        }

        if(actives.Count > 0)
        {
            actives[Random.Range(0, actives.Count)].GetComponent<SpawnKeys>().Spawn();
        }
        else
        {
            Time.timeScale = 0;
            roundCompleteMenu.SetActive(true);
            roundCompleteMenu.GetComponent<PlayRoundSong>().StartFullSong();
        }
        
    }
}