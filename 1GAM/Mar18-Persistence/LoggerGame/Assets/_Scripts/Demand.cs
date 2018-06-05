﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The demand should start at a point that is dependant on the health of the forest. If there are a lot of tree,
/// the demand should be high because there is not as much lumber in the market. This should also be dependant on the
/// type of terrain. For instance, arid terrain will have higher demand always because there are fewer trees. Demand 
/// influences how often an event is fired. The higher the demand, the more events occur. When the demand is lower, fewer
/// events will fire. This should give the player more reason to balance how quickly they chop down trees. 
/// THINGS TO CONSIDER: How does planting trees influence demand? Keep track of number of sales and lumber in the market.
/// Should add a closer look at the market by click on the demand icon
/// </summary>
public class Demand : MonoBehaviour {

    //public float minTimeToIncrease;
    //public float maxTimeToIncrease;
    //public int minDemand;
    //public int maxDemand;


    public int spawnTimeVeryLow;
    public int spawnTimeLow;
    public int spawnTimeMedium;
    public int spawnTimeHigh;
    public int spawnTimeVeryHigh;

    public Color veryLowColor;
    public Color lowColor;
    public Color mediumColor;
    public Color highColor;
    public Color veryHighColor;

    public enum demand { Very_High, High, Medium, Low, Very_Low }
    public demand marketDemand;

    private TMP_Text text;
    private BidManager bidManager;

    //private float timeToNextDemandIncrease;
    //private float timeSinceLastIncrease;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        bidManager = FindObjectOfType<BidManager>();
    }

    // Use this for initialization
    void Start()
    {
        //UpdateDemandText();
        //timeSinceLastIncrease = 0;
        //timeToNextDemandIncrease = GetNewTimeToIncrease();

        //marketDemand = demand.medium;
    } 

    //private void Update()
    //{
    //    //check if it is time to increase the demand
    //    if(timeSinceLastIncrease >= timeToNextDemandIncrease)
    //    {
    //        //increase demand
    //        GameManager.instance.demand += GetNewDemand();

    //        //update text
    //        UpdateDemandText();

    //        //get new time
    //        timeToNextDemandIncrease = GetNewTimeToIncrease();

    //        //reset the timer
    //        timeSinceLastIncrease = 0;

    //        //maybe throw an event here?
    //    }
    //    else
    //    {
    //        timeSinceLastIncrease += Time.deltaTime;
    //    }
    //}

    //private float GetNewTimeToIncrease()
    //{
    //    return Random.Range(minTimeToIncrease, maxTimeToIncrease);
    //}

    //private int GetNewDemand()
    //{
    //    return Random.Range(minDemand, maxDemand);
    //}

    //private void UpdateDemandText()
    //{
    //    text.text = GameManager.instance.demand.ToString();
    //}

    public void UpdateDemand()
    {
        if(GameManager.instance.lumberInMarket >= 500)
        {
            marketDemand = demand.Very_Low;
            text.text = demand.Very_Low.ToString().Replace('_', ' ');
            text.color = veryLowColor;
            bidManager.UpdateSpawnTime(spawnTimeVeryLow);
        }
        else if(GameManager.instance.lumberInMarket < 500 && GameManager.instance.lumberInMarket >= 300)
        {
            marketDemand = demand.Low;
            text.text = demand.Low.ToString();
            text.color = lowColor;
            bidManager.UpdateSpawnTime(spawnTimeLow);
        }
        else if (GameManager.instance.lumberInMarket < 300 && GameManager.instance.lumberInMarket >= 200)
        {
            marketDemand = demand.Medium;
            text.text = demand.Medium.ToString();
            text.color = mediumColor;
            bidManager.UpdateSpawnTime(spawnTimeMedium);
        }
        else if (GameManager.instance.lumberInMarket < 200 && GameManager.instance.lumberInMarket >= 100)
        {
            marketDemand = demand.High;
            text.text = demand.High.ToString();
            text.color = highColor;
            bidManager.UpdateSpawnTime(spawnTimeHigh);
        }
        else
        {
            marketDemand = demand.Very_High;
            text.text = demand.Very_High.ToString().Replace('_', ' ');
            text.color = veryHighColor;
            bidManager.UpdateSpawnTime(spawnTimeVeryHigh);
        }
    }
}