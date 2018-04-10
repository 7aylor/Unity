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

    public bool lumberjackHired;
    public bool planterHired;


    public Dictionary<int, int> rank;

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
        rank = new Dictionary<int, int>() { { 1, 2 }, { 2, 3 }, { 3, 15 }, { 4, 20 }, { 5, 30 } }; //adjust for testing
    }

    public void InstantiateMap(int x, int y)
    {
        map = new int[x, y];
    }
}