using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Class needed to inherit from IPointerEnterHandler and IPointerExitHandler from the 
/// UnityEngine.EventSystems namespace so it can manually track mouse pointer events
/// </summary>
public class EndOfLevelMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Player player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
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
}
