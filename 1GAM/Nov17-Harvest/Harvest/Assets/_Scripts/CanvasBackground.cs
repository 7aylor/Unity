using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBackground : MonoBehaviour {

    public GameObject[] seasonBackgrounds;
    private int imageBackgroundIndex;
    private Image currentBackgroundImage;

	// Use this for initialization
	void Start () {
        imageBackgroundIndex = 0;
        currentBackgroundImage = GetComponent<Image>();
        ChangeBackgroundImage();
	}

    /// <summary>
    /// Update the background image to the next in the array;
    /// </summary>
    public void ChangeBackgroundImage()
    {
        if(imageBackgroundIndex < seasonBackgrounds.Length)
        {
            currentBackgroundImage.sprite = seasonBackgrounds[imageBackgroundIndex].GetComponent<Image>().sprite;
            imageBackgroundIndex++;
        }
        else
        {
            throw new System.Exception("background image out of bounds");
        }
    }
	
}
