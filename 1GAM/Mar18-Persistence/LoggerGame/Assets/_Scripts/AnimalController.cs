using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour {

    public GameObject bear;
    public GameObject rabbit;

    private int xPos = 0;
    private int yPos = 0;
    private float spawnX = 0;
    private float spawnY = 0;
    private GameObject ourBear;
    private SpriteRenderer bearSprite;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnBear()
    {
        //pick random tree tile
        PickRespectiveTile((int)MapGenerator.tileType.tree);

        //spawn the bear
        ourBear = Instantiate(bear, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
        ourBear.transform.parent = transform;
        bearSprite = ourBear.GetComponent<SpriteRenderer>();
        bearSprite.enabled = false;
    }

    public void SpawnRabbit()
    {
        Debug.Log("Spawn Rabbit Called");

        int numRabbits = Random.Range(1, GameManager.instance.numGrassTiles / 4 + 1);

        for(int i = 0; i < numRabbits; i++)
        {
            PickRespectiveTile((int)MapGenerator.tileType.grass);
            GameObject ourRabbit = Instantiate(rabbit, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
            ourRabbit.transform.parent = transform;
        }
    }

    public void MoveBear()
    {
        PickRespectiveTile((int)MapGenerator.tileType.tree);
        ourBear.transform.position = new Vector3(spawnX, spawnY, 0);
    }

    private void PickRespectiveTile(int tileType)
    {
        do
        {
            xPos = Random.Range(0, GameManager.instance.sizeX);
            yPos = Random.Range(0, GameManager.instance.sizeY);
        } while (GameManager.instance.map[xPos, yPos] != tileType);

        spawnX = GameManager.instance.ArrayCoordToWorldCoordX(xPos);
        spawnY = GameManager.instance.ArrayCoordToWorldCoordY(yPos);
    }
}
