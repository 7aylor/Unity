using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class StatsPanel : MonoBehaviour
{

    [SerializeField]
    private static bool hide;
    private float screenHeight;
    private RectTransform rectTransform;
    private BidManager bidManager;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        screenHeight = FindObjectOfType<Canvas>().pixelRect.height;
        bidManager = FindObjectOfType<BidManager>();
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
                if (bidManager.IsBidQueueEmpty() == false)
                {
                    bidManager.FadeBanner(0);
                }


            });
            hide = false;
        }
        //crawl up
        else
        {
            rectTransform.DOAnchorPosY(0 + (rectTransform.rect.height / 2), 1).OnComplete(() =>
            {

            });

            hide = true;
        }
    }

    public bool IsPanelHidden()
    {
        return hide;
    }
}
