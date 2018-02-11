using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSummaryData : MonoBehaviour {

    public Summaries summaries;

    private void Awake()
    {
        summaries.bustsToday = 0;
        summaries.highestEarnings = 0;
        summaries.profitsToday = 0;
        summaries.salesToday = 0;
        summaries.totalBusts = 0;
        summaries.totalDaysBeforeBankruptcy = 1;
        summaries.totalSales = 0;
        summaries.totalTransactions = 0;
        summaries.transactionsToday = 0;
    }
}
