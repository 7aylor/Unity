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
    private Shaman shaman;
    private SpawnEnemies spawnEnemies;
    private EnabledCaveman cavemanMovement;
    private SoulCounter soulCounter;

    private List<Message> messages = new List<Message>
    {
        new Message(false, "Welcome, my child, to Ubwanga, the land that never dies!"),
        new Message(false, "Unfortunately, we are stuck here in this purgatory for all eternity."),
        new Message(false, "Unless..."),
        new Message(false, "Can you fight?"),
        new Message(false, "My strength has weakened from your rebirth, but with the Runes of Ubwanga we can escape!"),
        new Message(false, "I need 5 souls for each Rune and there are 4 runes total."),
        new Message(false, "Collect them and return them to me, and we may be able to return to the land of the living!"),
        new Message(false, "But be careful, I can only revive you with the power of the runes."),
        new Message(false, "Good luck my child!"),
        new Message(true, "Ya-ya Ub-Wan-Ga!"),
        new Message(true, "You've collected 5 Souls! Stand back, I will summon a rune!"),
        new Message(true, "Ub-Wan-Ga! Ub-Wan-GAAA! I rebirth you from the strength of the rune!"),
        new Message(true, "You have collected enough souls and I have summoned a portal out of this wretched place. Escape now, before it's too late!")
    };
        

	// Use this for initialization
	void Start () {
        panel = GetComponent<Image>();
        panelText = GetComponentInChildren<Text>();
        continueButton = transform.GetChild(1).gameObject;
        cavemanThrow = FindObjectOfType<Caveman_Throw>();
        words = messages[wordTracker].MessageText;
        continueButton.SetActive(false);
        shaman = FindObjectOfType<Shaman>();
        spawnEnemies = FindObjectOfType<SpawnEnemies>();
        cavemanMovement = FindObjectOfType<EnabledCaveman>();
        soulCounter = FindObjectOfType<SoulCounter>();
        //EnablePanel(true);
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
        shaman.Talk(false);
        if(wordTracker == 10)
        {
            Debug.Log("Word Tracker 10");
            soulCounter.BuildRune();
        }
        else if(wordTracker == 11)
        {
            Debug.Log("Word Tracker 11");
            soulCounter.DestroyRune();
        }

        //last message in this series
        if (messages[wordTracker].EndMessage == true)
        {
            EndOfMenu();
            shaman.Talk(false);
            EnablePanel(false);
            spawnEnemies.CanSpawnEnemies = true;
            spawnEnemies.ResetNumEnemies();
        }
        else
        {
            EndOfMenu();
            StartCoroutine("WriteWords");
        }
    }

    public void EnablePanel(bool enabled)
    {
        panel.enabled = enabled;
        panelText.enabled = enabled;
        shaman.Talk(true);
        if (enabled == true)
        {
            cavemanMovement.EnableMovement(false);
            StartCoroutine("WriteWords");
        }
        else
        {
            cavemanMovement.EnableMovement(true);
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

    public void DecreaseWordTracker()
    {
        wordTracker--;
    }

    public void SetWordTracker(int newNum)
    {
        wordTracker = newNum;
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