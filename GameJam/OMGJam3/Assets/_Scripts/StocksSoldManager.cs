using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StocksSoldManager : MonoBehaviour {

    Text stocksSoldText;
    public static int stocksSold { get; set; }
    public static int maxTransactionsPerDay { get; set; }
    DayManager dayManager;

    private void Awake()
    {
        stocksSoldText = GetComponent<Text>();
        dayManager = FindObjectOfType<DayManager>();
    }


    // Use this for initialization
    void Start () {
        stocksSold = 0;
        maxTransactionsPerDay = 3;
        UpdateStocksSoldText();
	}

    private void UpdateStocksSoldText()
    {
        stocksSoldText.text = "Stocks Sold: " + stocksSold.ToString();
    }

    public void IncreaseStocksSold()
    {
        stocksSold++;
        if(stocksSold >= maxTransactionsPerDay)
        {
            stocksSold = 0;
            dayManager.IncreaseDay();
        }
        UpdateStocksSoldText();
    }
}
