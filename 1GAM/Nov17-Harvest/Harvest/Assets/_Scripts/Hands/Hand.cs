using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hand : MonoBehaviour
{
    private int cardCount;
    private List<GameObject> cards = new List<GameObject>();
    public bool CardSelected { get; set; }
    private string selectedCard = "";

    // Use this for initialization
    void Start()
    {
        CardSelected = false;
        cardCount = 0;
        GetHandCardSlots();
    }

    // Update is called once per frame
    void Update()
    {

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

    public void AddCardToHand(GameObject newCard)
    {
        Debug.Log("Added Card to Hand");

        Image cardImage = newCard.GetComponent<Image>();
        cards[cardCount].GetComponent<Image>().sprite = cardImage.sprite;

        cardCount++;

        if(cardCount >= 5)
        {
            cardCount = 0;
        }
    }

    public bool CanSelectCard(GameObject obj)
    {
        string name = obj.GetComponent<Image>().sprite.name;

        if (CardSelected == false || name == selectedCard)
        {
            selectedCard = name;
            return true;
        }
        else if (name != selectedCard)
        {
            return false;
        }

        return false;
    }
    
}
