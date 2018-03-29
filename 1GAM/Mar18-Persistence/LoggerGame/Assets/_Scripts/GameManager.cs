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

    public Dictionary<string, bool> activeActionButtons;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        forestHealth = 100;
        money = 1000;
        lumber = 0;
        demand = 0;
        activeActionButtons = new Dictionary<string, bool>();
    }

    public void InstantiateMap(int x, int y)
    {
        map = new int[x, y];
    }

    private void InitializeActionButtonsDict()
    {
        activeActionButtons.Add("Lumberjack_Levelup", false);
        activeActionButtons.Add("Lumberjack_Chop", false);
        activeActionButtons.Add("Planter_Levelup", false);
        activeActionButtons.Add("Planter_Water", false);
        activeActionButtons.Add("Planter_Plant", false);
        activeActionButtons.Add("Hire_Planter", false);
        activeActionButtons.Add("Hire_Lumberjack", false);
    }
}