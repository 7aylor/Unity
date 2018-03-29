using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DigitalRuby.Tween;

public class IncreaseResource : MonoBehaviour {

    public Color noAlpha;
    private Color startColor;

    private Vector3 startPos;
    private float timeToAnimate = 2;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Use this for initialization
    void Start () {
        //gameObject.SetActive(false);
        startColor = text.color;
        text.color = noAlpha;
	}

    public void SetIncreaseResourceText(int inceaseAmount)
    {
        text.text = "+" + inceaseAmount.ToString();

        startPos = transform.position;
        text.color = startColor;

        //move position
        gameObject.Tween("move", transform.position, transform.position + Vector3.up * 50, timeToAnimate, TweenScaleFunctions.Linear, (t) =>
        {
            // progress
            gameObject.transform.position = t.CurrentValue;
        }, (t) => {
            // completion
            transform.position = startPos;
        });
        //change color
        gameObject.Tween("fade", text.color, noAlpha, timeToAnimate, TweenScaleFunctions.Linear, (t) =>
        {
            // progress
            text.color = t.CurrentValue;
        });
    }

    //private IEnumerator MoveUp()
    //{
    //    //Look into using Tweens here https://assetstore.unity.com/packages/tools/animation/tween-55983?aid=1011lGnL&utm_source=aff
    //    for (int i = 0; i < 10; i++)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //        transform.Translate(Vector3.up * 5);
    //        Color c = text.color;
    //        c.a -= 26;
    //        text.color = c;
    //    }

    //    transform.position += Vector3.down * 50;

    //}
}
