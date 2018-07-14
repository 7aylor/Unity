using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromotionPoints : MonoBehaviour {

    public int numPoints;
    TMP_Text text;
    private UIActions uIActions;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        uIActions = FindObjectOfType<UIActions>();
    }

    private void Start()
    {
        numPoints = 1;
    }

    private void OnEnable()
    {
        ResetPoints();
    }

    public void UsePoints()
    {
        if(numPoints > 0)
        {
            numPoints--;
            text.text = numPoints.ToString();
        }
    }

    public void ResetPoints()
    {
        //Debug.Log("Reset points");
        uIActions.PromoteState = GameManager.promote_UI_State.None;
        numPoints = 1;
        text.text = numPoints.ToString();
    }
}