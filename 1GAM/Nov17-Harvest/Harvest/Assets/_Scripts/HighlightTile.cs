using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color startColor;
    private Image image;
    private Color32 newColor;

    private void Start()
    {
        image = GetComponent<Image>();
        startColor = image.color;
        newColor = new Color32(197, 88, 83, 255);
    }

    /// <summary>
    /// Highlights tile when mouse hovers over it, if applicable
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //if there is a sprite on the object and it's not a default image
        if (image.sprite != null && !image.sprite.name.Contains("Default") && ActionPointManager.instance.GetActionPointsAvailable() > 0)
        {
            Hand hand = gameObject.transform.parent.GetComponent<Hand>();
            PlayspaceTile tile = gameObject.GetComponent<PlayspaceTile>();
            MarketTile marketCard = gameObject.GetComponent<MarketTile>();

            //if the object is part of a hand and there is a card selected, don't highlight
            if (hand != null && (hand.CardSelected == true))
            {
                return;
            }
            else if(hand == null)
            {
                if (marketCard != null && marketCard.CanHighlight == false)
                {
                    return;
                }
                if (tile != null && tile.CanHighlight == false)
                {
                    return;
                }
            }

            image.color = newColor;
        }
    }

    /// <summary>
    /// Removes highlight after mouse cursor stops hovering
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        Card thisCard = GetComponent<Card>();

        //if the object is a card and it is selected, don't remove highlight
        if(thisCard != null)
        {
            if (thisCard.isSelected)
            {
                return;
            }

        }

        image.color = startColor;
    }
}
