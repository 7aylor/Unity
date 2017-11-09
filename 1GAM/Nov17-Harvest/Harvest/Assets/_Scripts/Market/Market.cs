using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour {

    public GameObject[] cards;

	// Use this for initialization
	void Start () {
		
	}
	
    /// <summary>
    /// Gets a random card to deal
    /// </summary>
    /// <returns></returns>
    public GameObject DealRandomCard()
    {
        return cards[Random.Range(0, cards.Length - 1)];
    }

    /// <summary>
    /// Shifts the cards up dependings which card was just removed
    /// </summary>
    public void ShiftCardsForwad()
    {
        int counter = 0;
        int shiftIndex = 0;

        //finds the index of the card that was just removed
        foreach(Transform child in transform)
        {
            if(child.childCount == 0)
            {
                shiftIndex = counter;
                break;
            }
            counter++;
        }

        //deals with the cards of the card being in the free spot
        if(shiftIndex == 0)
        {
            Instantiate(transform.GetChild(1).GetChild(0), transform.GetChild(0));
            //Destroy(transform.GetChild(1));
            //Instantiate(transform.GetChild(2).GetChild(0), transform.GetChild(1));
            //Destroy(transform.GetChild(2));
            //Instantiate(DealRandomCard(), transform.GetChild(2));

            //transform.
        }
    }

}
