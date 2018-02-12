using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Stock : MonoBehaviour {

    public static string Name { get; set; }
    public static int Price { get; set; }
    Text stockNameText;
    StocksSoldManager stocksSold;

	// Use this for initialization
	void Awake () {
        stockNameText = GetComponent<Text>();
        stocksSold = FindObjectOfType<StocksSoldManager>();
	}
    private void Start()
    {
        stockNameText.text = "";
    }

    /// <summary>
    /// Randomly returns a stock name with 3 english letters
    /// </summary>
    /// <returns></returns>
    public void NewStock()
    {
        string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string stockName = "";

        for(int i = 0; i < 3; i++)
        {
            stockName += possibleChars[Random.Range(0, possibleChars.Length)];
        }

        Name = stockName;
        Price = GetRandomPrice();
        UpdateStockText();
    }

    private void UpdateStockText()
    {
        stockNameText.text = Name;
    }

    private int GetRandomPrice()
    {
        return Random.Range(100, 1001);
    }

    /// <summary>
    /// Resets the stock name and updates text, increase the stocksoldnum. Called from TimeManager
    /// </summary>
    public void StartNewRound()
    {
        NewStock();
        stocksSold.IncreaseStocksSold();
    }
}
