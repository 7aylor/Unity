using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyManager : MonoBehaviour {

    int money;
    Text moneyText;
    Summaries summaries;

	// Use this for initialization
	void Start () {
        moneyText = GetComponent<Text>();
        money = 3000;
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
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "$" + money.ToString();
    }

}
