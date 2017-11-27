using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonSlider : MonoBehaviour {

    private Slider slider;
    private CanvasBackground background;
    private SeasonText seasonText;
    private Market market;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
        background = GameObject.FindObjectOfType<CanvasBackground>();
        seasonText = GameObject.FindObjectOfType<SeasonText>();
        market = GameObject.FindObjectOfType<Market>();
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
        //triggers a season change. Lots to calculate
        slider.value = 0;
        background.ChangeBackgroundImage();
        seasonText.changeSeasonText();
        market.SpawnMarketCardImages();

        //trigger gold harvest
    }
}