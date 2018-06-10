using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BidManager : MonoBehaviour {

    public GameObject ourEvent;
    public Transform eventParent;
    public GameObject eventBanner;
    public int spawnTime;

    private List<GameObject> eventQueue;

    [SerializeField]
    private float timeSinceLastEvent;

    private int timeToNextEvent;


    private int currentEvent;
    private TMP_Text bannerText;
    private Image bannerImage;
    private BidPanel bidPanel;

    private void Awake()
    {
        eventQueue = new List<GameObject>();
        bannerText = eventBanner.GetComponentInChildren<TMP_Text>();
        bannerImage = eventBanner.GetComponent<Image>();
        bidPanel = FindObjectOfType<BidPanel>();
    }

    // Use this for initialization
    void Start () {
        timeSinceLastEvent = 0f;
        spawnTime = 100;
        timeToNextEvent = GetRandomVal(spawnTime, spawnTime + (spawnTime/2));
        currentEvent = 0;
        ChangeBannerImageTextAlpha(0);
    }
	
	// Update is called once per frame
	void Update () {
        //Check if time has elapsed to spawn new event
        if (CheckTimeElapsed() == true)
        {
            //if there are less than 4 events in the queue, spawn
            if(eventQueue.Count < 4)
            {
                GameObject newEvent = Instantiate(ourEvent, transform);
                eventQueue.Add(newEvent);

                //if the panel is hidden, fade in the new event banner
                if (bidPanel.IsPanelHidden() == true)
                {
                    FadeBanner(1);
                }
            }
            //more than 4 events, clear it
            else
            {
                foreach(GameObject obj in eventQueue)
                {
                    Destroy(obj);
                }

                eventQueue.Clear();
                FadeBanner(0);

            }

            timeSinceLastEvent = 0;
        }
        else
        {
            timeSinceLastEvent += Time.deltaTime;
        }
    }

    private void ChangeBannerImageTextAlpha(float alpha)
    {
        Color c = bannerImage.color;
        c.a = alpha;
        bannerImage.color = c;

        Color tc = bannerText.color;
        tc.a = alpha;
        bannerText.color = tc;
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
    /// Remove an event object from the Queue
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveEventFromQueue(GameObject obj)
    {
        currentEvent = eventQueue.Count - eventQueue.FindIndex(@event => @event == obj) - 1;
        eventQueue.Remove(obj);
        Destroy(obj);
    }

    public void FadeBanner(float alpha)
    {
        bannerText.DOFade(alpha, 0.5f);
        bannerImage.DOFade(alpha, 0.5f);
    }

    public bool IsEventQueueEmpty()
    {
        return eventQueue.Count == 0;
    }

    /// <summary>
    /// Sets the amount of time it takes for a new bid to be pitched
    /// </summary>
    /// <param name="newSpawnTime">new bid time</param>
    public void UpdateSpawnTime(int newSpawnTime)
    {
        //timeSinceLastEvent = 0;
        spawnTime = newSpawnTime;
        timeToNextEvent = spawnTime;
    }
}
