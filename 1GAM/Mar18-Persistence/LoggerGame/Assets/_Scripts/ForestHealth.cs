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

    void Start () {
        text.text = GameManager.instance.forestHealth.ToString();
        numSpaces = map.numTiles - map.riverCount - map.obstacleCount;

        forestHealth = map.treeCount / numSpaces;
        Debug.Log("Forest Health: " + forestHealth);
	}
}
