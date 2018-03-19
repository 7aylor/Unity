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
    public int dollars;
    public int lumber;
    public int demand;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        forestHealth = 100;
        dollars = 1000;
        lumber = 0;
        demand = 0;
    }

    public void InstantiateMap(int x, int y)
    {
        map = new int[x, y];
        

    }
}
