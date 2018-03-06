using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public int sizeX;
    public int sizeY;
    public int[,] map;
    public Transform terrainContainer;

    [Range(0,5)]
    public int smoothCycles;

    [Range(0, 100)]
    public int treeSpawnThreshold;

    [Range(0, 100)]
    public int rockSpawnThreshold;

    [Range(0,6)]
    public int neighborThreshold;

    #region GameObjects
    public GameObject grass;
    public GameObject trees;
    public GameObject rock;
    public GameObject riverStraight;
    public GameObject riverCurve;
    public GameObject riverStart;
    public GameObject riverEnd;
    #endregion

    private enum tileType
    {
        grass,
        tree,
        rock,
        startRiver,
        straightRiver,
        curveRiver,
        endRiver
    }

    private void Start()
    {
        map = new int[sizeX, sizeY];
        GenerateRandomMap();
        SmoothMap();
        CreateRiver();
        SpawnTerrain();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyTerrain();
            GenerateRandomMap();
            SmoothMap();
            CreateRiver();
            SpawnTerrain();
        }
    }

    private void GenerateRandomMap()
    {
        for(int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {

                int num = Random.Range(0, 100);

                if(num < treeSpawnThreshold)
                {
                    map[x, y] = (int)tileType.tree;
                }
                else if(num > rockSpawnThreshold)
                {
                    map[x, y] = (int)tileType.rock;
                }
                else
                {
                    map[x, y] = (int)tileType.grass;
                }
            }
        }
    }

    private void CreateRiver()
    {
        bool startOnX = System.Convert.ToBoolean(Random.Range(0, 2));

        int startX = 0;
        int startY = 0;

        //start on x axis x=0 or x=sizeX
        if (startOnX == true)
        {
            startX = PickSide(sizeX);
            startY = Random.Range(0, sizeY - 1);

            int x = startX;
            //on the left
            if (startX > 0)
            {
                while (x > 0)
                {
                    x--;
                    if (x == 0)
                    {
                        map[startX, x] = (int)tileType.endRiver;
                    }
                    else
                    {
                        map[startX, x] = (int)tileType.straightRiver;
                    }
                }
            }
            //on the right
            else
            {
                while (x < sizeX - 1)
                {
                    x++;
                    if (x == sizeX)
                    {
                        map[startX, x] = (int)tileType.endRiver;
                    }
                    else
                    {
                        map[startX, x] = (int)tileType.straightRiver;
                    }
                }
            }
        }
        //start on y axis
        else
        {
            startY = PickSide(sizeY);
            startX = Random.Range(0, sizeX - 1);

            int y = startY;
            //on the bottom
            if (startY > 0)
            {
                while (y > 0)
                {
                    y--;
                    if (y == 0)
                    {
                        map[startX, y] = (int)tileType.endRiver;
                    }
                    else
                    {
                        map[startX, y] = (int)tileType.straightRiver;
                    }
                }
            }
            //on top
            else
            {
                while (y < sizeY - 1)
                {
                    y++;

                    if(y == sizeY)
                    {
                        map[startX, y] = (int)tileType.endRiver;
                    }
                    else
                    {
                        map[startX, y] = (int)tileType.straightRiver;
                    }
                    
                }
            }
        }

        map[startX, startY] = (int)tileType.startRiver;
    }

    private int PickSide(int maxSize)
    {
        int startVal;
        if (Random.Range(0, 2) == 0)
        {
            startVal = 0;
        }
        else
        {
            startVal = maxSize - 1;
        }

        return startVal;
    }

    private void SmoothMap()
    {
        for(int i = 0; i < smoothCycles; i++)
        {
            for(int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    int neighboringForestCount = GetNeighboringForestCount(x, y);

                    if(neighboringForestCount > neighborThreshold)
                    {
                        map[x, y] = (int)tileType.tree;
                    }
                    else if (neighboringForestCount < neighborThreshold)
                    {
                        map[x, y] = (int)tileType.grass;
                    }
                }
            }
        }
    }

    private int GetNeighboringForestCount(int posInGridX, int posInGridY)
    {
        int neighborsWithForest = 0;

        for(int x = posInGridX - 1; x <= posInGridX + 1; x++)
        {
            for (int y = posInGridY - 1; y <= posInGridY + 1; y++)
            {
                if(x > 0 && x < sizeX && y > 0 && y < sizeY && (y != posInGridY || x != posInGridX))
                {
                    if(map[x,y] == 1)
                    {
                        neighborsWithForest++;
                    }
                }
            }
        }

        return neighborsWithForest;
    }

    /// <summary>
    /// Loops through the map array and spawns terrain
    /// </summary>
    private void SpawnTerrain()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GameObject newTile;

                if (map[x,y] == (int)tileType.tree)
                {
                    //Instantiate(black, new Vector3(-sizeX/2 + x + 0.5f, -sizeY/2 + y + 0.5f, 0), Quaternion.identity);
                    newTile = Instantiate(trees, new Vector3((float)x / 2 - sizeX / 4 + 0.5f, (float)y / 2 - sizeY / 4 + 0.25f, 0), Quaternion.identity);
                }
                else if(map[x, y] == (int)tileType.rock)
                {
                    newTile = Instantiate(rock, new Vector3((float)x / 2 - sizeX / 4 + 0.5f, (float)y / 2 - sizeY / 4 + 0.25f, 0), Quaternion.identity);
                }
                else if(map[x,y] == (int)tileType.startRiver)
                {
                    Quaternion rotation;

                    if(x == 0)
                    {
                        rotation = Quaternion.Euler(0, 0, 90);
                    }
                    else if (x == sizeX - 1)
                    {
                        rotation = Quaternion.Euler(0, 0, -90);
                    }
                    else if(y == 0)
                    {
                        rotation = rotation = Quaternion.Euler(0, 0, 180);
                    }
                    else
                    {
                        rotation = Quaternion.identity;
                    }

                    newTile = Instantiate(riverStart, new Vector3((float)x / 2 - sizeX / 4 + 0.5f, (float)y / 2 - sizeY / 4 + 0.25f, 0), rotation);
                }
                else if(map[x, y] == (int)tileType.straightRiver)
                {
                    Quaternion rotation = Quaternion.identity;

                    if(x > 0 && x < sizeX)
                    {
                        if (map[x - 1, y] == (int)tileType.straightRiver || map[x - 1, y] == (int)tileType.startRiver ||
                            map[x + 1, y] == (int)tileType.straightRiver || map[x + 1, y] == (int)tileType.startRiver)
                        {
                            rotation = Quaternion.Euler(0, 0, 90);
                        }
                    }
                    
                    newTile = Instantiate(riverStraight, new Vector3((float)x / 2 - sizeX / 4 + 0.5f, (float)y / 2 - sizeY / 4 + 0.25f, 0), rotation);
                }
                else if (map[x, y] == (int)tileType.endRiver)
                {
                    Quaternion rotation;

                    if (x == 0)
                    {
                        rotation = Quaternion.Euler(0, 0, 90);
                    }
                    else if (x == sizeX - 1)
                    {
                        rotation = Quaternion.Euler(0, 0, -90);
                    }
                    else if (y == 0)
                    {
                        rotation = rotation = Quaternion.Euler(0, 0, 180);
                    }
                    else
                    {
                        rotation = Quaternion.identity;
                    }

                    newTile = Instantiate(riverEnd, new Vector3((float)x / 2 - sizeX / 4 + 0.5f, (float)y / 2 - sizeY / 4 + 0.25f, 0), rotation);
                }
                else
                {
                    newTile = Instantiate(grass, new Vector3((float)x / 2 - sizeX / 4 + 0.5f, (float)y / 2 - sizeY / 4 + 0.25f, 0), Quaternion.identity);
                }

                newTile.transform.parent = terrainContainer;
            }
        }
    }

    private void DestroyTerrain()
    {
        foreach(Transform child in terrainContainer)
        {
            Destroy(child.gameObject);
        }
    }

}
