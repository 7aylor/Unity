using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPointManager : MonoBehaviour {

    private int actionPointsAvailable;
    private int startActionPoints;
    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        startActionPoints = 2;
        actionPointsAvailable = startActionPoints;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Ensures we have enough action points and removes one
    /// </summary>
    public void UseActionPoint()
    {
        if(actionPointsAvailable > 0)
        {
            actionPointsAvailable--;
            text.text = actionPointsAvailable.ToString();
        }
    }

    /// <summary>
    /// retuns the amount of available action points
    /// </summary>
    /// <returns>Available Action Points</returns>
    public int GetActionPointsAvailable()
    {
        return actionPointsAvailable;
    }

    /// <summary>
    /// Resets the action points available to the start amount and updates the text
    /// </summary>
    public void ResetActionPoints()
    {
        actionPointsAvailable = startActionPoints;
        text.text = actionPointsAvailable.ToString();
    }

}
