using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UserInput : MonoBehaviour {

    public GameObject moneyEarnedObj;
    public Summaries summaries;
    public bool tutorialCanType = false;
    public bool tutorialSuccess = false;

    [SerializeField]string typedInput;
    Text typedText;
    bool canType;
    int currentCharNum = 0;
    MoneyManager moneyManager;
    Color startColor;
    Tutorial tutorial;
    AudioSource click;
    
    

    private void Awake()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        tutorial = FindObjectOfType<Tutorial>();
        typedText = GetComponent<Text>();
        startColor = typedText.color;
        click = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        canType = false;
        typedText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if(canType && Input.inputString != "" && GoodInputCharacters() == true)
        {
            click.Play();
            HandleTyping();
        }
        else if (canType && TimeManager.currentTime <= 0 && Time.timeSinceLevelLoad > 1)
        {
            FailedTyping();
        }

        if (tutorialCanType == true && GoodInputCharacters() == true)
        {
            click.Play();
            HandleTyping();
        }
    }

    private bool GoodInputCharacters()
    {
        if(Input.inputString != "" && Input.inputString != "\n" && Input.inputString != "\b" && Input.inputString != "\r")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HandleTyping()
    {
        string currentChar = Input.inputString.ToUpper();
        typedInput += currentChar;
        typedText.text = typedInput;
        CheckNewCharacter(currentChar);
        currentCharNum++;
    }

    private void CheckNewCharacter(string currentChar)
    {
        if(canType == true)
        {
            if (Stock.Name[currentCharNum].ToString() != currentChar)
            {
                FailedTyping();
            }
            else if (currentCharNum == 2)
            {
                //start a new round?
                SucceededTyping();
            }
        }
        if(tutorialCanType == true)
        {
            if (Stock.Name[currentCharNum].ToString() != currentChar)
            {
                tutorialCanType = false;
                typedText.color = Color.red;
                tutorialSuccess = false;
                tutorial.EndTypingInTutorial();
            }
            else if (currentCharNum == 2)
            {
                Debug.Log("Called from UserInput");
                tutorialSuccess = true;
                tutorialCanType = false;
                typedText.color = Color.green;
                tutorial.EndTypingInTutorial();
            }
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
        tutorialCanType = false;
    }

    public void TutorialStartTyping()
    {
        currentCharNum = 0;
        canType = false;
        tutorialCanType = true;
        typedInput = "";
        typedText.text = "";
        typedText.color = startColor;
    }
}