using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ForestHealth : MonoBehaviour {

    private TMP_Text text;
    private int numSpaces;
    private MapGenerator map;
    private int forestHealth;
    private Image treeImage;
    private bool firstUpdate = true;
    private Demand demand;
    private StatsManager stats;

    public Color good;
    public Color medium;
    public Color bad;

    public Sprite goodTree;
    public Sprite mediumTree;
    public Sprite badTree;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        map = FindObjectOfType<MapGenerator>();
        treeImage = GetComponentInParent<Image>();
        demand = FindObjectOfType<Demand>();
        stats = FindObjectOfType<StatsManager>();
    }

    public void UpdateForestHealth()
    {
        numSpaces = GameManager.instance.numTiles - GameManager.instance.numRiverTiles - GameManager.instance.numObstacleTiles;
        forestHealth = Mathf.RoundToInt(((float)GameManager.instance.numTreesInPlay / numSpaces) * 100);
        Debug.Log("Tree count: " + GameManager.instance.numTreesInPlay + " Num Spaces: " + numSpaces + " Forest Health: " + forestHealth);
        text.text = forestHealth.ToString();

        if (forestHealth > 66)
        {
            text.color = good;
            treeImage.sprite = goodTree;
            IsFirstUpdate(100);
        }
        else if (forestHealth <= 66 && forestHealth > 33)
        {
            text.color = medium;
            treeImage.sprite = mediumTree;
            IsFirstUpdate(250);
        }
        else
        {
            text.color = bad;
            treeImage.sprite = badTree;
            IsFirstUpdate(500);
        }

        //GameManager.instance.lumberInMarket = 1000;
        //demand.UpdateDemand();
    }

    private void IsFirstUpdate(int lumberAmount)
    {
        Debug.Log("Outside FirstUpdate");
        if(firstUpdate == true)
        {
            Debug.Log("FirstUpdate Called");
            GameManager.instance.lumberInMarket = lumberAmount;
            stats.UpdateStats(StatsManager.stat.lumberInMarket);
            demand.UpdateDemand();
            firstUpdate = false;
        }
    }

    public int GetForestHealth()
    {
        return forestHealth;
    }
}
