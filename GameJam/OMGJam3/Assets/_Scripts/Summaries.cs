﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Summary", menuName ="Summary")]
public class Summaries : ScriptableObject {

    public int highestEarnings = 0;
    public int profitsToday = 0;
    public int salesToday = 0;
    public int bustsToday = 0;
    public int transactionsToday = 0;
    public int totalDaysBeforeBankruptcy = 1;
    public int totalTransactions = 0;
    public int totalSales = 0;
    public int totalBusts = 0;
}
