using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Tutorial : MonoBehaviour {

    public Difficulty difficulty;
    Text tutorialText;
    int messageCount = 0;
    Stock stock;
    UserInput userInput;
    TimeManager timeManager;

    string[] messages =
    {
        "Welcome to Stock Market Typer!",
        "You are here to sell stocks and earn money!",
        "During the game, a stock will appear on the screen like this:",
        "Now, type the name of the stock.",
        "Nice, you've made your first sale!",
        "Oops, you botched your sale by taking too long or mistyping. Try again!",
        "As the game progresses, it will get faster and harder.",
        "How long can you make it as a stock Trader before going bankrupt?",
        "Let's get started a find out!"
    };

    private void Awake()
    {
        tutorialText = GetComponent<Text>();
        stock = FindObjectOfType<Stock>();
        userInput = FindObjectOfType<UserInput>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    // Use this for initialization
    void Start () {
        if(difficulty.playTutorial == true)
        {
            StartCoroutine("WaitToDisplay");
        }
	}

    private IEnumerator DisplayMessage()
    {
        if(messageCount == messages.Length)
        {
            tutorialText.text = "";
            timeManager.StartNewRound();
            yield break;
        }
        if(messageCount == 2)
        {
            stock.NewStock();
        }
        if(messageCount == 3)
        {
            userInput.TutorialStartTyping();
        }
        if(messageCount == 4)
        {
            if (userInput.tutorialSuccess == false)
            {
                messageCount++;
            }
        }
        if(messageCount == 5)
        {
            if (userInput.tutorialSuccess == true)
            {
                messageCount++;
            }
        }
        if (messageCount == 6)
        {
            userInput.GetComponent<Text>().text = "";
            stock.GetComponent<Text>().text = "";
            if (userInput.tutorialSuccess == false)
            {
                messageCount = 3;
                stock.NewStock();
                userInput.TutorialStartTyping();
            }
        }

        tutorialText.text = messages[messageCount];

        messageCount++;
        yield return new WaitForSeconds(4);
        StartCoroutine("DisplayMessage");

    }

    private IEnumerator WaitToDisplay()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine("DisplayMessage");
    }

    private IEnumerator WaitForTyping()
    {
        while (userInput.tutorialCanType == true)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void EndTypingInTutorial()
    {
        StopAllCoroutines();
        StartCoroutine("DisplayMessage");
    }

}
