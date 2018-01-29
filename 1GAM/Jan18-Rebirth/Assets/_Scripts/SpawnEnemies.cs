using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public GameObject enemy;
    public bool CanSpawnEnemies { get; set; }
    private int numEnemies = 0;
    private int maxEnemies = 5;
    private float timeSinceLastSpawn = 0;
    private float timeToSpawn = 5f;

	// Use this for initialization
	void Start () {
        CanSpawnEnemies = false;
	}

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(CanSpawnEnemies == true && numEnemies < maxEnemies && timeSinceLastSpawn >= timeToSpawn)
        {
            SpawnEnemy();
            numEnemies++;
            timeSinceLastSpawn = 0;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemy, RandomLocation(), Quaternion.identity);
    }

    private Vector2 RandomLocation()
    {
        float x = Random.Range(-18f, 18f);

        while(x < 10 && x > -10)
        {
            x = Random.Range(-18f, 18f);
        }

        float y = Random.Range(-18f, 18f);

        while (y < 10 && y > -10)
        {
            y = Random.Range(-18f, 18f);
        }

        return new Vector2(x, y);

    }

    public void ResetNumEnemies()
    {
        numEnemies = 0;
    }
}
