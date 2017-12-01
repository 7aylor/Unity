using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSummary : MonoBehaviour {

    public Text success;
    public Text gameSummary;
    
    private HarvestController hc;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        hc = GameObject.FindObjectOfType<HarvestController>();
	}
	
    public void CalculateGameSummary()
    {
        //get total gold earned
        int totalGoldEarned = hc.TotalGoldEarned;

        if(totalGoldEarned >= GameManager.instance.GoldGoal)
        {
            success.text = "SUCCESS";
            gameSummary.text = "Congratulations! Your farm earned " + totalGoldEarned + " gold! May next harvest be even more plentiful!";
        }
        else
        {
            success.text = "FAILED";
            success.color = Color.red;
            gameSummary.text = "The deer has devoured your livelihood and you only earned " + totalGoldEarned + " gold. Your family will be penniless for generations...";
        }
    }
}
