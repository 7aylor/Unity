using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyManager : MonoBehaviour {

    int money;
    Text moneyText;

	// Use this for initialization
	void Start () {
        moneyText = GetComponent<Text>();
        money = 10000;
        UpdateMoneyText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateMoney(int additionalAmount)
    {
        money += additionalAmount;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "$" + money.ToString();
    }

}
