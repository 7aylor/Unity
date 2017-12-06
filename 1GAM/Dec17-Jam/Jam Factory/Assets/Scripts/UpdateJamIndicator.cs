using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UpdateJamIndicator : MonoBehaviour {

    public Image jamIndicator;
    private Image ourImage;

    private void Start()
    {
        ourImage = GetComponent<Image>();
    }

    /// <summary>
    /// Updates the image of the Jam Indicator
    /// </summary>
    public void UpdateIndicator()
    {
        jamIndicator.sprite = ourImage.sprite;
    }

}
