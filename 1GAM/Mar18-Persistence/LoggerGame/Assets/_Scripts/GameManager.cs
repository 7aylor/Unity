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

    public int numTreesInPlay;
    public int numRiverTiles;
    public int numObstacleTiles;
    public int numTiles;

    public bool lumberjackHired;
    public bool planterHired;

    public Dictionary<int, int> rank;
    public Dictionary<string, int> skillLevels;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        forestHealth = 100;
        money = 0;
        lumber = 0;
        demand = 0;
        lumberjackHired = false;
        planterHired = false;
        rank = new Dictionary<int, int>() { { 1, 1 }, { 2, 1 }, { 3, 1}, { 4, 1 }, { 5, 30 } }; //adjust for testing
        skillLevels = new Dictionary<string, int>() { { "ChopSpeed", 1 }, { "LumberjackJumpSpeed", 1 }, { "LumberjackStamina", 1 }, { "PlantSpeed", 1 }, { "WaterSpeed", 1 }, { "PlanterJumpSpeed", 1 }, { "PlanterStamina", 1 } };
    }

    public void InstantiateMap(int x, int y)
    {
        map = new int[x, y];
    }
}