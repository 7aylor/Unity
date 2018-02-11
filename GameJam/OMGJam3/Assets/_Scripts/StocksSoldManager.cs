using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StocksSoldManager : MonoBehaviour {

    public Summaries summaries;
    public Difficulty difficulty;
    [SerializeField] public static int maxTransactionsPerDay { get; set; }
    DayManager dayManager;
    Text stocksSoldText;

    private void Awake()
    {
        stocksSoldText = GetComponent<Text>();
        dayManager = FindObjectOfType<DayManager>();
    }


    // Use this for initialization
    void Start () {
        maxTransactionsPerDay = difficulty.numRounds;
        UpdateStocksSoldText();
	}

    private void UpdateStocksSoldText()
    {
        stocksSoldText.text = "Stocks Sold: " + summaries.transactionsToday.ToString();
    }

    public void IncreaseStocksSold()
    {
        if(summaries.transactionsToday >= maxTransactionsPerDay)
        {
            dayManager.IncreaseDay();
        }
        else
        {
            UpdateStocksSoldText();
            summaries.totalTransactions++;
            summaries.transactionsToday++;
        }
    }
}
