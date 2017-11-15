using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayspaceTile : MonoBehaviour {

    public bool CanHighlight { get; set; }
    private GameObject[] cards;
    private List<Image> images = new List<Image>();
    private Hand hand;

	// Use this for initialization
	void Start () {
        cards = GameObject.FindObjectOfType<Market>().cards;

        foreach(GameObject card in cards){
            images.Add(card.GetComponent<Image>());
        }

        hand = FindObjectOfType<Hand>();
        CanHighlight = false;
	}

    /// <summary>
    /// implements the ability to place a selected card from a hand into the playspace
    /// </summary>
    public void PlaceCard()
    {
        Debug.Log("Place Card called outside if " + CanHighlight + " " + hand.CardSelected + " " + hand.selectedCard);
        if (CanHighlight == true)//&& hand.CardSelected == true)
        {
            Debug.Log("Place Card called");
            //Check the tile image to make sure we can place a new tile here
            foreach(Image image in images)
            {
                if(image.sprite.name == hand.selectedCard)
                {
                    gameObject.GetComponent<Image>().sprite = image.sprite;
                }
            }
        }
    }
}
