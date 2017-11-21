using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayspaceTile : MonoBehaviour {

    public bool CanHighlight { get; set; }
    private Image tileImage;
    private GameObject[] marketCards;
    private List<Image> possibleActiveImages = new List<Image>();
    private Hand activeHand;
    private ActionPointManager apm;

	// Use this for initialization
	void Start () {
        marketCards = GameObject.FindObjectOfType<Market>().cards;

        //finds the active in slot 1 of the game objects
        activeHand = GameObject.FindObjectsOfType<Hand>()[1];
        tileImage = GetComponent<Image>();
        foreach(GameObject card in marketCards){
            possibleActiveImages.Add(card.GetComponent<Image>());
        }

        apm = GameObject.FindObjectOfType<ActionPointManager>();

        CanHighlight = false;
	}

    /// <summary>
    /// implements the ability to place a selected card from a hand into the playspace
    /// </summary>
    public void PlaceCard()
    {
        if (CanHighlight == true && CanPlaceCardHere() == true && apm.GetActionPointsAvailable() > 0)
        {
            //finds the image we want to place on the selected tile
            foreach(Image image in possibleActiveImages)
            {
                //places the image on the tile
                if(image.sprite.name == activeHand.selectedCard)
                {
                    gameObject.GetComponent<Image>().sprite = image.sprite;
                    activeHand.RemoveCardFromHand();
                    apm.UseActionPoint();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// checks if we can place a card on this tile
    /// </summary>
    /// <returns></returns>
    private bool CanPlaceCardHere()
    {
        if(tileImage.sprite.name != "Grass")
        {
            return false;
        }

        return true;
    }
}
