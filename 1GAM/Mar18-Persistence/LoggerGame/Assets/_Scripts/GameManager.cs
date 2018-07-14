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
    public int numGrassTiles;
    public int numTiles;

    public bool lumberjackHired;
    public bool planterHired;

    public int[] rank;
    public List<Skill> skillLevels;

    public static GameManager instance;

    public int earlyPointsToNextLevel;
    public int midPointsToNextLevel;
    public int latePointsToNextLevel;

    public enum planter_UI_State { Tree, Stump, Other, None }
    public enum lumberjack_UI_State { Tree, Stump, Other, None }


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

        skillLevels = new List<Skill>() { new Skill("ChopSpeed", 1, "Lumberjack" ), new Skill("DigSpeed", 1, "Lumberjack"), new Skill("LumberjackJumpSpeed", 1, "Lumberjack"), new Skill("LumberjackStamina", 1, "Lumberjack"), new Skill("PlantSpeed", 1, "Planter"), new Skill("WaterSpeed", 1, "Planter"), new Skill("PlanterJumpSpeed", 1, "Planter"), new Skill("PlanterStamina", 1, "Planter")};

        rank = new int[]{ earlyPointsToNextLevel, earlyPointsToNextLevel, earlyPointsToNextLevel, earlyPointsToNextLevel, earlyPointsToNextLevel,
                          midPointsToNextLevel, midPointsToNextLevel, midPointsToNextLevel, midPointsToNextLevel, midPointsToNextLevel,
                          latePointsToNextLevel, latePointsToNextLevel, latePointsToNextLevel, latePointsToNextLevel, latePointsToNextLevel};
    }

    private void Start()
    {
        maxWorldSpaceX = ArrayCoordToWorldCoordX(sizeX);
        maxWorldSpaceY = ArrayCoordToWorldCoordX(sizeY);
        minWorldSpaceX = -maxWorldSpaceX;
        minWorldSpaceY = -maxWorldSpaceY;

        //Debug.Log("minWorldSpaceX" + minWorldSpaceX);
        //Debug.Log("minWorldSpaceY" + minWorldSpaceY);
        //Debug.Log("maxWorldSpaceX" + maxWorldSpaceX);
        //Debug.Log("maxWorldSpaceY" + maxWorldSpaceY);
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
        return (float)num - sizeY / 2 + 0.8f;
    }

    public int WorldCoordToArrayCoordX(float num)
    {
        return (int)(num + (sizeX / 2));
    }

    public int WorldCoordToArrayCoordY(float num)
    {
        return (int)(num + (sizeY / 2));
    }

    /// <summary>
    /// Checks if the world space coordinate is a river tile
    /// </summary>
    /// <param name="worldX">x position in world space</param>
    /// <param name="worldY">y position in world space</param>
    /// <returns>Return true if river tile, false if not river tile</returns>
    public bool IsRiverTile(float worldX, float worldY)
    {
        int x = GameManager.instance.WorldCoordToArrayCoordX(worldX);
        int y = GameManager.instance.WorldCoordToArrayCoordY(worldY);
        if (map[x,y] == (int)MapGenerator.tileType.startRiver ||
            map[x, y] == (int)MapGenerator.tileType.curveRiver ||
            map[x, y] == (int)MapGenerator.tileType.straightRiver ||
            map[x, y] == (int)MapGenerator.tileType.endRiver)
        {
            return true;
        }

        return false;    
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

