using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class EndOfRoundPanel : MonoBehaviour {

    private Text titleText;

	// Use this for initialization
	void Start () {
        titleText = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
	}

    public void SetTitleText(bool gameOver, int roundNum)
    {
        if(gameOver == true)
        {
            titleText.text = "You have failed at round " + roundNum;
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            titleText.text = "Congratulations on Completing the Round " + roundNum;
        }
    }
}
