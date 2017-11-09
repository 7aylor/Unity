using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCard : MonoBehaviour
{
    private GameObject child;
    private Market market;

    void Start()
    {
        child = transform.GetChild(0).gameObject;
        market = FindObjectOfType<Market>();
    }

    public void Buy()
    {
        //check for adequate gold

        //check for an open slot in the respective hand

        //delete card from market

        if(gameObject.tag != "Market_Deck")
        {
            Destroy(child);
        }

        //move other cards to new positions
        market.ShiftCardsForwad();

        //draw new card from deck
    }


}
