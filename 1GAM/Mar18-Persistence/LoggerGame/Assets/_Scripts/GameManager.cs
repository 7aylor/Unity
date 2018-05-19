using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int[,] map;
    public int sizeX;
    public int sizeY;
    public bool playerSelected = false;
    public Player selectedPlayer;

    public int forestHealth;
    public int money;
    public int lumber;
    public int demand;

    public int lumberInMarket;
    public int totalLumberHarvested;
    public int totalMoneyEarned;
    public int totalMoneySpent;
    public int totalNumberOfSales;
    public int timeInBusiness;

    public int numTreesInPlay;
    public int numRiverTiles;
    public int numObstacleTiles;
    public int numTiles;

    public bool lumberjackHired;
    public bool planterHired;

    public Dictionary<int, int> rank;
    public Dictionary<string, int> skillLevels;

    public static GameManager instance;

    public int earlyPointsToNextLevel;
    public int midPointsToNextLevel;
    public int latePointsToNextLevel;

    private void Awake()
    {
        instance = this;
        forestHealth = 100;
        money = 0;
        lumber = 0;
        demand = 0;

        lumberInMarket = 0;
        totalLumberHarvested = 0;
        totalMoneyEarned = 0;
        totalMoneySpent = 0;
        totalNumberOfSales = 0;
        timeInBusiness = 0;

        lumberjackHired = false;
        planterHired = false;
        rank = new Dictionary<int, int>() { { 1, earlyPointsToNextLevel }, { 2, earlyPointsToNextLevel }, { 3, earlyPointsToNextLevel}, { 4, earlyPointsToNextLevel }, { 5, earlyPointsToNextLevel },
                                            { 6, midPointsToNextLevel }, { 7, midPointsToNextLevel }, { 8, midPointsToNextLevel }, { 9, midPointsToNextLevel }, { 10, midPointsToNextLevel },
                                            { 11, latePointsToNextLevel }, { 12, latePointsToNextLevel }, { 13, latePointsToNextLevel }, { 14, latePointsToNextLevel }, { 15, latePointsToNextLevel }}; //adjust for testing
        skillLevels = new Dictionary<string, int>() { { "ChopSpeed", 1 }, { "DigSpeed", 1},{ "LumberjackJumpSpeed", 1 }, { "LumberjackStamina", 1 }, { "PlantSpeed", 1 }, { "WaterSpeed", 1 }, { "PlanterJumpSpeed", 1 }, { "PlanterStamina", 1 } };
    }

    public void InstantiateMap(int x, int y)
    {
        map = new int[x, y];
    }
}