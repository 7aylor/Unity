using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class EventPanel : MonoBehaviour {

    public bool hide;

    private float screenHeight;
    private RectTransform rectTransform;
    private EventManager eventManager;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        screenHeight = FindObjectOfType<Canvas>().pixelRect.height;
        eventManager = FindObjectOfType<EventManager>();
    }

    private void Start()
    {
        hide = true;
        DOTween.Init();
    }

    public void HideEventManagerPanel()
    {
        //crawl down
        if (hide == true)
        {
            rectTransform.DOAnchorPosY(0 - (rectTransform.rect.height / 2), 1).OnComplete(() =>
            {
                if(eventManager.IsEventQueueEmpty() == false)
                {
                    eventManager.FadeBanner(0);
                }
                
                hide = false;
            });
        }
        //crawl up
        else
        {
            rectTransform.DOAnchorPosY(0 + (rectTransform.rect.height / 2), 1).OnComplete(() =>
            {
                hide = true;
            });
        }
    }
}
