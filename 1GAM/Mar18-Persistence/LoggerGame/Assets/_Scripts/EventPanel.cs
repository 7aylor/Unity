using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour {

    public GameObject newEventBanner;
    public bool hide;

    private RectTransform rectTransform;
    private float screenHeight;

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
        //crawl down
        if (hide == true)
        {
            rectTransform.DOAnchorPosY(0 - (rectTransform.rect.height / 2), 1).OnComplete(() =>
            {
                newEventBanner.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(() =>
                {
                    newEventBanner.SetActive(false);
                });
            });
            hide = false;
        }
        //swipe left
        else
        {rectTransform.DOAnchorPosY(0 + (rectTransform.rect.height / 2), 1);
            hide = true;
        }
    }
}
