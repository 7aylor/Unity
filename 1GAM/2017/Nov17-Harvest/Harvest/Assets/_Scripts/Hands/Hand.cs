using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hand : MonoBehaviour
{
    private int cardCount;
    private List<GameObject> cards = new List<GameObject>();
    private ActionPointManager apm;
    public bool CardSelected { get; set; }
    public string selectedCard;
    public GameObject defaultImage;

    // Use this for initialization
    void Start()
    {
        apm = GameObject.FindObjectOfType<ActionPointManager>();
        selectedCard = "";
        CardSelected = false;
        cardCount = 0;
        GetHandCardSlots();
    }

    /// <summary>
    /// Checks if the hand is full
    /// </summary>
    /// <returns></returns>
    public bool IsFull()
    {
        return cardCount >= 5;
    }

    /// <summary>
    /// Gets the slots in the hand and adds them to the card list
    /// </summary>
    private void GetHandCardSlots()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            cards.Add(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// deals with changing the image of the hand's card to the clicked market card's image 
    /// </summary>
    /// <param name="newCard"></param>
    public void AddCardToHand(Image cardImage)
    {
        cards[cardCount].GetComponent<Image>().sprite = cardImage.sprite;

        cardCount++;

        Debug.Log("Card Count: " + cardCount);

        //if(cardCount >= 5)
        //{
        //    cardCount = 0;
        //}
    }

    /// <summary>
    /// Removes an item from the hand and deselects it as well.
    /// </summary>
    public void RemoveCardFromHand()
    {
        foreach(GameObject card in cards)
        {
            Image cardImage = card.GetComponent<Image>();
            if (cardImage.sprite.name == selectedCard)
            {
                Card objCard = card.GetComponent<Card>();
                objCard.CardClicked();

                int index = cards.IndexOf(card);

                objCard.isSelected = false;
                CardSelected = false;
                selectedCard = "";
                cardCount--;
                Debug.Log("Card Count: " + cardCount);

                //loop from the card that is selected to the end and shift the images to the left one space
                if (index <= 4)
                {
                    for(int i = index; i < cards.Count - 1; i++)
                    {
                        cards[i].GetComponent<Image>().sprite = cards[i + 1].GetComponent<Image>().sprite;
                    }
                    //sets the last card tile to the default Active Hand image
                    cards[cards.Count - 1].GetComponent<Image>().sprite = defaultImage.GetComponent<Image>().sprite;
                }

                break;
            }
        }
    }

    /// <summary>
    /// determines if we can select a card from the hand
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool CanSelectCard(GameObject obj)
    {
        string name = obj.GetComponent<Image>().sprite.name;

        if(apm.GetActionPointsAvailable() > 0)
        {
            if (CardSelected == false || name == selectedCard)
            {
                Debug.Log("Can select card returns true, selected card name is: " + selectedCard);
                selectedCard = name;
                return true;
            }
            else if (name != selectedCard)
            {
                Debug.Log("Can select card returns false");
                selectedCard = "";
                return false;
            }
        }
        
        return false;
    }
    
}
