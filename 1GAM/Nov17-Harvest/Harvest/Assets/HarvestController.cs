using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestController : MonoBehaviour {

    private Harvest[] tiles;

	// Use this for initialization
	void Start () {
        tiles = FindObjectsOfType<Harvest>();
	}

    public int GetSeasonsHarvest()
    {
        int gold = 0;

        foreach(Harvest tile in tiles)
        {
            gold += tile.CalculateHarvest();
        }

        return gold;
    }
}
