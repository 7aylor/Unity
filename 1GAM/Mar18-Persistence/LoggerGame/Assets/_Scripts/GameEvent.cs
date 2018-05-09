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
    public int eventLife;
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

    public NameAnimPair[] companies;

    private NameAnimPair selectedCompany;

    private Money money;
    private Lumber lumber;
    private IncreaseResource lumberTweenText;
    
    private void Awake()
    {
        eventText = GetComponentInChildren<TMP_Text>();
        TalkingHeadAnimator = GetComponentInChildren<Animator>();
        rectTransform = GetComponent<RectTransform>();
        eventManager = GameObject.FindObjectOfType<EventManager>();
        screenHeight = FindObjectOfType<Canvas>().pixelRect.height;
        screenWidth = FindObjectOfType<Canvas>().pixelRect.width;
        money = FindObjectOfType<Money>();
        lumber = FindObjectOfType<Lumber>();
        lumberTweenText = FindObjectOfType<IncreaseResource>();
    }

    // Use this for initialization
    void Start () {
        isAccepted = false;
        DOTween.Init();
        AssignRandomCompany();
        BuildEventString();
    }

    private int GetRandomVal(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    private void AssignRandomCompany()
    {
        int rand = GetRandomVal(0, companies.Length);

        for (int i = 0; i < companies.Length; i++)
        {
            if(i == rand)
            {
                selectedCompany = companies[i];
                TalkingHeadAnimator.runtimeAnimatorController = companies[i].anim;

                break;
            }
        }
    }

    /// <summary>
    /// Used to animate the event going up and placing an event in the event queue
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.instance.lumber >= lumberNeeded)
        {
            Sequence tweens = DOTween.Sequence();

            foreach (Transform child in transform)
            {
                child.GetComponent<Image>().DOFade(0, 1);
                child.GetComponent<TMP_Text>().DOFade(0, 1);
            }

            GetComponent<Image>().DOFade(0, 1).OnComplete(() =>
            {
                eventManager.RemoveEventFromQueue(gameObject);
                money.ChangeMoneyAmount(priceToPay);
                lumber.UpdateLumberCount(-lumberNeeded);
                lumberTweenText.SetIncreaseResourceText(-lumberNeeded);
            });

            tweens.Play();
        }
    }

    private void BuildEventString()
    {
        //Get random values
        lumberNeeded = GetRandomVal(10, 50);
        priceToPay = lumberNeeded * 5;

        //build the string
        eventString = string.Format("{0} wants {1} lumber for ${2}", selectedCompany.name, lumberNeeded, priceToPay);

        //assign the text value
        eventText.text = eventString;
    }
}

[System.Serializable]
public struct NameAnimPair
{
    public string name;
    public AnimatorOverrideController anim;

    public NameAnimPair(string myName, AnimatorOverrideController overrider)
    {
        name = myName;
        anim = overrider;
    }
}