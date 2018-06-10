using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The demand should start at a point that is dependant on the health of the forest. If there are a lot of tree,
/// the demand should be high because there is not as much lumber in the market. This should also be dependant on the
/// type of terrain. For instance, arid terrain will have higher demand always because there are fewer trees. Demand 
/// influences how often an bid is fired. The higher the demand, the more bids occur. When the demand is lower, fewer
/// bids will fire. This should give the player more reason to balance how quickly they chop down trees. 
/// THINGS TO CONSIDER: How does planting trees influence demand? Keep track of number of sales and lumber in the market.
/// </summary>
public class Demand : MonoBehaviour {

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

    public enum demand { Very_High = 0, High = 100, Medium = 200, Low = 300, Very_Low = 500 }
    public demand marketDemand;

    private TMP_Text marketDemandText;
    private BidManager bidManager;

    private void Awake()
    {
        marketDemandText = GetComponent<TMP_Text>();
        bidManager = FindObjectOfType<BidManager>();
    }

    private void Start()
    {
        Debug.Log("Very Low " + (int)demand.Very_Low);
    }

    /// <summary>
    /// Updates the Demand variables and text. Called from MarketManager, 
    /// </summary>
    public void UpdateDemand()
    {
        //very low demand
        if(GameManager.instance.lumberInMarket >= (int)demand.Very_Low)
        {
            SetDemandVarsAndText(demand.Very_Low, demand.Very_Low.ToString().Replace('_', ' '), veryLowColor, spawnTimeVeryLow);
            //marketDemand = demand.Very_Low;
            //marketDemandText.text = demand.Very_Low.ToString().Replace('_', ' ');
            //marketDemandText.color = veryLowColor;
            //bidManager.UpdateSpawnTime(spawnTimeVeryLow);
        }
        //low demand
        else if(GameManager.instance.lumberInMarket < (int)demand.Very_Low && GameManager.instance.lumberInMarket >= (int)demand.Low)
        {
            SetDemandVarsAndText(demand.Low, demand.Low.ToString(), lowColor, spawnTimeLow);
            //marketDemand = demand.Low;
            //marketDemandText.text = demand.Low.ToString();
            //marketDemandText.color = lowColor;
            //bidManager.UpdateSpawnTime(spawnTimeLow);
        }
        //medium demand
        else if (GameManager.instance.lumberInMarket < (int)demand.Low && GameManager.instance.lumberInMarket >= (int)demand.Medium)
        {
            SetDemandVarsAndText(demand.Medium, demand.Medium.ToString(), mediumColor, spawnTimeMedium);
            //marketDemand = demand.Medium;
            //marketDemandText.text = demand.Medium.ToString();
            //marketDemandText.color = mediumColor;
            //bidManager.UpdateSpawnTime(spawnTimeMedium);
        }
        //high demand
        else if (GameManager.instance.lumberInMarket < (int)demand.Medium && GameManager.instance.lumberInMarket >= (int)demand.High)
        {
            SetDemandVarsAndText(demand.High, demand.High.ToString(), highColor, spawnTimeHigh);
            //marketDemand = demand.High;
            //marketDemandText.text = demand.High.ToString();
            //marketDemandText.color = highColor;
            //bidManager.UpdateSpawnTime(spawnTimeHigh);
        }
        //very high demand
        else
        {
            SetDemandVarsAndText(demand.Very_High, demand.Very_High.ToString().Replace('_', ' '), veryHighColor, spawnTimeVeryHigh);
            //marketDemand = demand.Very_High;
            //marketDemandText.text = demand.Very_High.ToString().Replace('_', ' ');
            //marketDemandText.color = veryHighColor;
            //bidManager.UpdateSpawnTime(spawnTimeVeryHigh);
        }
    }

    /// <summary>
    /// Updates the necessary vrabiles for the market demand components
    /// </summary>
    /// <param name="newDemand">sets the demand type, ie medium, high, etc.</param>
    /// <param name="newDemandText">sets the market demand text to the demand type</param>
    /// <param name="newDemandColor">sets the new color of the demand text</param>
    /// <param name="newBidTime">sets the new time between bid spawns</param>
    private void SetDemandVarsAndText(demand newDemand, string newDemandText, Color newDemandColor, int newBidTime)
    {
        marketDemand = newDemand;
        marketDemandText.text = newDemandText;
        marketDemandText.color = newDemandColor;
        bidManager.UpdateSpawnTime(newBidTime);
    }
}