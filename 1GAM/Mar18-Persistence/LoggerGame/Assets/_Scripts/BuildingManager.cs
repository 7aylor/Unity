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
    private Vector2 side;
    [SerializeField]private List<Vector2Int> housePositions; //all house positions on the map in array coords

    private void Awake()
    {
        housesParent = GameObject.FindGameObjectWithTag("Houses").transform;
    }

    private void Start()
    {
        mapSides = new List<Vector2> { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
        housePositions = new List<Vector2Int>();
        startPos = new Vector2Int(0, 0);
        lastHousePos = startPos;
        houseCount = 0;

        //temporarily call here
        FindSuitableSpawnPos();
        Invoke("FindSuitableSpawnPos", 1);
        Invoke("FindSuitableSpawnPos", 1);
        Invoke("FindSuitableSpawnPos", 1);
        Invoke("FindSuitableSpawnPos", 1);
    }

    /// <summary>
    /// Called from MarketManager to spawn a house
    /// </summary>
    public void SpawnHouseFromMarket()
    {
        //find a suitable position on the edge of the world
        FindSuitableSpawnPos();
    }

    /// <summary>
    /// 
    /// </summary>
    private void FindSuitableSpawnPos()
    {
        startPos = lastHousePos;
        //pick random edge then spawn house in grass position
        if(houseCount < 1)
        {
            do
            {
                PickRandomSideTile();
            } while (GameManager.instance.map[startPos.x, startPos.y] != (int)MapGenerator.tileType.grass);

            SpawnHouse();
        }
        else
        {
            if (side == Vector2.left)
            {
                //find nearest grass tile and spawn
                int distanceFromStartTile = 1;
                bool goUp = true;

                for (int x = startPos.x; x < GameManager.instance.sizeX; x++)
                {
                    for (int y = startPos.y; y < GameManager.instance.sizeY && y > 0; y += distanceFromStartTile)
                    {

                        if(y + distanceFromStartTile == GameManager.instance.sizeY)
                        {
                            distanceFromStartTile = startPos.y - 1;
                        }
                        else if(y + distanceFromStartTile == 0)
                        {
                            distanceFromStartTile = startPos.y + 1;
                        }
                        //else
                        //{
                        //    if (goUp)
                        //    {
                        //        distanceFromStartTile++;
                        //    }
                        //    else
                        //    {
                        //        distanceFromStartTile--;
                        //    }
                        //}

                        if (GameManager.instance.map[startPos.x, y] == (int)MapGenerator.tileType.grass)
                        {
                            startPos.y = y;
                            break;
                        }
                    }
                }

                SpawnHouse();
            }
            else if (side == Vector2.left)
            {
                //find nearest grass tile and spawn
                int distanceFromStartTile = 0;
                bool goUp = true;

                for (int x = startPos.x; x > 0; x--)
                {
                    for (int y = startPos.y; y < GameManager.instance.sizeY && y > 0; y += distanceFromStartTile)
                    {
                        if (y + distanceFromStartTile == GameManager.instance.sizeY)
                        {
                            distanceFromStartTile = startPos.y - 1;
                        }
                        else if (y + distanceFromStartTile == 0)
                        {
                            distanceFromStartTile = startPos.y + 1;
                        }
                        //else
                        //{
                        //    if (goUp)
                        //    {
                        //        distanceFromStartTile++;
                        //    }
                        //    else
                        //    {
                        //        distanceFromStartTile--;
                        //    }
                        //}

                        if (GameManager.instance.map[startPos.x, y] == (int)MapGenerator.tileType.grass)
                        {
                            startPos.y = y;
                            break;
                        }
                    }
                }

                SpawnHouse();
            }
            else if (side == Vector2.up || side == Vector2.down)
            {
                startPos.x += 1;
                SpawnHouse();
            }
        }
    }

    /// <summary>
    /// Spawns house in the new position and deletes grass tile
    /// </summary>
    private void SpawnHouse()
    {
        //update the map
        GameManager.instance.map[startPos.x, startPos.y] = (int)MapGenerator.tileType.house;
        housePositions.Add(startPos);

        //spawn the house and update its parent
        GameObject house = Instantiate(houses[0].gameObject, new Vector3(GameManager.instance.ArrayCoordToWorldCoordX(startPos.x),
            GameManager.instance.ArrayCoordToWorldCoordY(startPos.y), 0), Quaternion.identity);
        house.transform.parent = housesParent;

        //delete the grass tile
        RaycastHit2D ray = Physics2D.Raycast(house.transform.position, Vector2.zero);

        if (ray.collider.gameObject.tag == "Grass")
        {
            Destroy(ray.collider.gameObject);
        }

        houseCount++;
        lastHousePos = startPos;
    }

    /// <summary>
    /// picks a random side and picks a random tile on that side
    /// </summary>
    private void PickRandomSideTile()
    {
        side = mapSides[Random.Range(0, mapSides.Count)];

        //pick left side of the map
        if (side == Vector2.left)
        {
            startPos.x = 0;
            startPos.y = Random.Range(0, GameManager.instance.sizeY);
        }
        //pick right side of the map
        else if (side == Vector2.right)
        {
            startPos.x = GameManager.instance.sizeX - 1;
            startPos.y = Random.Range(0, GameManager.instance.sizeY);
        }
        //pick top of the map
        else if (side == Vector2.up)
        {
            startPos.x = Random.Range(0, GameManager.instance.sizeX);
            startPos.y = GameManager.instance.sizeY - 1;
        }
        //pick bottom of the map
        else
        {
            startPos.x = Random.Range(0, GameManager.instance.sizeX);
            startPos.y = 0;
        }
    }
}
