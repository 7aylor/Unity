using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lumber : MonoBehaviour {

    private TMP_Text text;
    private IncreaseResource decreaseLumber;
    private IncreaseResource increaseLumber;
    private StatsManager stats;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        decreaseLumber = GameObject.FindGameObjectWithTag("DecreaseLumber").GetComponent<IncreaseResource>();
        increaseLumber = GameObject.FindGameObjectWithTag("IncreaseLumber").GetComponent<IncreaseResource>();
        stats = FindObjectOfType<StatsManager>();
    }

    private void Start()
    {
        //UpdateLumberCount(0);
    }

    public void UpdateLumberCount(int increaseAmount)
    {
        GameManager.instance.lumber += increaseAmount;
        text.text = GameManager.instance.lumber.ToString();

        if (increaseAmount < 0)
        {
            decreaseLumber.SetIncreaseResourceText(increaseAmount);
        }
        else
        {
            increaseLumber.SetIncreaseResourceText(increaseAmount);
            GameManager.instance.totalLumberHarvested += increaseAmount;
            stats.UpdateStats(StatsManager.stat.totalLumberHarvested);
        }

    }

}
