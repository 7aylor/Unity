﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour {

    private TMP_Text text;


    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Use this for initialization
    void Start () {
        text.text = GameManager.instance.lumber.ToString();
	}
	
}