using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Image panel;
    private Text panelText;
    private GameObject continueButton;
    private int wordTracker = 0;
    private int subStrTracker = 0;
    private string words;
    private string wordsSubStr;
    private Caveman_Throw cavemanThrow;

    private List<Message> messages = new List<Message>
    {
        new Message(false, "Welcome, my child, to Ubwanga, the land that never dies!"),
        new Message(false, "Unfortunately, we are stuck here in this purgatory for all eternity."),
        new Message(false, "Unless..."),
        new Message(false, "Can you fight?"),
        new Message(false, "My strength has weakened from your rebirth, but with the Runes of Ubwanga we can escape!"),
        new Message(false, "I need 5 souls for each Rune."),
        new Message(false, "Collect them and return them to me, and we may be able to return to the land of the living!"),
        new Message(false, "Good luck my child!"),
        new Message(true, "Ya-ya Ub-Wan-Ga!"),
        new Message(true, "You've collected 5 Souls! Stand back, I will summon a rune!")
    };
        

	// Use this for initialization
	void Start () {
        panel = GetComponent<Image>();
        panelText = GetComponentInChildren<Text>();
        continueButton = transform.GetChild(1).gameObject;
        cavemanThrow = FindObjectOfType<Caveman_Throw>();
        words = messages[wordTracker].MessageText;
        continueButton.SetActive(false);
        EnablePanel(true);
	}
	
    private IEnumerator WriteWords()
    {
        while (wordsSubStr != words)
        {
            wordsSubStr += words[subStrTracker];
            panelText.text = wordsSubStr.ToString();
            subStrTracker++;
            yield return new WaitForSeconds(0.01f);
        }

        continueButton.SetActive(true);
    }

    private void EndOfMenu()
    {
        wordsSubStr = "";
        subStrTracker = 0;
        continueButton.SetActive(false);

        if (wordTracker < messages.Count - 1)
        {
            wordTracker++;
            words = messages[wordTracker].MessageText;
        }
    }

    public void ClickContinue()
    {
        if (messages[wordTracker].EndMessage == true)
        {
            EndOfMenu();
            Debug.Log("End Message true");
            EnablePanel(false);
        }
        else
        {
            EndOfMenu();
            Debug.Log("End Message false");
            StartCoroutine("WriteWords");
        }
    }

    public void EnablePanel(bool enabled)
    {
        panel.enabled = enabled;
        panelText.enabled = enabled;
        if(enabled == true)
        {
            StartCoroutine("WriteWords");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cavemanThrow.CanThrow(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cavemanThrow.CanThrow(true);
    }
}

public struct Message
{
    public bool EndMessage { get; set; }
    public string MessageText { get; set; }

    public Message(bool isEndMessage, string message)
    {
        EndMessage = isEndMessage;
        MessageText = message;
    }
}