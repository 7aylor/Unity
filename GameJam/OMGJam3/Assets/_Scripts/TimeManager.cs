using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeManager : MonoBehaviour {


    public static float currentTime { get; set; }
    bool timerStarted = false;
    Text timerText;
    public static float maxTime { get; set; }
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
        maxTime = 2;
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
            if(StocksSoldManager.stocksSold < StocksSoldManager.maxTransactionsPerDay)
            {
                StartCoroutine("WaitForNewRound");
            }
        }
	}

    public void StartTimer()
    {
        timerStarted = true;
    }

    public void UpdateTimerText()
    {
        timerText.text = (Mathf.Round(currentTime * 10) / 10f).ToString();
    } 

    private IEnumerator WaitForNewRound()
    {
        yield return new WaitForSeconds(1);
        StartNewRound();
    }

    public void StartNewRound()
    {
        userInput.StartNewRound();
        stock.StartNewRound();
        StartTimer();
    }

    public void FasterTimer()
    {
        timerStarted = false;
        maxTime -= 0.1f;
        currentTime = maxTime;
    }
}
