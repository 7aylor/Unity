using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {


    //TODO: Fix bug with completely straight river
    //TODO: Fix out of bounds errors

    public GameObject[] buildings;
    private int buildingCount;
    private Vector2Int lastBuildingPos; //holds the position of the last building spawned
    private enum lastNeighborChange { greater, lesser }
    lastNeighborChange lastNeighbor;

    private Transform buildingsParent; //buildings container gameobject
    private List<Vector2> mapSides; //holds the possible sides of the map
    private Vector2 side; //holds the side we spawned on
    private Vector2Int greaterNeighbor; //indicate either neighbor above, or neighbor to the right
    private Vector2Int lesserNeighbor;  //indicate either neighbor below, or neighbor to the left
    private int spawnRounds = 0;

    private LinkedList<Vector2Int> buildingPositions;
    [SerializeField]
    private SpawnDirection spawnDirection;

    private void Awake()
    {
        buildingsParent = GameObject.FindGameObjectWithTag("Buildings").transform;
    }

    private void Start()
    {
        mapSides = new List<Vector2> { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
        buildingPositions = new LinkedList<Vector2Int>();
        lastBuildingPos = new Vector2Int(0, 0);
        greaterNeighbor = lastBuildingPos;
        lesserNeighbor = lastBuildingPos;
        buildingCount = 0;
        spawnDirection = new SpawnDirection(true, true, true, true);
        side = mapSides[Random.Range(0, mapSides.Count)];
    }

    /// <summary>
    /// Using as testing for now. Press Space to spawn a building
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Build();
        }
    }

    /// <summary>
    /// Called from MarketManager to spawn a house
    /// </summary>
    public void BuildFromMarket()
    {
        //find a suitable position on the edge of the world
        Build();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Build()
    { 
        //pick random edge then spawn first building in grass position
        if(buildingCount < 1)
        {
            do
            {
                PickRandomSideTile();
            } while (GameManager.instance.map[lastBuildingPos.x, lastBuildingPos.y] != (int)MapGenerator.tileType.grass);

            //lastBuildingPos.x = 5;
            //lastBuildingPos.y = 0;

            //greaterNeighbor = lastBuildingPos;
            //lesserNeighbor = lastBuildingPos;
            //////PickRandomSideTile();
            //side = Vector2.down;

            SpawnBuilding();
        }
        else if(buildingCount < 150)
        {
            if (side == Vector2.left)
            {
                //1. try spawning up
                //   --> while we hit a river, try spawning up one more
                //      -->if we hit a try, trigger falling animation, then destroy tree and spawn
                //      -->if we hit rocks/obstacles, destroy and spawn
                //      -->if we hit the top edge, set flag and try spawning below
                //   --> set 
                //2. try spawning down
                //   --> while we hit a river, try spawning up one more
                //      -->if we hit the top edge, set flag and try spawning below


                VerticalSpawns(false);

            }
            else if (side == Vector2.right)
            {
                VerticalSpawns(true);
            }
            //TODO: Horizontal building spawns
            else if (side == Vector2.up)
            {
                HorizontalSpawns(false);
            }
            else if(side == Vector2.down)
            {
                HorizontalSpawns(true);
            }
        }
    }

    /// <summary>
    /// Used to Spawn buildings vertically. Checks to see if we have chosen a river tile or have gone out of bounds
    /// </summary>
    /// <param name="startOnRight">true if we start on the right, false if we start on the left</param>
    private void VerticalSpawns(bool startOnRight)
    {
        //spawn up
        if (spawnDirection.canSpawnUp)
        {
            //y is a temporary var
            int y = greaterNeighbor.y;

            //loops through tiles going up to verify it isn't a river and it isn't out of bounds
            do
            {
                y++;
            } while (y >= 0 && y < GameManager.instance.sizeY && TileNotRiver(lastBuildingPos.x, y) == false);

            //if it is in bounds, spawn it and update relevant vars
            if (y <= GameManager.instance.sizeY - 1)
            {
                lastBuildingPos.y = y;
                greaterNeighbor = lastBuildingPos;
                SpawnBuilding();
            }
            //else, we can't spawn up anymore
            else
            {
                spawnDirection.canSpawnUp = false;
            }

            //exit method
            return;
        }
        //spawn down
        else if (spawnDirection.canSpawnDown)
        {
            //y is a temporary var
            int y = lesserNeighbor.y;

            //loops through tiles going up to verify it isn't a river and it isn't out of bounds
            do
            {
                y--;
            } while (y >= 0 && y < GameManager.instance.sizeY && TileNotRiver(lastBuildingPos.x, y) == false);

            //if it is in bounds, spawn it and update relevant vars
            if (y >= 0)
            {
                lastBuildingPos.y = y;
                lesserNeighbor = lastBuildingPos;
                SpawnBuilding();
            }
            //else, we can't spawn down anymore
            else
            {
                spawnDirection.canSpawnDown = false;
            }

            //exit method
            return;
        }


        //if we have made it this far, we can't go up or down. 
        //TODO: might need to tweak this
        spawnRounds++;
        
        if(startOnRight == true)
        {
            lastBuildingPos.x -= 1;
        }
        else
        {
            lastBuildingPos.x += 1;
        }

        PickRandomSideTile(lastBuildingPos.x);

        //reset the values and spawn a tile in the new position
        greaterNeighbor = lastBuildingPos;
        lesserNeighbor = lastBuildingPos;
        spawnDirection.canSpawnUp = true;
        spawnDirection.canSpawnDown = true;
        SpawnBuilding();
    }

    /// <summary>
    /// Used to Spawn buildings horizontally. Checks to see if we have chosen a river tile or have gone out of bounds
    /// </summary>
    /// <param name="startOnBot">true if we start on the bottom edge, false if we start on the top</param>
    private void HorizontalSpawns(bool startOnBot)
    {
        //spawn right
        if (spawnDirection.canSpawnRight)
        {
            //x is a temporary var
            int x = greaterNeighbor.x;

            //loops through tiles going up to verify it isn't a river and it isn't out of bounds
            do
            {
                x++;
            } while (x >= 0 && x < GameManager.instance.sizeX && TileNotRiver(x, lastBuildingPos.y) == false);

            //if it is in bounds, spawn it and update relevant vars
            if (x <= GameManager.instance.sizeX - 1)
            {
                lastBuildingPos.x = x;
                greaterNeighbor = lastBuildingPos;
                SpawnBuilding();
            }
            //else, we can't spawn up anymore
            else
            {
                spawnDirection.canSpawnRight = false;
            }

            //exit method
            return;
        }
        //spawn down
        else if (spawnDirection.canSpawnLeft)
        {
            //y is a temporary var
            int x = lesserNeighbor.x;

            //loops through tiles going up to verify it isn't a river and it isn't out of bounds
            do
            {
                x--;
            } while (x >= 0 && x < GameManager.instance.sizeX && TileNotRiver(x, lastBuildingPos.y) == false);

            //if it is in bounds, spawn it and update relevant vars
            if (x >= 0)
            {
                lastBuildingPos.x = x;
                lesserNeighbor = lastBuildingPos;
                SpawnBuilding();
            }
            //else, we can't spawn down anymore
            else
            {
                spawnDirection.canSpawnLeft = false;
            }

            //exit method
            return;
        }

        //if we have made it this far, we can't go up or down. 
        //TODO: might need to tweak this
        spawnRounds++;

        if (startOnBot == true)
        {
            lastBuildingPos.y += 1;
        }
        else
        {
            lastBuildingPos.y -= 1;
        }

        PickRandomSideTile(lastBuildingPos.y);

        //reset the values and spawn a tile in the new position
        greaterNeighbor = lastBuildingPos;
        lesserNeighbor = lastBuildingPos;
        spawnDirection.canSpawnLeft = true;
        spawnDirection.canSpawnRight = true;
        SpawnBuilding();
    }


    /// <summary>
    /// Returns if the given tile coordinate is a river tile or not
    /// </summary>
    /// <param name="x">x position in map array</param>
    /// <param name="y">y position in map array</param>
    /// <returns></returns>
    private bool TileNotRiver(int x, int y)
    {
        Debug.Log("x: " + " y: " + y);

        return (GameManager.instance.map[x, y] != (int)MapGenerator.tileType.startRiver &&
                GameManager.instance.map[x, y] != (int)MapGenerator.tileType.straightRiver &&
                GameManager.instance.map[x, y] != (int)MapGenerator.tileType.curveRiver &&
                GameManager.instance.map[x, y] != (int)MapGenerator.tileType.endRiver);
    }

    /// <summary>
    /// Spawns house in the new position and deletes grass tile
    /// </summary>
    private void SpawnBuilding()
    {
        //delete the grass tile
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(GameManager.instance.ArrayCoordToWorldCoordX(lastBuildingPos.x), 
            GameManager.instance.ArrayCoordToWorldCoordY(lastBuildingPos.y)), Vector3.forward);

        Debug.Log(ray.collider.tag);

        if (ray.collider.gameObject.tag == "Grass")
        {
            Destroy(ray.collider.gameObject);
        }
        else if (ray.collider.gameObject.tag == "Tree")
        {
            Debug.Log("Called from " + transform.position.x + " " + transform.position.y);
            ray.collider.GetComponent<Tree>().DealDamage(10);
        }

        //update the map
        GameManager.instance.map[lastBuildingPos.x, lastBuildingPos.y] = (int)MapGenerator.tileType.building;
        //buildingPositions.Add(lastBuildingPos);

        //spawn the house and update its parent
        GameObject building = Instantiate(buildings[Random.Range(0, buildings.Length)].gameObject, new Vector3(GameManager.instance.ArrayCoordToWorldCoordX(lastBuildingPos.x),
            GameManager.instance.ArrayCoordToWorldCoordY(lastBuildingPos.y), 0), Quaternion.identity);
        building.transform.parent = buildingsParent;

        buildingCount++;

        SetSpawnDirection();

        GameManager.instance.lumberInMarket -= 50;
    }
    
    /// <summary>
    /// Sets the spawn directions that can be used which helps pick the next tile to build on
    /// </summary>
    private void SetSpawnDirection()
    {
        if(lastBuildingPos.x == 0)
        {
            spawnDirection.canSpawnLeft = false;
        }
        if(lastBuildingPos.x == GameManager.instance.sizeX - 1)
        {
            spawnDirection.canSpawnRight = false;
        }
        if (lastBuildingPos.y == 0)
        {
            spawnDirection.canSpawnDown = false;
        }
        if (lastBuildingPos.y == GameManager.instance.sizeY - 1)
        {
            spawnDirection.canSpawnUp = false;
        }
    }

    /// <summary>
    /// picks a random side and picks a random tile on that side
    /// </summary>
    /// <param name="overrideVal">override gives an offset value if the houses aren't on an edge of the screen, -1 means edge</param>
    private void PickRandomSideTile(int overrideVal = -1)
    {
        //TODO: Clean up the Up and Down sides. Also, fix index out of bounds error when spawning many houses
        if (side == Vector2.left || side == Vector2.right)
        {
            //no value provided so its the first side
            if (overrideVal == -1)
            {
                //set left
                if (side == Vector2.left)
                {
                    lastBuildingPos.x = 0;
                }
                //set right
                else if (side == Vector2.right)
                {
                    lastBuildingPos.x = GameManager.instance.sizeX - 1;
                }
            }
            //otherwise, set x to the override value
            else
            {
                lastBuildingPos.x = overrideVal;
            }

            //FindSuitableSpawnPos(lastBuildingPos.y, GameManager.instance.sizeY);

            //get random y start position, then store that value in startY, and use hitTop to see if we have hit the top of the map
            lastBuildingPos.y = Random.Range(0, GameManager.instance.sizeY);
            int startY = lastBuildingPos.y;
            bool hitTop = false;

            //loop sizeY times to check if we hit a river
            for (int i = 0; i < GameManager.instance.sizeY; i++)
            {
                //if this tile is not a river, use these coords
                if (TileNotRiver(lastBuildingPos.x, lastBuildingPos.y))
                {
                    //set neighbor tiles to this first tile in the column and return
                    greaterNeighbor = lastBuildingPos;
                    lesserNeighbor = lastBuildingPos;
                    return;
                }
                //if we hit a river tile
                else
                {
                    //if we hit top, go down
                    if (hitTop)
                    {
                        lastBuildingPos.y--;
                    }
                    //if we haven't hit top, go up
                    else
                    {
                        lastBuildingPos.y++;
                        //if we hit top now, reset to startY and go down
                        if (lastBuildingPos.y >= GameManager.instance.sizeY)
                        {
                            lastBuildingPos.y = startY;
                            lastBuildingPos.y--;
                        }
                    }
                }
            }

            //if we made it here, this entire column is a river, so move to the next column
            if (side == Vector2.right)
            {
                PickRandomSideTile(lastBuildingPos.x + 1);
            }
            else if (side == Vector2.left)
            {
                PickRandomSideTile(lastBuildingPos.x - 1);
            }
        }

        if (side == Vector2.up || side == Vector2.down)
        {
            //no value provided so its the first side
            if (overrideVal == -1)
            {
                //set up
                if (side == Vector2.up)
                {
                    lastBuildingPos.y = GameManager.instance.sizeY - 1;
                }
                //set down
                else if (side == Vector2.down)
                {
                    lastBuildingPos.y = 0;
                }
            }
            //otherwise, set y to the override value
            else
            {
                lastBuildingPos.y = overrideVal;
            }

            //FindSuitableSpawnPos(lastBuildingPos.y, GameManager.instance.sizeY);

            //get random y start position, then store that value in startY, and use hitTop to see if we have hit the top of the map
            lastBuildingPos.x = Random.Range(0, GameManager.instance.sizeX);
            int startX = lastBuildingPos.x;
            bool hitSide = false;

            //loop sizeX times to check if we hit a river
            for (int i = 0; i < GameManager.instance.sizeX; i++)
            {
                //if this tile is not a river, use these coords
                if (TileNotRiver(lastBuildingPos.x, lastBuildingPos.y))
                {
                    //set neighbor tiles to this first tile in the column and return
                    greaterNeighbor = lastBuildingPos;
                    lesserNeighbor = lastBuildingPos;
                    return;
                }
                //if we hit a river tile
                else
                {
                    //if we hit top, go down
                    if (hitSide)
                    {
                        lastBuildingPos.x--;
                    }
                    //if we haven't hit top, go up
                    else
                    {
                        lastBuildingPos.x++;
                        //if we hit top now, reset to startY and go down
                        if (lastBuildingPos.x >= GameManager.instance.sizeX)
                        {
                            lastBuildingPos.x = startX;
                            lastBuildingPos.x--;
                        }
                    }
                }
            }

            //if we made it here, this entire column is a river, so move to the next column
            if (side == Vector2.up)
            {
                PickRandomSideTile(lastBuildingPos.y - 1);
            }
            else if (side == Vector2.down)
            {
                PickRandomSideTile(lastBuildingPos.y + 1);
            }
        }
    }

    private void FindSuitableSpawnPos(int pos, int maxSize)
    {
        //get random y start position, then store that value in startY, and use hitTop to see if we have hit the top of the map
        pos = Random.Range(0, GameManager.instance.sizeY);
        int startPos = pos;
        bool hitTop = false;

        //loop sizeY times to check if we hit a river
        for (int i = 0; i < maxSize; i++)
        {
            //if this tile is not a river, use these coords
            if (TileNotRiver(lastBuildingPos.x, lastBuildingPos.y))
            {
                //set neighbor tiles to this first tile in the column and return
                greaterNeighbor = lastBuildingPos;
                lesserNeighbor = lastBuildingPos;
                return;
            }
            //if we hit a river tile
            else
            {
                //if we hit top, go down
                if (hitTop)
                {
                    pos--;
                }
                //if we haven't hit top, go up
                else
                {
                    pos++;
                    //if we hit top now, reset to startY and go down
                    if (pos >= maxSize)
                    {
                        pos = startPos;
                        pos--;
                    }
                }
            }
        }
    }

}

[System.Serializable]
struct SpawnDirection
{
    public bool canSpawnUp;
    public bool canSpawnDown;
    public bool canSpawnLeft;
    public bool canSpawnRight;

    public SpawnDirection(bool up, bool down, bool left, bool right)
    {
        canSpawnUp = up;
        canSpawnDown = down;
        canSpawnLeft = left;
        canSpawnRight = right;
    }

    public void Reset()
    {
        canSpawnUp = true;
        canSpawnDown = true;
        canSpawnLeft = true;
        canSpawnRight = true;
    }
}
