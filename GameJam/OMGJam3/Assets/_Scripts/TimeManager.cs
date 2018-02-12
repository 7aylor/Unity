using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeManager : MonoBehaviour {

    public Summaries summaries;
    public Difficulty difficulty;
    public static float currentTime { get; set; }
    bool timerStarted;
    Text timerText;
    [SerializeField] public static float maxTime { get; set; }
    UserInput userInput;
    Stock stock;

	// Use this for initialization
	void Awake () {
        timerText = GetComponent<Text>();
        userInput = FindObjectOfType<UserInput>();
        stock = FindObjectOfType<Stock>();
	}

    private void Start()
    {
        timerStarted = false;
        maxTime = difficulty.maxTime;
        currentTime = maxTime;
        timerText.text = 0.ToString();

        if (difficulty.playTutorial == false)
        {
            StartCoroutine("WaitForNewRound");
        }
    }

    // Update is called once per frame
    void Update () {
        if (timerStarted && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                timerStarted = false;
                currentTime = 0;
                StartCoroutine("WaitForNewRound");
            }
            UpdateTimerText();
        }
	}

    public void StartTimer()
    {
        currentTime = maxTime;
        timerStarted = true;
    }

    public void UpdateTimerText()
    {
        timerText.text = (Mathf.Round(currentTime * 10) / 10f).ToString();
    } 

    public IEnumerator WaitForNewRound()
    {
        yield return new WaitForSeconds(1);
        StartNewRound();
    }

    public void StartNewRound()
    {
        Debug.Log("StartNewRound in TimeManager");
        StartTimer();
        if (summaries.transactionsToday < StocksSoldManager.maxTransactionsPerDay)
        {
            //StartCoroutine("WaitForNewRound");
        }
        userInput.StartNewRound();
        stock.StartNewRound();
    }

    public void FasterTimer()
    {
        timerStarted = false;
        maxTime -= 0.1f;
        currentTime = maxTime;
    }
}
