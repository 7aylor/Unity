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

    private LinkedList<Vector2Int> buildingPositions;

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

        //temporarily call here
        FindSuitableSpawnPos();
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
            do
            {
                PickRandomSideTile();
            } while (GameManager.instance.map[lastBuildingPos.x, lastBuildingPos.y] != (int)MapGenerator.tileType.grass);

            SpawnBuilding();
        }
        else
        {
            if (side == Vector2.left)
            {
                #region old
                ////find nearest grass tile and spawn
                //int distanceFromStartTile = 1;
                //bool goUp = true;

                //for (int x = startPos.x; x < GameManager.instance.sizeX; x++)
                //{
                //    for (int y = startPos.y; y < GameManager.instance.sizeY && y > 0; y += distanceFromStartTile)
                //    {

                //        if (y + distanceFromStartTile == GameManager.instance.sizeY)
                //        {
                //            distanceFromStartTile = startPos.y - 1;
                //        }
                //        else if (y + distanceFromStartTile == 0)
                //        {
                //            distanceFromStartTile = startPos.y + 1;
                //        }
                //        //else
                //        //{
                //        //    if (goUp)
                //        //    {
                //        //        distanceFromStartTile++;
                //        //    }
                //        //    else
                //        //    {
                //        //        distanceFromStartTile--;
                //        //    }
                //        //}

                //        if (GameManager.instance.map[startPos.x, y] == (int)MapGenerator.tileType.grass)
                //        {
                //            startPos.y = y;
                //            break;
                //        }
                //    }
                //}
#endregion



                SpawnBuilding();

            }
            else if (side == Vector2.right)
            {
                #region old
                ////find nearest grass tile and spawn
                //int distanceFromStartTile = 0;
                //bool goUp = true;

                //for (int x = startPos.x; x > 0; x--)
                //{
                //    for (int y = startPos.y; y < GameManager.instance.sizeY && y > 0; y += distanceFromStartTile)
                //    {
                //        if (y + distanceFromStartTile == GameManager.instance.sizeY)
                //        {
                //            distanceFromStartTile = startPos.y - 1;
                //        }
                //        else if (y + distanceFromStartTile == 0)
                //        {
                //            distanceFromStartTile = startPos.y + 1;
                //        }
                //        //else
                //        //{
                //        //    if (goUp)
                //        //    {
                //        //        distanceFromStartTile++;
                //        //    }
                //        //    else
                //        //    {
                //        //        distanceFromStartTile--;
                //        //    }
                //        //}

                //        if (GameManager.instance.map[startPos.x, y] == (int)MapGenerator.tileType.grass)
                //        {
                //            startPos.y = y;
                //            break;
                //        }
                //    }
                //}

#endregion


                SpawnBuilding();
            }
            else if (side == Vector2.up || side == Vector2.down)
            {
                lastBuildingPos.x += 1;
                SpawnBuilding();
            }
        }
    }

    /// <summary>
    /// Spawns house in the new position and deletes grass tile
    /// </summary>
    private void SpawnBuilding()
    {
        Debug.Log("Building a Building");

        //update the map
        GameManager.instance.map[lastBuildingPos.x, lastBuildingPos.y] = (int)MapGenerator.tileType.building;
        //buildingPositions.Add(lastBuildingPos);

        //spawn the house and update its parent
        GameObject building = Instantiate(buildings[0].gameObject, new Vector3(GameManager.instance.ArrayCoordToWorldCoordX(lastBuildingPos.x),
            GameManager.instance.ArrayCoordToWorldCoordY(lastBuildingPos.y), 0), Quaternion.identity);
        building.transform.parent = buildingsParent;

        //delete the grass tile
        RaycastHit2D ray = Physics2D.Raycast(building.transform.position, Vector2.zero);
        if (ray.collider.gameObject.tag == "Grass")
        {
            Destroy(ray.collider.gameObject);
        }

        buildingCount++;

        if(buildingCount > 1)
        {
            //update lastNeighbor to reflect new building placement

            //increase
            if (Random.Range(0, 2) == 1)
            {

            }
            ////decrease
            else
            {

            }
        }

        SetNeighborCoords();

        GameManager.instance.lumberInMarket -= 50;
    }

    /// <summary>
    /// Sets the lesser and greater neighbor coorinates for the current building position
    /// </summary>
    private void SetNeighborCoords()
    {
        if (side == Vector2.left ||side == Vector2.right)
        {
            if (lastNeighbor == lastNeighborChange.lesser)
            {
                lesserNeighbor.x = lastBuildingPos.x;
                if (lastBuildingPos.y > 0)
                {
                    lesserNeighbor.y = lastBuildingPos.y - 1;
                }
                else
                {
                    lesserNeighbor.y = -1; //-1 indicates lastBuildingPos is at 0
                }
            }
            if(lastNeighbor == lastNeighborChange.greater)
            {
                greaterNeighbor.x = lastBuildingPos.x;
                if (lastBuildingPos.y < GameManager.instance.sizeY - 1)
                {
                    greaterNeighbor.y = lastBuildingPos.y + 1;
                }
                else
                {
                    greaterNeighbor.y = -1; //-1 indicates lastBuildingPos is at sizeY
                }
            }
        }
        else if (side == Vector2.up || side == Vector2.down)
        {
            if(lastNeighbor == lastNeighborChange.lesser)
            {
                lesserNeighbor.y = lastBuildingPos.y;
                if (lastBuildingPos.x > 0)
                {
                    lesserNeighbor.x = lastBuildingPos.y - 1;
                }
                else
                {
                    lesserNeighbor.x = -1; //-1 indicates lastBuildingPos is at 0
                }
            }

            if (lastNeighbor == lastNeighborChange.greater)
            {
                greaterNeighbor.y = lastBuildingPos.y;
                if (lastBuildingPos.x < GameManager.instance.sizeX - 1)
                {
                    greaterNeighbor.x = lastBuildingPos.y + 1;
                }
                else
                {
                    greaterNeighbor.x = -1; //-1 indicates lastBuildingPos is at sizeY
                }
            }
        }
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
            lastBuildingPos.x = 0;
            lastBuildingPos.y = Random.Range(0, GameManager.instance.sizeY);
        }
        //pick right side of the map
        else if (side == Vector2.right)
        {
            lastBuildingPos.x = GameManager.instance.sizeX - 1;
            lastBuildingPos.y = Random.Range(0, GameManager.instance.sizeY);
        }
        //pick top of the map
        else if (side == Vector2.up)
        {
            lastBuildingPos.x = Random.Range(0, GameManager.instance.sizeX);
            lastBuildingPos.y = GameManager.instance.sizeY - 1;
        }
        //pick bottom of the map
        else
        {
            lastBuildingPos.x = Random.Range(0, GameManager.instance.sizeX);
            lastBuildingPos.y = 0;
        }
    }
}
