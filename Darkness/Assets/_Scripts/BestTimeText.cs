using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTimeText : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

        if (PlayerPrefs.HasKey(gameObject.name) && PlayerPrefs.GetInt(gameObject.name) != Int32.MaxValue)
        {
            text.text = "Best Time: " + PlayerPrefs.GetInt(gameObject.name);
        }
        else
        {
            text.text = "";
        }
	}
}
