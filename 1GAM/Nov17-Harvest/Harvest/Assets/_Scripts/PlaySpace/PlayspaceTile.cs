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
    private ActiveCardHandler ach;

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
        ach = GameObject.FindObjectOfType<ActiveCardHandler>();

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
                    activeHand.RemoveCardFromHand();
                    ach.HandleCard(image.sprite.name);
                    if(image.sprite.name != "Bulldozer")
                    {
                        gameObject.GetComponent<Image>().sprite = image.sprite;
                    }
                    else
                    {
                        gameObject.GetComponent<Image>().sprite = GameObject.Find("Dirt").GetComponent<Image>().sprite;
                    }
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
        string name = tileImage.sprite.name;

        Debug.Log("Selected Card: " + activeHand.selectedCard);
        Debug.Log("Image name: " + name);

        if (activeHand.selectedCard == "Bulldozer" && name != "House" && name != "Lake" && name != "Grass")
        {
            return true;
        }
        else if (name != "Grass" && name != "Dirt" && activeHand.selectedCard != "Bulldozer")
        {
            return false;
        }
        else if((name == "Grass" || name == "Dirt") && activeHand.selectedCard != "Bulldozer")
        {
            return true;
        }
        else
        {
            Debug.Log("Else called");
            return false;
        }
    }
}
