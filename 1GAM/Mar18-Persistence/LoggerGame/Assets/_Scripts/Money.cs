using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour {

    public int upkeepCost;
    public int lumberjackCost;
    public int planterCost;
    public int paymentTime;
    public int startMoney;

    private TMP_Text text;

    private float timeToNextPayment;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Use this for initialization
    void Start () {
        ChangeMoneyAmount(startMoney);
        UpdateMoneyText();
	}

    private void Update()
    {
        if(timeToNextPayment >= paymentTime)
        {
            int changeAmount = upkeepCost;

            if (GameManager.instance.lumberjackHired)
            {
                changeAmount += lumberjackCost;
            }

            if (GameManager.instance.planterHired)
            {
                changeAmount += planterCost;
            }

            //decrease money
            ChangeMoneyAmount(-changeAmount);

            //update text
            UpdateMoneyText();

            //reset timer
            timeToNextPayment = 0;

        }
        else
        {
            timeToNextPayment += Time.deltaTime;
        }
    }

    private void ChangeMoneyAmount(int changeAmount)
    {
        GameManager.instance.money += changeAmount;
    }

    private void UpdateMoneyText()
    {
        text.text = GameManager.instance.money.ToString();
    }
}
