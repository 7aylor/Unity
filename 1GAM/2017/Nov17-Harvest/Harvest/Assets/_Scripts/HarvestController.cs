using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestController : MonoBehaviour {

    public int CropsInSeason { get; set; }
    public int TotalGoldEarned { get; private set; }
    private Harvest[] tiles;
    private int highestEarningCropVal;
    private string highEarningCropName;

	// Use this for initialization
	void Start () {
        tiles = FindObjectsOfType<Harvest>();
        TotalGoldEarned = 0;
        ResetHighestEarnedCrop();
	}

    /// <summary>
    /// Gets the total gold earned this season
    /// </summary>
    /// <returns>Total gold earned</returns>
    public int GetSeasonsHarvest()
    {
        int gold = 0;
        ResetHighestEarnedCrop();

        foreach(Harvest tile in tiles)
        {
            int moneyCrop = tile.CalculateHarvest();

            CheckHighestEarnedCrop(tile, moneyCrop);

            gold += moneyCrop;
        }

        TotalGoldEarned += gold;

        return gold;
    }

    private void CheckHighestEarnedCrop(Harvest tile, int moneyCrop)
    {
        if (moneyCrop > highestEarningCropVal)
        {
            highestEarningCropVal = moneyCrop;
            highEarningCropName = tile.GetComponent<Image>().sprite.name;
        }
    }

    private void ResetHighestEarnedCrop()
    {
        CropsInSeason = 0;
        highestEarningCropVal = 0;
        highEarningCropName = "None";
    }

    public string GetHighestEarnedCrop()
    {
        return highEarningCropName;
    }

    public int GetHighestEarnedCropAmount()
    {
        return highestEarningCropVal;
    }

    /// <summary>
    /// Gets the number of crops harvested this season
    /// </summary>
    /// <returns></returns>
    public int GetNumberOfCropsHarvested()
    {
        int counter = 0;

        foreach (Transform tile in transform)
        {
            string spriteName = tile.GetComponent<Image>().sprite.name;
            if (HandType.Crops.ContainsKey(spriteName))
            {
                counter++;
            }
        }

        return counter;
    }
}
