using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck {

    int numCards;
    List<Card> cards = new List<Card>();
    Helpers.DeckType type;

    /// <summary>
    /// Default Constructor. 52 card deck.
    /// </summary>
    public Deck()
    {
        numCards = 52;
        Init();
        Shuffle();
        Print();
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
                cards.Add(new Card(s, c));
            }
        }
    }

    /// <summary>
    /// Prints all cards in the deck
    /// </summary>
    private void Print()
    {
        foreach(Card c in cards)
        {
            c.Print();
        }
    }

    /// <summary>
    /// shuffles the deck
    /// </summary>
    public void Shuffle()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            int swapPos = Random.Range(i, cards.Count);
            Card temp = cards[i];
            cards[i] = cards[swapPos];
            cards[swapPos] = temp;
        }
    }

}
