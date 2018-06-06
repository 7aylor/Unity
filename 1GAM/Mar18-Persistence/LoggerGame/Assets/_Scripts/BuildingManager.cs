using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public GameObject[] house;
    private int houseCount;
    private Vector2Int startPos; //holds the first house start position in array coords
    private Vector2Int lastHousePos; //holds the position of the last house spawned

    private void Start()
    {
        houseCount = 0;
    }

    /// <summary>
    /// Called from MarketManager to spawn a house
    /// </summary>
    public void SpawnHouse()
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
            
        }
        else
        {

        }
    }

}
