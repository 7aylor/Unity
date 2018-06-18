using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindSuitableSpawnPos();
        }
    }

    /// <summary>
    /// Called from MarketManager to spawn a house
    /// </summary>
    public void BuildFromMarket()
    {
        //find a suitable position on the edge of the world
        FindSuitableSpawnPos();
    }

    /// <summary>
    /// 
    /// </summary>
    private void FindSuitableSpawnPos()
    { 
        //pick random edge then spawn first building in grass position
        if(buildingCount < 1)
        {
            //do
            //{
            //    PickRandomSideTile();
            //} while (GameManager.instance.map[lastBuildingPos.x, lastBuildingPos.y] != (int)MapGenerator.tileType.grass);

            lastBuildingPos.x = GameManager.instance.sizeX - 1;
            lastBuildingPos.y = 5;

            greaterNeighbor = lastBuildingPos;
            lesserNeighbor = lastBuildingPos;

            side = Vector2.right;

            SpawnBuilding();
        }
        else
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
            else if (side == Vector2.up || side == Vector2.down)
            {
                lastBuildingPos.x += 1;
                SpawnBuilding();
            }
        }
    }

    /// <summary>
    /// Used to Spawn buildings vertically. Checks to see if we have chosen a river tile or have gone out of bounds
    /// </summary>
    /// <param name="goLeft">true if we start on the right, false if we start on the left</param>
    private void VerticalSpawns(bool goLeft)
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
            } while (y > 0 && y < GameManager.instance.sizeY && TileNotRiver(lastBuildingPos.x, y) == false);

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
            } while (TileNotRiver(lastBuildingPos.x, y) == false);

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
        PickRandomSideTile();

        if(goLeft == true)
        {
            lastBuildingPos.x -= spawnRounds;
        }
        else
        {
            lastBuildingPos.x += spawnRounds;
        }

        //reset the values and spawn a tile in the new position
        greaterNeighbor = lastBuildingPos;
        lesserNeighbor = lastBuildingPos;
        spawnDirection.canSpawnUp = true;
        spawnDirection.canSpawnDown = true;
        SpawnBuilding();
    }

    /// <summary>
    /// Returns if the give tile coordinate is a river tile or not
    /// </summary>
    /// <param name="x">x position in map array</param>
    /// <param name="y">y position in map array</param>
    /// <returns></returns>
    private bool TileNotRiver(int x, int y)
    {
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

        //update the map
        GameManager.instance.map[lastBuildingPos.x, lastBuildingPos.y] = (int)MapGenerator.tileType.building;
        //buildingPositions.Add(lastBuildingPos);

        //spawn the house and update its parent
        GameObject building = Instantiate(buildings[Random.Range(0, buildings.Length)].gameObject, new Vector3(GameManager.instance.ArrayCoordToWorldCoordX(lastBuildingPos.x),
            GameManager.instance.ArrayCoordToWorldCoordY(lastBuildingPos.y), 0), Quaternion.identity);
        building.transform.parent = buildingsParent;

        //delete the grass tile
        RaycastHit2D ray = Physics2D.Raycast(building.transform.position, Vector2.zero);
        if (ray.collider.gameObject.tag == "Grass")
        {
            Destroy(ray.collider.gameObject);
        }

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
    private void PickRandomSideTile(int overrideVal = -1)
    {
        //TODO: Clean up the Up and Down sides. Also, fix index out of bounds error when spawning many houses
        int count = 0;
        //pick left side of the map
        if (side == Vector2.left)
        {
            if(overrideVal == -1)
            {
                lastBuildingPos.x = 0;
            }
            else
            {
                lastBuildingPos.x = overrideVal;
            }

            do
            {
                lastBuildingPos.y = Random.Range(0, GameManager.instance.sizeY);

                count++;

                if(TileNotRiver(lastBuildingPos.x, lastBuildingPos.y) == true)
                {
                    return;
                }
            } while (TileNotRiver(lastBuildingPos.x, lastBuildingPos.y) == false && count < 100);

            PickRandomSideTile(lastBuildingPos.x + 1);
        }
        //pick right side of the map
        else if (side == Vector2.right)
        {
            if(overrideVal == -1)
            {
                lastBuildingPos.x = GameManager.instance.sizeX - 1;
            }
            else
            {
                lastBuildingPos.x = overrideVal;
            }

            lastBuildingPos.y = 0;

            do
            {
                lastBuildingPos.y = Random.Range(0, GameManager.instance.sizeY);

                count++;

                if (TileNotRiver(lastBuildingPos.x, lastBuildingPos.y) == true)
                {
                    return;
                }
            } while (TileNotRiver(lastBuildingPos.x, lastBuildingPos.y) == false && count < 100);

            PickRandomSideTile(lastBuildingPos.x + 1);
        }
        //pick top of the map
        else if (side == Vector2.up)
        {
            if (overrideVal == -1)
            {
                lastBuildingPos.y = GameManager.instance.sizeY - 1;
            }
            else
            {
                lastBuildingPos.y = overrideVal;
            }

            for (int i = 0; i < GameManager.instance.sizeX; i++)
            {
                lastBuildingPos.x = Random.Range(0, GameManager.instance.sizeX);

                if (TileNotRiver(lastBuildingPos.x, lastBuildingPos.y))
                {
                    return;
                }
            }

            PickRandomSideTile(overrideVal + 1);
        }
        //pick bottom of the map
        else
        {

            if (overrideVal == -1)
            {
                lastBuildingPos.y = 0;
            }
            else
            {
                lastBuildingPos.y = overrideVal;
            }

            for (int i = 0; i < GameManager.instance.sizeX; i++)
            {
                lastBuildingPos.x = Random.Range(0, GameManager.instance.sizeX);

                if (TileNotRiver(lastBuildingPos.x, lastBuildingPos.y))
                {
                    return;
                }
            }

            PickRandomSideTile(overrideVal + 1);
        }

        greaterNeighbor = lastBuildingPos;
        lesserNeighbor = lastBuildingPos;
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
