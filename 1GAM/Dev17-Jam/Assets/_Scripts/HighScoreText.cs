using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighScoreText : MonoBehaviour {

    private Text highScoreText;

	// Use this for initialization
	void Start () {
        highScoreText = GetComponent<Text>();
        UpdateHighScoreText();
	}

    private void UpdateHighScoreText()
    {
        //GameManager.instance.SetHighScore(0);
        highScoreText.text = GameManager.instance.GetHighScore().ToString() + 
                             " Jars of Jam!";
    }

}
