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
    //private bool canHighlight = false;

    private void Start()
    {
        image = GetComponent<Image>();
        startColor = image.color;
        newColor = new Color32(197, 88, 83, 255);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(image.sprite != null)// && canHighlight == true)
        {
            image.color = newColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = startColor;
    }
}
