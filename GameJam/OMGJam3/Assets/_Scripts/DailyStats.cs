using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DailyStats : MonoBehaviour {

    public Summaries summaries;
    Text dailyStatsText;

    private void Awake()
    {
        dailyStatsText = GetComponent<Text>();
    }

    public void UpdateStatsText()
    {
        dailyStatsText.text = "Today's Sales: " + summaries.salesToday + "\n" +
                              "Today's Busts: " + summaries.bustsToday + "\n" +
                              "Today's Transactions: " + summaries.transactionsToday + "\n" +
                              "Today's Profits: " + FormatMoney(summaries.profitsToday);

        summaries.profitsToday = 0;
        summaries.salesToday = 0;
        summaries.bustsToday = 0;
        summaries.transactionsToday = 0;
    }

    private string FormatMoney(int amount)
    {
        if(amount >= 0)
        {
            Debug.Log("positive");
            return "$" + amount.ToString();
        }
        else
        {
            Debug.Log("negative");
            return "-$" + amount.ToString().TrimStart('-');
        }
    }
}
