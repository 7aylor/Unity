using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UserInput : MonoBehaviour {

    public GameObject moneyEarnedObj;
    public Summaries summaries;

    [SerializeField]string typedInput;
    Text typedText;
    bool canType;
    int currentCharNum = 0;
    MoneyManager moneyManager;
    Color startColor;
    

    private void Awake()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        typedText = GetComponent<Text>();
        startColor = typedText.color;
    }

    // Use this for initialization
    void Start () {
        canType = false;
        typedText.text = "";
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
        summaries.bustsToday++;
        summaries.profitsToday -= Stock.Price;
        canType = false;
        typedText.color = Color.red;
        Instantiate(moneyEarnedObj, transform.parent);
        MoneyEarned.successfullyTyped = false;
        moneyManager.UpdateMoney(-Stock.Price);
    }

    private void SucceededTyping()
    {
        summaries.totalSales++;
        summaries.salesToday++;
        summaries.profitsToday += Stock.Price;
        canType = false;
        typedText.color = Color.green;
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
        typedText.color = startColor;
        canType = true;
    }
}