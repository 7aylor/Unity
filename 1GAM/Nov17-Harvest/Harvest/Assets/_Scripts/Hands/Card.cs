using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    private Hand hand;
    public bool isSelected = false; //

	// Use this for initialization
	void Start () {
        hand = gameObject.transform.parent.GetComponent<Hand>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Handles how to deal with a card being clicked from the Active Hand
    /// </summary>
    public void CardClicked()
    {
        //if we can select the card and the sprite is not the default, make the sprite color red, indicated we have selected it
        if(hand.CanSelectCard(gameObject) == true && isSelected == false && !GetComponent<Image>().sprite.name.Contains("Default"))
        {
            hand.CardSelected = true;
            isSelected = true;
            SetCanHighlightPlayspaceTiles(true);
            SetCanHighlightMarketTiles(false);
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            //if we can select the card and we've clicked it, remove the highlight and remove card being selected from hand
            if (hand.CanSelectCard(gameObject))
            {
                hand.CardSelected = false;
                isSelected = false;
                SetCanHighlightPlayspaceTiles(false);
                SetCanHighlightMarketTiles(true);
                GetComponent<Image>().color = Color.white;
            }
        }
    }

    /// <summary>
    /// Loops through all tiles in the playspace and sets the CanHighlight property
    /// </summary>
    /// <param name="canHighlight"></param>
    public void SetCanHighlightPlayspaceTiles(bool canHighlight)
    {
        foreach(PlayspaceTile tile in FindObjectsOfType<PlayspaceTile>())
        {
            tile.CanHighlight = canHighlight;
        }
    }

    /// <summary>
    /// Loops through all tiles in the market and sets the CanHighlight property
    /// </summary>
    /// <param name="canHighlight"></param>
    public void SetCanHighlightMarketTiles(bool canHighlight)
    {
        foreach (MarketTile tile in FindObjectsOfType<MarketTile>())
        {
            tile.CanHighlight = canHighlight;
        }
    }
}