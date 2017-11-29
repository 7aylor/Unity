using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour {

    private SeasonSlider slider;
    private GameObject passiveHand;
    private DeerMovement dm;

	//Use this for initialization
	void Start () {
        slider = GameObject.FindObjectOfType<SeasonSlider>();
        passiveHand = GameObject.FindGameObjectWithTag("Hand_Passive");
        dm = GameObject.FindObjectOfType<DeerMovement>();
	}

    /// <summary>
    /// Resets action points and season slider
    /// </summary>
    public void EndTurnAndReset()
    {
        //account for passives
        foreach(Transform obj in passiveHand.transform)
        {
            string spriteName = obj.GetComponent<Image>().sprite.name;
            if (!spriteName.Contains("Default"))
            {
                if(spriteName == "Bank" || spriteName == "Family")
                {
                    Debug.Log("Bank or Family");
                    GameObject.FindObjectOfType<PassiveCardHandler>().HandleCard(spriteName);
                }
            }
        }
        ActionPointManager.instance.ResetActionPoints();
        slider.IncreaseSeasonSlider();

        dm.MoveDeer();
    }
}
