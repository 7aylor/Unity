using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    public GameObject ourEvent;
    public GameObject newEventBanner;
    public Transform eventParent;

    private List<GameObject> eventQueue;

    private float timeSinceLastEvent;
    private int timeToNextEvent;

    private int currentEvent;

    private void Awake()
    {
        eventQueue = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {
        timeSinceLastEvent = 0f;
        timeToNextEvent = GetRandomVal(3, 5);
        currentEvent = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (CheckTimeElapsed())
        {
            if(eventQueue.Count < 4)
            {
                GameObject newEvent = Instantiate(ourEvent, transform);
                eventQueue.Add(newEvent);
                //PushNextEventDown();
            }
            else
            {
                GameObject removedObj = eventQueue[0].gameObject;
                eventQueue.RemoveAt(0);
                Destroy(removedObj);
            }

            timeSinceLastEvent = 0;
        }
        else
        {
            timeSinceLastEvent += Time.deltaTime;
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
    /// Remove an event object from the Queue
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveEventFromQueue(GameObject obj)
    {
        currentEvent = eventQueue.Count - eventQueue.FindIndex(@event => @event == obj) - 1;
        eventQueue.Remove(obj);
        Destroy(obj);
    }
}
