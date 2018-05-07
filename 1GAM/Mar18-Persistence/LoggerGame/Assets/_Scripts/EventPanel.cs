using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EventPanel : MonoBehaviour {

    private RectTransform rectTransform;
    private float screenHeight;
    private bool hide;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        screenHeight = FindObjectOfType<Canvas>().pixelRect.height;
    }

    private void Start()
    {
        hide = true;
        DOTween.Init();
    }

    public void HideEventManagerPanel()
    {
        Sequence mySequence = DOTween.Sequence();

        //crawl down
        if (hide == true)
        {
            mySequence.Append(rectTransform.DOAnchorPosY(0 - (rectTransform.rect.height / 2), 1));
            hide = false;
        }
        //swipe left
        else
        {
            mySequence.Append(rectTransform.DOAnchorPosY(0 + (rectTransform.rect.height / 2), 1));
            hide = true;
        }
    }
}
