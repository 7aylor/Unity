using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

[Serializable] public class DictionaryOfStringAndAnim : SerializableDictionary<string, Animator> { }

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }
}

public class GameEvent : MonoBehaviour, IPointerClickHandler
{
    //public string[] companies;
    public int eventLife;

    public DictionaryOfStringAndAnim companies;

    private int lumberNeeded;
    private int priceToPay;
    private string eventString;

    private Animator animator;
    private float timeSinceLastEvent;
    private int timeToNextEvent;

    private TMP_Text eventText;
    private Animator TalkingHeadAnimator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        eventText = GetComponentInChildren<TMP_Text>();
        TalkingHeadAnimator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start () {
        timeSinceLastEvent = 0f;
        timeToNextEvent = GetRandomVal(1, 5);
        lumberNeeded = GetRandomVal(10, 80);
	}
	
	// Update is called once per frame
	void Update () {

        if (CheckTimeElapsed())
        {
            BuildEventString();
            TriggerEvent(true);
            timeSinceLastEvent = 0;
        }
        else
        {
            timeSinceLastEvent += Time.deltaTime;
        }
    }

    /// <summary>
    /// animates the event going up and down
    /// </summary>
    /// <param name="isNotificationDown">true crawls down, false crawls up</param>
    private void TriggerEvent(bool isNotificationDown)
    {
        if (isNotificationDown)
        {
            animator.SetTrigger("CrawlDown");
        }
        else
        {
            animator.SetTrigger("CrawlUp");
        }
    }

    private bool CheckTimeElapsed()
    {
        return timeSinceLastEvent >= timeToNextEvent;
    }

    private int GetRandomVal(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    /// <summary>
    /// Used to animate the event going up and placing an event in the event queue
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        TriggerEvent(false);
        //place the event in the event queue
    }

    private void BuildEventString()
    {
        //Get random values
        lumberNeeded = GetRandomVal(10, 50);
        priceToPay = lumberNeeded * 5;

        //build the string
        //eventString = string.Format("{0} wants {1} lumber for ${2}", 
        //    companies[GetRandomVal(0, companies.Length)], lumberNeeded, priceToPay);

        //assign the text value
        eventText.text = eventString;
    }

    /// <summary>
    /// Called at the end of the CrawlDown animation
    /// </summary>
    public void HandleEventLife()
    {
        StartCoroutine(EventLife());
    }

    /// <summary>
    /// triggers a crawl up after eventLife time
    /// </summary>
    /// <returns></returns>
    private IEnumerator EventLife()
    {
        yield return new WaitForSeconds(eventLife);
        TriggerEvent(false);
    }
}