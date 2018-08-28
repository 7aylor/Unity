using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand {
    string name;
    int numCards;
    int score;
    List<Card> hand = new List<Card>();

    /// <summary>
    /// Default hand constructor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="numCards"></param>
    public Hand(string name, int numCards)
    {
        this.name = name;
        this.numCards = numCards;
        score = 0;
    }

    /// <summary>
    /// returns name of hand
    /// </summary>
    public string Name
    {
        get
        {
            return name;
        }
    }

    /// <summary>
    /// return number of cards in hand
    /// </summary>
    public int NumCards
    {
        get
        {
            return numCards;
        }
    }

    /// <summary>
    /// Returns Score and Sets Score
    /// </summary>
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    public void PlayCard()
    {
        
    }

    /// <summary>
    /// Adds a card to hand
    /// </summary>
    /// <param name="c">Card to add to hand</param>
    public void AddCardToHand(Card c)
    {
        hand.Add(c);
    }

    /// <summary>
    /// Prints the hand and sorts it
    /// </summary>
    public void Print()
    {
        Sort();
        string cardString = name + " hand: ";
        foreach(Card c in hand)
        {
            cardString += c.Type + " " + c.Suit + " ";
        }

        Debug.Log(cardString);
    }

    /// <summary>
    /// Sort hand by card type
    /// </summary>
    private void Sort()
    {
        hand.Sort((a,b) => a.Type.CompareTo(b.Type));
    }
}
