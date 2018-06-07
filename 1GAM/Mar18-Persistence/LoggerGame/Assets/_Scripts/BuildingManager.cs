using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public GameObject[] houses;
    private int houseCount;
    private Vector2Int startPos; //holds the first house start position in array coords
    private Vector2Int lastHousePos; //holds the position of the last house spawned
    private Transform housesParent; //houses container gameobject
    private List<Vector2> mapSides; //holds the possible sides

    private void Awake()
    {
        housesParent = GameObject.FindGameObjectWithTag("Houses").transform;
    }

    private void Start()
    {
        mapSides = new List<Vector2> { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
        startPos = new Vector2Int(0, 0);
        lastHousePos = startPos;
        houseCount = 0;

        //temporarily call here
        FindSuitableSpawnPos();
    }

    /// <summary>
    /// Called from MarketManager to spawn a house
    /// </summary>
    public void SpawnHouseFromMarket()
    {
        //find a suitable position on the edge of the world
    }

    /// <summary>
    /// 
    /// </summary>
    private void FindSuitableSpawnPos()
    {
        //pick random edge then spawn house in grass position
        if(houseCount < 1)
        {
            Vector2 side;

            do
            {
                //chooses the random side
                side = mapSides[Random.Range(0, mapSides.Count)];

                if (side == Vector2.left)
                {
                    startPos.x = 0;
                    startPos.y = Random.Range(0, GameManager.instance.sizeY);
                }
                else if (side == Vector2.right)
                {
                    startPos.x = GameManager.instance.sizeX - 1;
                    startPos.y = Random.Range(0, GameManager.instance.sizeY);
                }
                else if (side == Vector2.up)
                {
                    startPos.x = Random.Range(0, GameManager.instance.sizeX);
                    startPos.y = GameManager.instance.sizeY - 1;
                }
                //down
                else
                {
                    startPos.x = Random.Range(0, GameManager.instance.sizeX);
                    startPos.y = 0;
                }
            } while (GameManager.instance.map[startPos.x, startPos.y] != (int)MapGenerator.tileType.grass);

            SpawnHouse();

            //update the lastHousePos to the current position
            lastHousePos = startPos;

        }
        else
        {

        }
    }

    private void SpawnHouse()
    {
        //update the map
        GameManager.instance.map[startPos.x, startPos.y] = (int)MapGenerator.tileType.house;

        GameObject house = Instantiate(houses[0].gameObject, new Vector3(GameManager.instance.ArrayCoordToWorldCoordX(startPos.x),
            GameManager.instance.ArrayCoordToWorldCoordY(startPos.y), 0), Quaternion.identity);
        house.transform.parent = housesParent;

        //delete the grass tile
        RaycastHit2D ray = Physics2D.Raycast(house.transform.position, Vector2.zero);


        if (ray.collider.gameObject.tag == "Grass")
        {
            Destroy(ray.collider.gameObject);
        }
    }

    private void PickRandomSide()
    {

    }
}
