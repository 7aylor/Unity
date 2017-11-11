﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCard : MonoBehaviour
{
    private int costOfDeckCard = 20;
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
            GameObject newCard = gameObject;
            if (costOfCard == costOfDeckCard)
            {
                newCard = market.DealRandomCard();
                market.AssignMarketCardTag(newCard);
                gameObject.tag = newCard.tag;
            }

            //check for an open slot in the respective hand
            if (gameObject.tag == "Card_Active")
            {
                HandleHand("Hand_Active", costOfCard, newCard);
            }
            else if(gameObject.tag == "Card_Passive")
            {
                HandleHand("Hand_Passive", costOfCard, newCard);
            }
        }
        else
        {
            //tell user they can't afford this card
        }
    }

    /// <summary>
    /// Finds the hand based off of the tag, then adds to the respective hand. Updates gold amount and shifts market cards
    /// </summary>
    /// <param name="handTag">Tag of the hand you want to add card to</param>
    private void HandleHand(string handTag, int costOfCard, GameObject cardToAdd)
    {
        //find the hand with the given tag
        GameObject handObj = GameObject.FindGameObjectWithTag(handTag);

        if (handObj != null)
        {
            Hand hand = handObj.GetComponent<Hand>();

            //if the hand is not full, add card to hand, shift market cards, update gold
            if (hand != null)// && hand.IsFull() == false)
            {
                hand.AddCardToHand(cardToAdd);
                goldInBank.SetGoldAmount(goldInBank.GetGoldAmount() - costOfCard);

                if(costOfCard != costOfDeckCard)
                {
                    market.ShiftCardsForward(gameObject);
                }

            }
        }
        //if no tag is found, throw exception
        else
        {
            string exceptionString = String.Format("No {0} found. Ensure the {0} object has the proper tag", handTag);
            throw new System.Exception(exceptionString);
        }
    }

}
