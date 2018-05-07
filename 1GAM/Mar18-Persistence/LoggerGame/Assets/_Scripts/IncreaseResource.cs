using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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
        startPos = transform.position;
        DOTween.Init();
	}

    public void SetIncreaseResourceText(int inceaseAmount)
    {
        text.text = "+" + inceaseAmount.ToString();

        transform.position = startPos;
        text.color = startColor;

        transform.DOLocalMoveY(50, 1);
        text.DOFade(0, 1);
    }
}
