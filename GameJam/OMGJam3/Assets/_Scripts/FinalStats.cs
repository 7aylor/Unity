using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FinalStats : MonoBehaviour
{

    public Summaries summaries;
    Text dailyStatsText;

    private void Awake()
    {
        dailyStatsText = GetComponent<Text>();
    }

    private void Start()
    {
        UpdateStatsText();
    }

    public void UpdateStatsText()
    {
        dailyStatsText.text = "Total Sales: " + summaries.totalSales + "\n" +
                              "Total Busts: " + summaries.totalBusts + "\n" +
                              "Total Transactions: " + summaries.totalTransactions + "\n" +
                              "Total Days Before Bankruptcy: " + summaries.totalDaysBeforeBankruptcy + "\n" +
                              "Highest Earnings: " + summaries.highestEarnings;

        summaries.profitsToday = 0;
        summaries.salesToday = 0;
        summaries.bustsToday = 0;
        summaries.transactionsToday = 0;
    }
}

