using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class SeedIndicator : MonoBehaviour {

    public Color noAlpha;

    private Vector3 startPos;
    private SpriteRenderer sprite;
    private float timeToAnimate = 1;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        startPos = transform.position;
    }
    private void OnEnable()
    {
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
