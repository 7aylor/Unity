using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LumberjackPromotionPoints : MonoBehaviour {

    private int startPoints;
    public int numPoints;
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        startPoints = 1;
        numPoints = 0;
    }

    private void OnEnable()
    {
        numPoints = startPoints;
    }

    public void UsePoints()
    {
        if(numPoints > 0)
        {
            numPoints--;
            text.text = numPoints.ToString();
        }
    }
}