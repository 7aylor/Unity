using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCard : MonoBehaviour
{
    private GameObject child;
    private Market market;
    private Gold goldInBank;

    void Start()
    {
        child = transform.gameObject;
        market = FindObjectOfType<Market>();
        goldInBank = FindObjectOfType<Gold>();
    }

    public void Buy(int costOfCard)
    {
        //check for adequate gold
        if (goldInBank.canBuy(costOfCard))
        {
            //check for an open slot in the respective hand

            //if the object we clicked is not the market deck
            if (gameObject.tag != "Market_Deck")
            {
                market.ShiftCardsForward(gameObject);
            }
            //if we do click on the market deck, spawn a random and assign it to the proper hand
            else
            {

            }

            //update the amount of gold in the bank after we buy a card
            goldInBank.SetGoldAmount(goldInBank.GetGoldAmount() - costOfCard);
        }
        else
        {
            //tell user they can't afford this card
        }
    }


}
