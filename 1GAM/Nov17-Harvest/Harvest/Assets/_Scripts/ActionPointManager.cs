using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPointManager : MonoBehaviour {

    private int actionPointsAvailable;
    private int startActionPoints;
    private Text text;
    private EndTurn endTurnButton;
    private Color32 highlightColor = new Color32(255, 146, 146, 255);

    //singleton
    public static ActionPointManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(instance);
    }

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        startActionPoints = 2;
        actionPointsAvailable = startActionPoints;
        endTurnButton = GameObject.FindObjectOfType<EndTurn>();
        endTurnButton.GetComponent<Button>().enabled = false;
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
            if(actionPointsAvailable == 0)
            {
                SetEndTurnButton(true, Color.red, highlightColor);
            }
            else
            {
                SetEndTurnButton(false, Color.white, Color.white);
            }
        }
    }

    private void SetEndTurnButton(bool enabled, Color color, Color highlight)
    {
        endTurnButton.GetComponent<Button>().enabled = enabled;
        ColorBlock c = endTurnButton.GetComponent<Button>().colors;
        c.normalColor = color;
        c.highlightedColor = highlight;
        endTurnButton.GetComponent<Button>().colors = c;
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
        SetEndTurnButton(false, Color.white, Color.white);
    }

}
