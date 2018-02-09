﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeManager : MonoBehaviour {


    public static float currentTime { get; set; }
    bool timerStarted = false;
    Text timerText;
    float maxTime = 2;
    UserInput userInput;
    Stock stock;

	// Use this for initialization
	void Awake () {
        timerText = GetComponent<Text>();
        userInput = FindObjectOfType<UserInput>();
        stock = FindObjectOfType<Stock>();
        currentTime = maxTime;
	}

    private void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update () {

        if (timerStarted && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                currentTime = 0;
            }
            UpdateTimerText();
        }
        else if(currentTime <= 0)
        {
            timerStarted = false;
            currentTime = maxTime;
            StartCoroutine("WaitForNewRound");
        }
	}

    public void StartTimer()
    {
        timerStarted = true;
    }

    public void UpdateTimerText()
    {
        timerText.text = currentTime.ToString();
    } 

    private IEnumerator WaitForNewRound()
    {
        yield return new WaitForSeconds(1);
        StartNewRound();
    }

    private void StartNewRound()
    {
        userInput.StartNewRound();
        stock.StartNewRound();
        StartTimer();
    }
}