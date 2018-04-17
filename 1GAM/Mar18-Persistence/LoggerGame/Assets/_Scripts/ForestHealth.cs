using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForestHealth : MonoBehaviour {

    private TMP_Text text;
    private int numSpaces;
    private MapGenerator map;
    private int forestHealth;

    public Color good;
    public Color medium;
    public Color bad;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        map = FindObjectOfType<MapGenerator>();
    }

    public void UpdateForestHealth()
    {
        numSpaces = GameManager.instance.numTiles - GameManager.instance.numRiverTiles - GameManager.instance.numObstacleTiles;
        forestHealth = Mathf.RoundToInt(((float)GameManager.instance.numTreesInPlay / numSpaces) * 100);
        Debug.Log("Tree count: " + GameManager.instance.numTreesInPlay + " Num Spaces: " + numSpaces + " Forest Health: " + forestHealth);
        text.text = forestHealth.ToString();
    }
}
