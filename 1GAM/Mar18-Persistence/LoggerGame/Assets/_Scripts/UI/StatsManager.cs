using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsManager : MonoBehaviour {

    public enum stat { lumberInMarket, totalLumberHarvested, totalMoneyEarned,
        totalMoneySpent, totalNumberOfSales, timeInBusiness }

    private TMP_Text lumberInMarketText;
    private TMP_Text totalLumberHarvestedText;
    private TMP_Text totalMoneyEarnedText;
    private TMP_Text totalMoneySpentText;
    private TMP_Text totalNumberOfSalesText;
    private TMP_Text timeInBusinessText;

    private void Awake()
    {
        lumberInMarketText = transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>();
        totalLumberHarvestedText = transform.GetChild(1).transform.GetChild(1).GetComponent<TMP_Text>();
        totalMoneyEarnedText = transform.GetChild(2).transform.GetChild(1).GetComponent<TMP_Text>();
        totalMoneySpentText = transform.GetChild(3).transform.GetChild(1).GetComponent<TMP_Text>();
        totalNumberOfSalesText = transform.GetChild(4).transform.GetChild(1).GetComponent<TMP_Text>();
        timeInBusinessText = transform.GetChild(5).transform.GetChild(1).GetComponent<TMP_Text>();
    }

    private void Start()
    {
        foreach (stat s in Enum.GetValues(typeof(stat)))
        {
            //Debug.Log("Called on " + s);
            UpdateStats(s);
        }
    }

    public void UpdateStats(stat @stat)
    {
        switch (@stat)
        {
            case stat.lumberInMarket:
                lumberInMarketText.text = GameManager.instance.lumberInMarket.ToString();
                break;
            case stat.totalLumberHarvested:
                totalLumberHarvestedText.text = GameManager.instance.totalLumberHarvested.ToString();
                break;
            case stat.totalMoneyEarned:
                totalMoneyEarnedText.text = GameManager.instance.totalMoneyEarned.ToString();
                break;
            case stat.totalMoneySpent:
                totalMoneySpentText.text = GameManager.instance.totalMoneySpent.ToString();
                break;
            case stat.totalNumberOfSales:
                totalNumberOfSalesText.text = GameManager.instance.totalNumberOfSales.ToString();
                break;
            case stat.timeInBusiness:
                timeInBusinessText.text = GameManager.instance.timeInBusiness.ToString() + " days";
                break;
        }
    }
}
