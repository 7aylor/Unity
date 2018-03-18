using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int[,] map;
    public int sizeX;
    public int sizeY;
    public bool playerSelected = false;
    public Player selectedPlayer;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void InstantiateMap(int x, int y)
    {
        map = new int[x, y];
    }
}
