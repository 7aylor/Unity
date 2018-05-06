using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class EventPanel : MonoBehaviour {

    private RectTransform rectTransform;
    private float screenHeight;
    private bool hide;

    private void Awake()
    {
        rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        screenHeight = FindObjectOfType<Canvas>().pixelRect.height;
    }

    private void Start()
    {
        hide = true;
    }

    public void HideEventManagerPanel()
    {
        //crawl down
        if (hide == true)
        {
            //positions that scale
            float scalar = (screenHeight / 720);
            Vector3 deltaHeight = rectTransform.rect.height * Vector3.down * scalar;

            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + deltaHeight;

            gameObject.Tween("move", startPos, endPos, 0.5f, TweenScaleFunctions.QuadraticEaseInOut, (t) =>
            {
                // progress
                transform.position = t.CurrentValue;
            }, (t) =>
            {
                //completeion
            }
            );

            hide = false;
        }
        //swipe left
        else
        {
            //positions that scale
            float scalar = (screenHeight / 720);
            Vector3 deltaHeight = rectTransform.rect.height * Vector3.up * scalar;

            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + deltaHeight;

            gameObject.Tween("move", startPos, endPos, 0.5f, TweenScaleFunctions.QuadraticEaseIn, (t) =>
            {
                // progress
                transform.position = t.CurrentValue;
            }, (t) =>
            {
                //completeion
            }
            );

            hide = true;
        }
    }
}
