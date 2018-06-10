using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BidManager : MonoBehaviour {

    [Tooltip("Bid prefab")]
    public GameObject ourBid;
    public GameObject newBidBanner;

    [Tooltip("How long to wait between bid spawns")] //set when the demand changes
    public int spawnTime;

    //holds all of the bids in the queue
    private List<GameObject> bidQueue;

    [SerializeField]
    private float timeSinceLastBid;


    private int timeToNextBid;

    private int currentBid; //holds the number in the bid queue to determine where the next bid will be spawned
    private TMP_Text newBidBannerText;
    private Image newBidBannerImage;
    private BidPanel bidPanel;

    private void Awake()
    {
        bidQueue = new List<GameObject>();
        newBidBannerText = newBidBanner.GetComponentInChildren<TMP_Text>();
        newBidBannerImage = newBidBanner.GetComponent<Image>();
        bidPanel = FindObjectOfType<BidPanel>();
    }

    // Use this for initialization
    void Start () {
        timeSinceLastBid = 0f;
        spawnTime = 100;
        timeToNextBid = GetRandomVal(spawnTime, spawnTime + (spawnTime/2));
        currentBid = 0;
        SetNewBidBannerImageTextAlpha(0);
    }
	
	// Update is called once per frame
	void Update () {
        //Check if time has elapsed to spawn new bid
        if (CheckTimeElapsed() == true)
        {
            //if there are less than 4 bids in the queue, spawn
            if(bidQueue.Count < 4)
            {
                GameObject newBid = Instantiate(ourBid, transform);
                bidQueue.Add(newBid);

                //if the panel is hidden, fade in the new bid banner
                if (bidPanel.IsPanelHidden() == true)
                {
                    FadeBanner(1);
                }
            }
            //more than 4 bids, clear it
            else
            {
                foreach(GameObject obj in bidQueue)
                {
                    Destroy(obj);
                }

                bidQueue.Clear();
                FadeBanner(0);

            }

            timeSinceLastBid = 0;
        }
        else
        {
            timeSinceLastBid += Time.deltaTime;
        }
    }

    /// <summary>
    /// Set the alpha and color of the new bid banner to alpha amount
    /// </summary>
    /// <param name="alpha"></param>
    private void SetNewBidBannerImageTextAlpha(float alpha)
    {
        Color c = newBidBannerImage.color;
        c.a = alpha;
        newBidBannerImage.color = c;

        Color tc = newBidBannerText.color;
        tc.a = alpha;
        newBidBannerText.color = tc;
    }

    /// <summary>
    /// Check if time has elapsed between now and when we last spawned a bid
    /// </summary>
    /// <returns></returns>
    private bool CheckTimeElapsed()
    {
        return timeSinceLastBid >= timeToNextBid;
    }

    /// <summary>
    /// Get a random value between min and max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private int GetRandomVal(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    /// <summary>
    /// Remove a bid object from the Queue
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveBidFromQueue(GameObject obj)
    {
        currentBid = bidQueue.Count - bidQueue.FindIndex(bid => bid == obj) - 1;
        bidQueue.Remove(obj);
        Destroy(obj);
    }

    /// <summary>
    /// Fade out the Banner Text and Color
    /// </summary>
    /// <param name="alpha"></param>
    public void FadeBanner(float alpha)
    {
        newBidBannerText.DOFade(alpha, 0.5f);
        newBidBannerImage.DOFade(alpha, 0.5f);
    }

    /// <summary>
    /// Check if the bid queue is empty
    /// </summary>
    /// <returns></returns>
    public bool IsBidQueueEmpty()
    {
        return bidQueue.Count == 0;
    }

    /// <summary>
    /// Sets the amount of time it takes for a new bid to be pitched
    /// </summary>
    /// <param name="newSpawnTime">new bid time</param>
    public void UpdateSpawnTime(int newSpawnTime)
    {
        spawnTime = newSpawnTime;
        timeToNextBid = spawnTime;
    }
}
