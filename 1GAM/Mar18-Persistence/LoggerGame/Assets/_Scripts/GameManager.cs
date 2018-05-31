using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int[,] map;
    public int sizeX;
    public int sizeY;

    public float minWorldSpaceX;
    public float minWorldSpaceY;
    public float maxWorldSpaceX;
    public float maxWorldSpaceY;

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

    public int[] rank;
    public List<Skill> skillLevels;

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

        minWorldSpaceX = ArrayCoordToWorldCoordX(0);
        minWorldSpaceY = ArrayCoordToWorldCoordY(0);
        maxWorldSpaceX = ArrayCoordToWorldCoordX(sizeX);
        maxWorldSpaceY = ArrayCoordToWorldCoordX(sizeY);

        lumberjackHired = false;
        planterHired = false;

        skillLevels = new List<Skill>() { new Skill("ChopSpeed", 1, "Lumberjack" ), new Skill("DigSpeed", 1, "Lumberjack"), new Skill("LumberjackJumpSpeed", 1, "Lumberjack"), new Skill("LumberjackStamina", 1, "Lumberjack"), new Skill("PlantSpeed", 1, "Planter"), new Skill("WaterSpeed", 1, "Planter"), new Skill("PlanterJumpSpeed", 1, "Planter"), new Skill("PlanterStamina", 1, "Planter")};

        rank = new int[]{ earlyPointsToNextLevel, earlyPointsToNextLevel, earlyPointsToNextLevel, earlyPointsToNextLevel, earlyPointsToNextLevel,
                          midPointsToNextLevel, midPointsToNextLevel, midPointsToNextLevel, midPointsToNextLevel, midPointsToNextLevel,
                          latePointsToNextLevel, latePointsToNextLevel, latePointsToNextLevel, latePointsToNextLevel, latePointsToNextLevel};
    }

    public void InstantiateMap(int x, int y)
    {
        map = new int[x, y];
    }

    public float ArrayCoordToWorldCoordX(float num)
    {
        return (float)num - sizeX / 2;
    }

    public float ArrayCoordToWorldCoordY(float num)
    {
        return (float)num - sizeX / 2 + 0.8f;
    }
}

public class Skill
{
    public string name;
    public int level;
    public string associatedPlayer;

    public Skill(string newName, int newLevel, string newPlayer)
    {
        name = newName;
        level = newLevel;
        associatedPlayer = newPlayer;
    }
}

