using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InstrumentImages : MonoBehaviour {

    public Sprite[] images;
    private int currentImageIndex;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
        currentImageIndex = 0;
        UseNextInstrumentHighlightedImage();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UseNextInstrumentHighlightedImage()
    {
        if(currentImageIndex < images.Length)
        {
            image.sprite = images[currentImageIndex];
            currentImageIndex++;
        }
        else
        {
            Debug.Log("Not enough instrument images");
        }
    }

    public void ResetIstrumentImage()
    {
        currentImageIndex = 0;
        UseNextInstrumentHighlightedImage();
    }
}
