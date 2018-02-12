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
    bool retry = false;

    string[] messages =
    {
        "Welcome to Trade Typer",
        "You are here to sell stocks and earn money!",
        "A stock will appear on the screen, like this:",
        "Now, type the name of the stock.",
        "Nice, you've made your first sale!",
        "Oops, you mistyped the name and botched your sale",
        "Type as fast you can though, if time runs out, you'll botch the sale",
        "As the game progresses, it will get faster and harder",
        "The more money you earn, the nicer your apartment gets",
        "How long can you make it as a stock Trader?",
        "Now, let's get started!"
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
        if(messageCount == messages.Length - 1)
        {
            timeManager.WaitForNewRound();
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
            if (userInput.tutorialSuccess == false && retry == false)
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
        //if(messageCount == 6)
        //{
        //    if (userInput.tutorialSuccess == false)
        //    {
        //        retry = true;
        //        messageCount = 4;
        //    }
        //}

        tutorialText.text = messages[messageCount];

        messageCount++;
        yield return new WaitForSeconds(5);
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

}
