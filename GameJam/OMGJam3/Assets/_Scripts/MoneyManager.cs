using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyManager : MonoBehaviour {

    public Summaries summaries;
    int money;
    Text moneyText;

	// Use this for initialization
	void Start () {
        moneyText = GetComponent<Text>();
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

}
