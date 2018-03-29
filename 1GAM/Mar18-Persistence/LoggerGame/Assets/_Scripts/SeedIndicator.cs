using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class SeedIndicator : MonoBehaviour {

    public Color noAlpha;
    public Color startColor;

    private Vector3 startPos;
    private SpriteRenderer sprite;
    private float timeToAnimate = 1;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
    }

    private void OnEnable()
    {
        startPos = transform.position;
        sprite.color = startColor;

        //move position
        gameObject.Tween("move", transform.position, transform.position + Vector3.up * 0.5f, timeToAnimate, TweenScaleFunctions.Linear, (t) =>
        {
            // progress
            gameObject.transform.position = t.CurrentValue;
        }, (t) => {
            // completion
            transform.position = startPos;
            gameObject.SetActive(false);
        });

        //change color
        gameObject.Tween("fade", sprite.color, noAlpha, timeToAnimate, TweenScaleFunctions.Linear, (t) =>
        {
            // progress
            sprite.color = t.CurrentValue;
        });
    }
}