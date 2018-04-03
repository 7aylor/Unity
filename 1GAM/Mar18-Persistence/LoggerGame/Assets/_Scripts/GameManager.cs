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

    //These are used for promoting
    public float lumberjackChopSpeed; //animator speed
    public float lumberjackJumpSpeed; //animator and translate speeds
    public float planterPlantSpeed; //animator speed
    public float planterWaterSpeed; //animator speed
    public float planterJumpSpeed; //animator and translate speeds

    public Dictionary<int, int> rank;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        forestHealth = 100;
        money = 1000;
        lumber = 0;
        demand = 0;
        lumberjackChopSpeed = 1;
        lumberjackJumpSpeed = 1;
        planterPlantSpeed = 1;
        planterWaterSpeed = 1;
        planterJumpSpeed = 1;
        lumberjackHired = false;
        planterHired = false;
        rank = new Dictionary<int, int>() { { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 6 }, { 5, 10 } };
    }

    public void InstantiateMap(int x, int y)
    {
        map = new int[x, y];
    }
}