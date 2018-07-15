using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class IncreaseResource : MonoBehaviour {

    public Color noAlpha;

    private Vector3 startPos;
    private float timeToAnimate = 2;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Use this for initialization
    void Start () {
        text.color = noAlpha;
        startPos = transform.position;
        DOTween.Init();
	}

    public void SetIncreaseResourceText(int inceaseAmount)
    {
        string increaseText = "";

        if(inceaseAmount > 0)
        {
            increaseText += "+";
            Color c = Color.green;
            c.a = 1;
            text.color = c;
        }
        else
        {
            Color c = Color.red;
            c.a = 1;
            text.color = c;
        }

        increaseText += inceaseAmount.ToString();

        text.text = increaseText;

        transform.position = startPos;

        transform.DOLocalMoveY(50, 1);
        text.DOFade(0, 1);
    }
}
