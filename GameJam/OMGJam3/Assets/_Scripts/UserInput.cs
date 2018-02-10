using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UserInput : MonoBehaviour {

    public GameObject moneyEarnedObj;

    [SerializeField]string typedInput;
    Text typedText;
    bool canType;
    int currentCharNum = 0;
    MoneyManager moneyManager;
    Summaries summaries;

    private void Awake()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        typedText = GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
        typedInput = "";
        canType = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(canType && Input.inputString != "" && Input.inputString != "\n" && Input.inputString != "\b" && Input.inputString != "\r")
        {
            string currentChar = Input.inputString.ToUpper();
            typedInput += currentChar;
            typedText.text = typedInput;
            CheckNewCharacter(currentChar);
            currentCharNum++;
        }
        else if (canType && TimeManager.currentTime <= 0 && Time.timeSinceLevelLoad > 1)
        {
            FailedTyping();
        }
	}

    private void CheckNewCharacter(string currentChar)
    {
        if(Stock.Name[currentCharNum].ToString() != currentChar)
        {
            FailedTyping();
        }
        else if(currentCharNum == 2)
        {
            //start a new round?
            SucceededTyping();
        }
    }

    private void FailedTyping()
    {
        summaries.totalBusts++;
        summaries.profitsToday -= Stock.Price;
        canType = false;
        Instantiate(moneyEarnedObj, transform.parent);
        MoneyEarned.successfullyTyped = false;
        moneyManager.UpdateMoney(-Stock.Price);
    }

    private void SucceededTyping()
    {
        summaries.totalSales++;
        summaries.profitsToday -= Stock.Price;
        canType = false;
        Instantiate(moneyEarnedObj, transform.parent);
        MoneyEarned.successfullyTyped = true;
        moneyManager.UpdateMoney(Stock.Price);
    }

    /// <summary>
    /// Resets all of the necessary fields. Called from Time Manager
    /// </summary>
    public void StartNewRound()
    {
        currentCharNum = 0;
        typedInput = "";
        typedText.text = "";
        canType = true;
    }
}