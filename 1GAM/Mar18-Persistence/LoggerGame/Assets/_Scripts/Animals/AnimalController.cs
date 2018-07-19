using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour {

    public GameObject bear;
    public GameObject rabbit;
    public GameObject turtle;

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
        int numRabbits = Random.Range(0, 1 + GameManager.instance.numGrassTiles / 6);

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

    public void SpawnTurtle()
    {
        //only spawn turtle half the time
        if (Random.Range(0, 1f) < 0.5f)
        {
            //find river end
            Vector3 riverEndPos = GameObject.Find("River End(Clone)").transform.position;

            GameObject t = Instantiate(turtle, riverEndPos, Quaternion.identity);
            t.transform.parent = FindObjectOfType<AnimalController>().transform;
        }
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
