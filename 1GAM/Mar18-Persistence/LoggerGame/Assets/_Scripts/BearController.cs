using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour {

    public GameObject bear;

    private int xPos = 0;
    private int yPos = 0;
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
        PickTreeTile();

        float spawnX = (float)xPos - GameManager.instance.sizeX / 2;
        float spawnY = (float)yPos - GameManager.instance.sizeY / 2 + 0.8f;

        //spawn the bear
        ourBear = Instantiate(bear, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
        ourBear.transform.parent = transform;
        bearSprite = ourBear.GetComponent<SpriteRenderer>();
        bearSprite.enabled = false;
    }

    private void PickTreeTile()
    {
        do
        {
            xPos = Random.Range(0, GameManager.instance.sizeX);
            yPos = Random.Range(0, GameManager.instance.sizeY);
            Debug.Log("xPos: " + xPos + " yPos: " + yPos + " Tile Type: " + GameManager.instance.map[xPos, yPos]);
        } while (GameManager.instance.map[xPos, yPos] != (int)MapGenerator.tileType.tree);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");

        if (collision.GetComponent<Player>() != null)
        {
            Debug.Log("Bear collides with Player");
            bearSprite.enabled = true;
        }
    }
}
