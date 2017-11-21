using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour {

    private SeasonSlider slider;

	// Use this for initialization
	void Start () {
        slider = GameObject.FindObjectOfType<SeasonSlider>();
	}

    /// <summary>
    /// Resets action points and season slider
    /// </summary>
    public void EndTurnAndReset()
    {
        ActionPointManager.instance.ResetActionPoints();
        slider.IncreaseSeasonSlider();
    }
}
