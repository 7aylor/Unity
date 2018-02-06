using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonSlider : MonoBehaviour {

    public Text CropsHarvested;
    public Text CropsInSeason;
    public Text CropsEaten;
    public Text TotalGoldEarned;
    public Text HighestEarningCrop;
    public Text SeasonSummaryText;
    public GameObject SeasonSummary;
    public GameSummary gameSummary;

    private Slider slider;
    private CanvasBackground background;
    private SeasonText seasonText;
    private Market market;
    private GoldManager gold;
    private HarvestController hc;
    private DeerMovement deer;
    private SeasonSymbol symbols;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
        background = GameObject.FindObjectOfType<CanvasBackground>();
        seasonText = GameObject.FindObjectOfType<SeasonText>();
        market = GameObject.FindObjectOfType<Market>();
        gold = GameObject.FindObjectOfType<GoldManager>();
        hc = GameObject.FindObjectOfType<HarvestController>();
        deer = hc.GetComponent<DeerMovement>();
        symbols = GameObject.FindObjectOfType<SeasonSymbol>();
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
        int goldEarned = gold.GetGoldAmount();
        //get the total gold harvest for this season
        gold.SetGoldAmount(goldEarned + hc.GetSeasonsHarvest());

        //check if we are in the final season
        if (seasonText.GetCurrentSeason() == "Fall")
        {
            gameSummary.gameObject.SetActive(true);
            gameSummary.CalculateGameSummary();
        }

        //triggers a season change. Lots to calculate

        //update the season summary stuff
        SeasonSummary.SetActive(true);
        SeasonSummaryText.text = seasonText.GetCurrentSeason() + " Summary";
        CropsHarvested.text = hc.GetNumberOfCropsHarvested().ToString();
        CropsEaten.text = deer.NumCropsEaten.ToString();
        //crops in season
        CropsInSeason.text = hc.CropsInSeason.ToString();

        TotalGoldEarned.text = hc.GetSeasonsHarvest().ToString();
        HighestEarningCrop.text = hc.GetHighestEarnedCrop() + " " + hc.GetHighestEarnedCropAmount();

        //update UI elements
        slider.value = 0;
        background.ChangeBackgroundImage();
        seasonText.changeSeasonText();
        symbols.UpdateSeasonSymbol();
        market.SpawnMarketCardImages();

    }
}