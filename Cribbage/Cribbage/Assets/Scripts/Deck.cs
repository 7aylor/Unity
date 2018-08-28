using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {

    int numCards;
    List<Card> deck = new List<Card>();
    List<Card> usedDeck = new List<Card>();
    //Helpers.DeckType type;

    /// <summary>
    /// Default Constructor. 52 card deck.
    /// </summary>
    public Deck(int numCards)
    {
        this.numCards = numCards;
        Init();
        Shuffle();
        //Print();
    }

    /// <summary>
    /// Creates the deck of cards
    /// </summary>
    private void Init()
    {
        foreach(Helpers.Suit s in System.Enum.GetValues(typeof(Helpers.Suit)))
        {
            foreach (Helpers.CardType c in System.Enum.GetValues(typeof(Helpers.CardType)))
            {
                int cardValue = (int)c + 1;
                if(c == Helpers.CardType.Jack || c == Helpers.CardType.Queen || c == Helpers.CardType.King)
                {
                    cardValue = 10;
                }

                deck.Add(new Card(s, c, cardValue));
            }
        }
    }

    /// <summary>
    /// Prints all cards in the deck
    /// </summary>
    public void Print()
    {
        foreach(Card c in deck)
        {
            c.Print();
        }
    }

    /// <summary>
    /// Print size of deck and used deck
    /// </summary>
    public void PrintSize()
    {
        Debug.Log("Deck Size: " + deck.Count);
        Debug.Log("Used Deck Size: " + usedDeck.Count);
    }

    /// <summary>
    /// shuffles the deck
    /// </summary>
    public void Shuffle()
    {
        for(int i = 0; i < deck.Count; i++)
        {
            int swapPos = Random.Range(i, deck.Count);
            Card temp = deck[i];
            deck[i] = deck[swapPos];
            deck[swapPos] = temp;
        }
    }

    /// <summary>
    /// Deal to all present hands
    /// </summary>
    /// <param name="hands">List of hands in play</param>
    public void Deal(List<Hand> hands)
    {
        for(int i = 0; i < hands[0].NumCards; i++)
        {
            foreach(Hand h in hands)
            {
                h.AddCardToHand(DrawTopCard());
            }
        }
    }

    /// <summary>
    /// Draws the top card, removes it from the deck, adds it to used deck, and returns it
    /// </summary>
    /// <returns>Top card from the deck</returns>
    public Card DrawTopCard()
    {
        Card topCard = deck[deck.Count - 1];
        deck.RemoveAt(deck.Count - 1);
        usedDeck.Add(topCard);
        return topCard;
    }

    /// <summary>
    /// Removes all cards from used deck and places them in deck, then shuffles
    /// </summary>
    public void Reset()
    {
        foreach(Card c in usedDeck)
        {
            deck.Add(c);
        }

        usedDeck.Clear();
        Shuffle();
    }
}
