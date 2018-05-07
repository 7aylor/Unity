using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class GameEvent : MonoBehaviour, IPointerClickHandler
{
    //could replace this with a dictionary defined here
    public string[] companies;

    public int eventLife;

    public AnimatorOverrideController Adam;
    public AnimatorOverrideController Seth;


    private RectTransform rectTransform;
    private Transform parent;
    private int lumberNeeded;
    private int priceToPay;
    private string eventString;
    private float screenHeight;
    private float screenWidth;

    private TMP_Text eventText;
    private Animator TalkingHeadAnimator;

    private EventManager eventManager;
    private bool isAccepted;

    private void Awake()
    {
        eventText = GetComponentInChildren<TMP_Text>();
        TalkingHeadAnimator = GetComponentInChildren<Animator>();
        rectTransform = GetComponent<RectTransform>();
        eventManager = GameObject.FindObjectOfType<EventManager>();
        screenHeight = FindObjectOfType<Canvas>().pixelRect.height;
        screenWidth = FindObjectOfType<Canvas>().pixelRect.width;
    }

    // Use this for initialization
    void Start () {
        BuildEventString();
        isAccepted = false;
        DOTween.Init();
        SwitchAnimator();
	}

    private int GetRandomVal(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    private void SwitchAnimator()
    {
        if(Random.Range(0, 2) == 1)
        {
            TalkingHeadAnimator.runtimeAnimatorController = Adam;
        }
        else
        {
            TalkingHeadAnimator.runtimeAnimatorController = Seth;
        }
        
    }

    /// <summary>
    /// Used to animate the event going up and placing an event in the event queue
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {

        Debug.Log("Notification Clicked");

        Sequence tweens = DOTween.Sequence();

        foreach(Transform child in transform)
        {
            child.GetComponent<Image>().DOFade(0, 1);
            child.GetComponent<TMP_Text>().DOFade(0, 1);
        }

        GetComponent<Image>().DOFade(0, 1).OnComplete(() => eventManager.RemoveEventFromQueue(gameObject));

        tweens.Play();
        
    }

    private void BuildEventString()
    {
        //Get random values
        lumberNeeded = GetRandomVal(10, 50);
        priceToPay = lumberNeeded * 5;

        //build the string
        eventString = string.Format("{0} wants {1} lumber for ${2}",
            companies[GetRandomVal(0, companies.Length)], lumberNeeded, priceToPay);

        //assign the text value
        eventText.text = eventString;
    }
}