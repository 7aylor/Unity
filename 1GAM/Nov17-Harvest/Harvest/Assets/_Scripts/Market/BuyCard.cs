﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCard : MonoBehaviour
{
    private int costOfDeckCard = 20;
    private GameObject child;
    private Market market;
    private Gold goldInBank;
    private Hand activeHand;
    private PassiveCardHandler passiveHand;
    private ActionPointManager apm;

    void Start()
    {
        child = transform.gameObject;
        market = FindObjectOfType<Market>();
        goldInBank = FindObjectOfType<Gold>();
        activeHand = GameObject.FindGameObjectWithTag("Hand_Active").GetComponent<Hand>();
        passiveHand = GameObject.FindGameObjectWithTag("Hand_Passive").GetComponent<PassiveCardHandler>();
        apm = GameObject.FindObjectOfType<ActionPointManager>();
    }

    /// <summary>
    /// Functionality to buy a card from the market
    /// </summary>
    /// <param name="costOfCard"></param>
    public void Buy(int costOfCard)
    {
        if(activeHand.CardSelected == false)
        {
            //check for adequate gold and Action points
            if (goldInBank.canBuy(costOfCard) && apm.GetActionPointsAvailable() > 0)
            {
                //make a copy of the gameobject and store it in newcard so that we can edit on the fly if needed
                GameObject newCard = gameObject;

                //deals with the deck card (face down card)
                if (costOfCard == costOfDeckCard)
                {
                    //ensure the new card isn't a passive that is in play
                    newCard = market.DealRandomCardUniquePassives();
                    passiveHand.HandleCard(newCard);
                    market.AssignMarketCardTag(newCard);
                    gameObject.tag = newCard.tag;
                }

                //check for an open slot in the respective hand
                if (gameObject.tag == "Card_Active")
                {
                    HandleHand("Hand_Active", costOfCard, newCard);
                }
                else if (gameObject.tag == "Card_Passive")
                {
                    HandleHand("Hand_Passive", costOfCard, newCard);
                }
            }
            else
            {
                //tell user they can't afford this card or don't have enough AP
                if (goldInBank.GetGoldAmount() < costOfCard)
                {
                    Console.instance.WriteToConsole("Not enough gold");
                }
                else if(ActionPointManager.instance.GetActionPointsAvailable() <= 0)
                {
                    Console.instance.WriteToConsole("Not enough AP");
                }
            }
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
            if (hand != null && hand.IsFull() == false)
            {
                hand.AddCardToHand(cardToAdd.GetComponent<Image>());
                passiveHand.HandleCard(cardToAdd);
                goldInBank.SetGoldAmount(goldInBank.GetGoldAmount() - costOfCard);
                apm.UseActionPoint();

                if (costOfCard != costOfDeckCard)
                {
                    market.ShiftCardsForward(gameObject);
                }
            }
            else if(hand.IsFull() == true)
            {
                //tell player the hand is full
                Console.instance.WriteToConsole(hand.name + " is full.");
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
