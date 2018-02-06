using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonSummary : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EnableSeasonSummary(false);
	}

    public void EnableSeasonSummary(bool enable)
    {
        gameObject.SetActive(enable);
    }
}
