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
    private bool endSection = false;

    private string[] messages =
    {
        "Welcome, my child, to Ubwanga, the land that never dies!",
        "Unfortunately, we are stuck here in this purgatory for all eternity.",
        "Unless...",
        "Can you fight?",
        "My strength has weakened from your rebirth, but with the Runes of Ubwanga we can escape!",
        "I need 5 souls for each Rune.",
        "Collect them and return them to me, and we may be able to return to the land of the living!",
        "Good luck my child!",
        "Ya-ya Ub-Wan-Ga!" //8 stop here
    };

	// Use this for initialization
	void Start () {
        panel = GetComponent<Image>();
        panelText = GetComponentInChildren<Text>();
        continueButton = transform.GetChild(1).gameObject;
        cavemanThrow = FindObjectOfType<Caveman_Throw>();
        words = messages[wordTracker];
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

        EndOfMenu();
    }

    private void EndOfMenu()
    {
        continueButton.SetActive(true);
        wordsSubStr = "";
        subStrTracker = 0;
        if (wordTracker < messages.Length - 1)
        {
            wordTracker++;
            words = messages[wordTracker];
            if (wordTracker == 8)
            {
                endSection = true;
            }
            else
            {
                endSection = false;
            }
        }
        else
        {
            EnablePanel(false);
            continueButton.SetActive(false);
        }
    }

    public void ClickContinue()
    {
        if(endSection == true)
        {
            StartCoroutine("WriteWords");
        }
        else
        {
            StartCoroutine("WriteWords");
            continueButton.SetActive(false);
        }
    }

    public void EnablePanel(bool enabled)
    {
        panel.enabled = enabled;
        panelText.enabled = enabled;
        StartCoroutine("WriteWords");
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
