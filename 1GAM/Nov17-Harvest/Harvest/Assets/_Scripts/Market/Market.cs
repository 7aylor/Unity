using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour {

    public GameObject[] cards;
    private List<GameObject> marketCards = new List<GameObject>();
    private GameObject passiveHand;

    // Use this for initialization
    void Start () {
        GetMarketCardSlots();
        SpawnMarketCardImages();
        passiveHand = GameObject.FindGameObjectWithTag("Hand_Passive").gameObject;
        Debug.Log(passiveHand.name);
    }

    /// <summary>
    /// Loops through all Market slots and spawns Random card images, then sets the proper tag on the gameObject
    /// </summary>
    private void SpawnMarketCardImages()
    {
        foreach(GameObject card in marketCards)
        {
            card.GetComponent<Image>().sprite = DealRandomCard().GetComponent<Image>().sprite;
            AssignMarketCardTag(card);
        }
    }

    /// <summary>
    /// Gets a random card to deal
    /// </summary>
    /// <returns>Returns a random gameobject from the Passive/Active Card prefabs</returns>
    private GameObject DealRandomCard()
    {
        return cards[Random.Range(0, cards.Length)];
    }

    /// <summary>
    /// checks to see if a given passive card is in play already
    /// </summary>
    /// <param name="cardName"></param>
    /// <returns></returns>
    public bool passiveCardInPlay(string cardName)
    {
        //loop through the passive hand to check if this card is play
        foreach(Transform card in passiveHand.transform)
        {
            if(card.GetComponent<Image>().name == cardName)
            {
                return true;
            }
        }

        //loop through the market and check if this card is in the market
        foreach(Transform marketCard in transform)
        {
            if(marketCard.GetComponent<Image>().name == cardName)
            {
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Shifts the cards up dependings which card was just removed
    /// </summary>
    public void ShiftCardsForward(GameObject clickedObj)
    {
        //counts the current index we are checking against
        int counter = 0;

        //the index of the market card we just clicked, will be found next
        int shiftIndex = 0;

        //finds the index of the market card that was just removed
        foreach (GameObject card in marketCards)
        {
            if (card == clickedObj)
            {
                shiftIndex = counter;
                break;
            }
            counter++;
        }

        //moves the cards forward based off of their position in the market, then sets the tag based off of the card type
        while (counter < marketCards.Count - 1)
        {
            marketCards[counter].transform.GetComponent<Image>().sprite = marketCards[counter + 1].transform.GetComponent<Image>().sprite;
            AssignMarketCardTag(marketCards[counter]);
            counter++;
        }

        //if they are the last position in the market, spawn a random card *****May need to adjust based off of season and card number limits*****
        GameObject newCard = DealRandomCardUniquePassives();

        marketCards[2].transform.GetComponent<Image>().sprite = newCard.GetComponent<Image>().sprite;

        //set tag of the last card based off of card type
        AssignMarketCardTag(marketCards[counter]);
    }

    public GameObject DealRandomCardUniquePassives()
    {
        GameObject newCard = DealRandomCard();
        while (passiveCardInPlay(newCard.GetComponent<Image>().name) == true)
        {
            Debug.Log("re-roll passive card");
            newCard = DealRandomCard();
        }

        return newCard;
    }

    /// <summary>
    /// Gets the market cards and stores them in a list, not counting the deck
    /// </summary>
    private void GetMarketCardSlots()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            marketCards.Add(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Checks the image sprite name on the card object and sets the proper tag
    /// </summary>
    /// <param name="card"></param>
    public void AssignMarketCardTag(GameObject card)
    {
        Image cardImage = card.GetComponent<Image>();

        if (cardImage != null)
        {
            if (HandType.ActiveHandType.Contains(cardImage.sprite.name))
            {
                cardImage.tag = "Card_Active";
            }
            else if (HandType.PassiveHandType.Contains(cardImage.sprite.name))
            {
                cardImage.tag = "Card_Passive";
            }
        }
    }

}
