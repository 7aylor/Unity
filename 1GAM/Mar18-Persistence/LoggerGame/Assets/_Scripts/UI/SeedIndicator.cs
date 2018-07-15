using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SeedIndicator : MonoBehaviour {

    public Color startColor;

    private Vector3 startPos;
    private SpriteRenderer sprite;
    private float timeToTween = 1;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startColor = sprite.color;
        DOTween.Init();
    }

    private void OnEnable()
    {
        //get current position and reset color
        startPos = transform.position;
        sprite.color = startColor;

        //move seed image up
        transform.DOLocalMoveY(1, timeToTween);

        //fade out, then on complete disable and reset position
        sprite.DOFade(0, timeToTween).OnComplete(() => { gameObject.SetActive(false); transform.position = startPos; });
    }
}