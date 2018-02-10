using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DayManager : MonoBehaviour {

    public GameObject summary;
    Text dayText;
    int day;
    TimeManager timeManager;

    private void Awake()
    {
        //summary.SetActive(false);
        dayText = GetComponent<Text>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    // Use this for initialization
    void Start () {
        day = 1;
        UpdateDayText();
	}

    private void UpdateDayText()
    {
        dayText.text = "Day" + day.ToString();
    }

    public void IncreaseDay()
    {
        day++;
        UpdateDayText();
        if(TimeManager.maxTime > 1)
        {
            timeManager.FasterTimer();
        }

        //enable Summary
        summary.SetActive(true);
    }
}
