using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    Helpers.Suit suit;
    Helpers.CardType type;
    int value;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="suit"></param>
    /// <param name="type"></param>
    public Card(Helpers.Suit suit, Helpers.CardType type, int value)
    {
        this.suit = suit;
        this.type = type;
        this.value = value;
    }

    /// <summary>
    /// returns card suit
    /// </summary>
    public Helpers.Suit Suit
    {
        get
        {
            return suit;
        }
    }

    /// <summary>
    /// returns card type
    /// </summary>
    public Helpers.CardType Type
    {
        get
        {
            return type;
        }
    }

    /// <summary>
    /// returns card value
    /// </summary>
    public int Value
    {
        get
        {
            return value;
        }
    }

    /// <summary>
    /// Prints card type and suit
    /// </summary>
    public void Print()
    {
        Debug.Log(type + " of " + suit + " value " + value);
    }
}
