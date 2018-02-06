using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// Class needed to inherit from IPointerEnterHandler and IPointerExitHandler from the 
/// UnityEngine.EventSystems namespace so it can manually track mouse pointer events
/// </summary>
public class EndOfLevelMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Player player;
    private Text timerText;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
        
        foreach(Transform child in transform)
        {
            if(child.gameObject.name == "Time Message")
            {
                timerText = child.gameObject.GetComponent<Text>();
            }
        }

        EnableMenu(false);
	}
	
    /// <summary>
    /// Allows outside access to activate and deactivate gameObject
    /// </summary>
    /// <param name="enabled"></param>
    public void EnableMenu(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    /// <summary>
    /// detects if the mouse has started hovering over the gameObject
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        player.canTurn = false;
    }

    /// <summary>
    /// detects if the mouse has stopped hovering over the gameObject
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        player.canTurn = true;
    }

    /// <summary>
    /// Updates the time text to reflect the amount of time it took to beat the level
    /// </summary>
    /// <param name="time"></param>
    public void UpdateWinMenuTimerText(int time)
    {
        if(timerText != null)
        {
            timerText.text = "You beat this level in " + time.ToString() + " seconds!";
        }
        else
        {
            timerText.text = "";
        }
    }
}
