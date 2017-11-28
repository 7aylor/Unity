using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonSlider : MonoBehaviour {

    private Slider slider;
    private CanvasBackground background;
    private SeasonText seasonText;
    private Market market;
    private GoldManager gold;
    private HarvestController hc;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
        background = GameObject.FindObjectOfType<CanvasBackground>();
        seasonText = GameObject.FindObjectOfType<SeasonText>();
        market = GameObject.FindObjectOfType<Market>();
        gold = GameObject.FindObjectOfType<GoldManager>();
        hc = GameObject.FindObjectOfType<HarvestController>();
	}

    /// <summary>
    /// Increases the season slider and calculates if the slider needs to be reset
    /// </summary>
    public void IncreaseSeasonSlider()
    {
        if(slider.value < slider.maxValue)
        {
            slider.value++;
        }
        else
        {
            ResetSeasonSlider();
        }
    }

    /// <summary>
    /// Resets the season slider and triggers the season change
    /// </summary>
    private void ResetSeasonSlider()
    {
        //get the total gold harvest for this season
        gold.SetGoldAmount(gold.GetGoldAmount() + hc.GetSeasonsHarvest());

        //check if we are in the final season
        if (seasonText.GetCurrentSeason() == "Fall")
        {
            //End of the game
        }

        //triggers a season change. Lots to calculate
        slider.value = 0;
        background.ChangeBackgroundImage();
        seasonText.changeSeasonText();
        market.SpawnMarketCardImages();

    }
}