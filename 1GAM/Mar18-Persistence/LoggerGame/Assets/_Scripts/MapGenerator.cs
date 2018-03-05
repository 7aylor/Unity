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
    #endregion

    private void Start()
    {
        map = new int[sizeX, sizeY];
        GenerateRandomMap();
        SmoothMap();
        SpawnTerrain();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyTerrain();
            GenerateRandomMap();
            SmoothMap();
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
                    map[x, y] = 1;
                }
                else if(num > rockSpawnThreshold)
                {
                    map[x, y] = 2;
                }
                else
                {
                    map[x, y] = 0;
                }
            }
        }
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
                        map[x, y] = 1;
                    }
                    else if (neighboringForestCount < neighborThreshold)
                    {
                        map[x, y] = 0;
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

                if (map[x,y] == 1)
                {
                    //Instantiate(black, new Vector3(-sizeX/2 + x + 0.5f, -sizeY/2 + y + 0.5f, 0), Quaternion.identity);
                    newTile = Instantiate(trees, new Vector3((float)x / 2 - sizeX / 4 + 0.5f, (float)y / 2 - sizeY / 4 + 0.25f, 0), Quaternion.identity);
                }
                else if(map[x, y] == 2)
                {
                    newTile = Instantiate(rock, new Vector3((float)x / 2 - sizeX / 4 + 0.5f, (float)y / 2 - sizeY / 4 + 0.25f, 0), Quaternion.identity);
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
