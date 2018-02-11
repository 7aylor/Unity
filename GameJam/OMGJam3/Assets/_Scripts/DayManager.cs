using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DayManager : MonoBehaviour {

    public GameObject summary;
    public Summaries summaries;
    Text dayText;
    TimeManager timeManager;
    DailyStats dailyStats;

    private void Awake()
    {
        //summary.SetActive(false);
        dayText = GetComponent<Text>();
        timeManager = FindObjectOfType<TimeManager>();
        dailyStats = summary.GetComponentInChildren<DailyStats>();
    }

    // Use this for initialization
    void Start () {
        UpdateDayText();
	}

    private void UpdateDayText()
    {
        dayText.text = "Day" + summaries.totalDaysBeforeBankruptcy.ToString();
    }

    public void IncreaseDay()
    {
        if(TimeManager.maxTime > 1)
        {
            timeManager.FasterTimer();
        }

        //enable Summary
        summary.SetActive(true);
        dailyStats.UpdateStatsText();
        summaries.totalDaysBeforeBankruptcy++;
        UpdateDayText();
    }
}
