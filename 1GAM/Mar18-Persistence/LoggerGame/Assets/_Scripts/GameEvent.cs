using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using DigitalRuby.Tween;

//[Serializable] public class DictionaryOfStringAndAnim : SerializableDictionary<string, Animator> { }

//[Serializable]
//public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
//{
//    [SerializeField]
//    private List<TKey> keys = new List<TKey>();

//    [SerializeField]
//    private List<TValue> values = new List<TValue>();

//    // save the dictionary to lists
//    public void OnBeforeSerialize()
//    {
//        keys.Clear();
//        values.Clear();
//        foreach (KeyValuePair<TKey, TValue> pair in this)
//        {
//            keys.Add(pair.Key);
//            values.Add(pair.Value);
//        }
//    }

//    // load dictionary from lists
//    public void OnAfterDeserialize()
//    {
//        this.Clear();

//        if (keys.Count != values.Count)
//            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

//        for (int i = 0; i < keys.Count; i++)
//            this.Add(keys[i], values[i]);
//    }
//}

public class GameEvent : MonoBehaviour, IPointerClickHandler
{
    //could replace this with a dictionary defined here
    public string[] companies;

    public int eventLife;

    private RectTransform rectTransform;

    private int lumberNeeded;
    private int priceToPay;
    private string eventString;

    private TMP_Text eventText;
    private Animator TalkingHeadAnimator;

    private void Awake()
    {
        eventText = GetComponentInChildren<TMP_Text>();
        TalkingHeadAnimator = GetComponentInChildren<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start () {
        BuildEventString();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveEventPosition(true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            MoveEventPosition(false);
        }
    }

    /// <summary>
    /// animates the event going up and down
    /// </summary>
    /// <param name="down">true crawls down, false crawls up</param>
    public void MoveEventPosition(bool down)
    {
        if (down)
        {
            //animator.SetTrigger("CrawlDown");
            gameObject.Tween("move", transform.position, transform.position + Vector3.down * rectTransform.rect.height * 2, 0.5f, TweenScaleFunctions.QuarticEaseInOut, (t) =>
            {
                // progress
                gameObject.transform.position = t.CurrentValue;
            });
        }
        else
        {
            //animator.SetTrigger("CrawlUp");
            gameObject.Tween("move", transform.position, transform.position + Vector3.left * rectTransform.rect.width * 2, 0.5f, TweenScaleFunctions.QuarticEaseInOut, (t) =>
            {
                // progress
                gameObject.transform.position = t.CurrentValue;
            }, (t) => Destroy(gameObject));

            //add the event to the event queue
            
        }
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
        MoveEventPosition(false);
        //place the event in the event queue
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
        MoveEventPosition(false);
    }
}