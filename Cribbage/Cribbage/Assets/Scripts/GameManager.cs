using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    Deck deck;
    Hand player1;
    Hand player2;
    Hand player3;

    // Use this for initialization
    void Start () {
        deck = new Deck(52);
        player1 = new Hand("Player 1", 6);
        player2 = new Hand("Player 2", 6);

        deck.Deal(new List<Hand>() { player1, player2 });
        player1.Print();
        player2.Print();
        deck.PrintSize();
        deck.Reset();
        deck.PrintSize();
    }
}
