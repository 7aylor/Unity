using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour {

    public bool isRight;
    public GameObject bird;
    public int minTimeBetweenSpawn;
    public int maxTimeBetweenSpawn;
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
            GameObject newBird = Instantiate(bird, transform.position + new Vector3(0, Random.Range(-2, 2), 0), Quaternion.identity);

            newBird.GetComponent<Bird>().isRight = isRight;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
	}

    private float GetNewMaxTimeBetweenSpawns()
    {
        return Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }
}