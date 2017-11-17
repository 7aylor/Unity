using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour {

    private ActionPointManager apm;
    private SeasonSlider slider;

	// Use this for initialization
	void Start () {
        apm = GameObject.FindObjectOfType<ActionPointManager>();
        slider = GameObject.FindObjectOfType<SeasonSlider>();
	}

    /// <summary>
    /// Resets action points and season slider
    /// </summary>
    public void EndTurnAndReset()
    {
        apm.ResetActionPoints();
        slider.IncreaseSeasonSlider();
    }
}
