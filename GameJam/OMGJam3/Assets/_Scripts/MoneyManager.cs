using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyManager : MonoBehaviour {

    public Summaries summaries;
    SceneChange scenery;
    int money;
    Text moneyText;

    private void Awake()
    {
        moneyText = GetComponent<Text>();
        scenery = GetComponent<SceneChange>();
    }

    // Use this for initialization
    void Start () {
        money = 1000;
        summaries.highestEarnings = money;
        UpdateMoneyText();
	}

    public void UpdateMoney(int additionalAmount)
    {
        if(money > summaries.highestEarnings)
        {
            summaries.highestEarnings = money;
        }
        money += additionalAmount;
        UpdateMoneyText();

        if(money <= 0)
        {
            LevelManager.instance.LoadScene("End");
        }
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "$" + money.ToString();
    }

    public void UpdateScenery()
    {
        if (money < 2000)
        {
            scenery.EarlyGame();
        }
        else if (money < 4000)
        {
            scenery.MidGame();

        }
        else
        {
            scenery.EndGame();
        }
    }
}
