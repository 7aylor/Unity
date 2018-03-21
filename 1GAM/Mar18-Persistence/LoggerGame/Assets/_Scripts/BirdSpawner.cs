using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour {

    public bool isRight;
    public GameObject bird;
    private float maxTimeBetweenSpawns;
    private float timeSinceLastSpawn;

	// Use this for initialization
	void Start () {
        timeSinceLastSpawn = 0;
        maxTimeBetweenSpawns = GetNewMaxTimeBetweenSpawns();
	}
	
	// Update is called once per frame
	void Update () {
		if(timeSinceLastSpawn >= maxTimeBetweenSpawns)
        {
            timeSinceLastSpawn = 0;
            GameObject newBird = Instantiate(bird, transform.position, Quaternion.identity);
            newBird.GetComponent<Bird>().isRight = isRight;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
	}

    private float GetNewMaxTimeBetweenSpawns()
    {
        return Random.Range(1, 3f);
    }
}
