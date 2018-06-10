using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Bid : MonoBehaviour, IPointerClickHandler
{
    //could replace this with a dictionary defined here
    public int bidLife;
    private RectTransform rectTransform;
    private Transform parent;
    private int lumberNeeded;
    private int priceToPay;
    private string bidString;
    private float screenHeight;
    private float screenWidth;
    private Demand demand;

    private TMP_Text bidText;
    private Animator TalkingHeadAnimator;

    private BidManager bidManager;
    private bool isAccepted;

    public NameAnimPair[] companies;

    private NameAnimPair selectedCompany;

    private Money money;
    private Lumber lumber;
    private StatsManager stats;
    
    private void Awake()
    {
        bidText = GetComponentInChildren<TMP_Text>();
        TalkingHeadAnimator = GetComponentInChildren<Animator>();
        rectTransform = GetComponent<RectTransform>();
        bidManager = GameObject.FindObjectOfType<BidManager>();
        screenHeight = FindObjectOfType<Canvas>().pixelRect.height;
        screenWidth = FindObjectOfType<Canvas>().pixelRect.width;
        money = FindObjectOfType<Money>();
        lumber = FindObjectOfType<Lumber>();
        demand = FindObjectOfType<Demand>();
        stats = FindObjectOfType<StatsManager>();
    }

    // Use this for initialization
    void Start () {
        isAccepted = false;
        DOTween.Init();
        AssignRandomCompany();
        BuildBidString();
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
    /// Used to animate the bid going up and placing an bid in the bid queue
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
                bidManager.RemoveBidFromQueue(gameObject);
                money.ChangeMoneyAmount(priceToPay);
                lumber.UpdateLumberCount(-lumberNeeded);
                GameManager.instance.lumberInMarket += lumberNeeded;
                stats.UpdateStats(StatsManager.stat.lumberInMarket);
                GameManager.instance.totalNumberOfSales++;
                stats.UpdateStats(StatsManager.stat.totalNumberOfSales);
                demand.UpdateDemand();
            });

            tweens.Play();
        }
    }

    private void BuildBidString()
    {
        //Get random values
        lumberNeeded = GetRandomVal(10, 50);
        priceToPay = lumberNeeded * 5;

        //build the string
        bidString = string.Format("<color=#{0}><b>{1}</b></color> wants <color=#602D06FF>{2} lumber</color> for <color=#1F5F14FF>${3}</color>", ColorUtility.ToHtmlStringRGB(selectedCompany.textColor), selectedCompany.name, lumberNeeded, priceToPay);

        //assign the text value
        bidText.text = bidString;
    }
}

[System.Serializable]
public struct NameAnimPair
{
    public string name;
    public AnimatorOverrideController anim;
    public Color textColor;

    public NameAnimPair(string myName, AnimatorOverrideController overrider, Color c)
    {
        name = myName;
        anim = overrider;
        textColor = c;
    }
}