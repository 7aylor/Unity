using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{

    public bool CanHighlight { get; set; }
    private Hand activeHand;

	// Use this for initialization
	void Start () {

        activeHand = GameObject.FindGameObjectWithTag("Hand_Active").GetComponent<Hand>();

        CanHighlight = false;
	}
	
    public void DeleteCard()
    {
        if (CanHighlight == true && activeHand != null && activeHand.selectedCard != "" 
            && ActionPointManager.instance.GetActionPointsAvailable() > 0)
        {
            activeHand.RemoveCardFromHand();
            ActionPointManager.instance.UseActionPoint();

        }
    }

}
