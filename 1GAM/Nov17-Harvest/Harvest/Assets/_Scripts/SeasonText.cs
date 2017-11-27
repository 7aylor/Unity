using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonText : MonoBehaviour {

    private string[] seasons = { "Winter", "Spring", "Summer", "Fall" };
    private int currentSeasonIndex;
    private string currentSeason;
    private Text seasonText;

	// Use this for initialization
	void Start () {
        currentSeasonIndex = 0;
        seasonText = GetComponent<Text>();
        changeSeasonText();
	}
	
    /// <summary>
    /// Update the season text
    /// </summary>
    public void changeSeasonText()
    {
        if(currentSeasonIndex < seasons.Length)
        {
            currentSeason = seasons[currentSeasonIndex];
            seasonText.text = currentSeason;
            currentSeasonIndex++;
        }
        else
        {
            throw new System.Exception("season text out of bounds");
        }

    }

}
