using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public GameObject ourEvent;

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
            GameObject newEvent = Instantiate(ourEvent, transform.parent);
            eventQueue.Add(newEvent);
            PushNextEventDown();
            timeSinceLastEvent = 0;
        }
        else
        {
            timeSinceLastEvent += Time.deltaTime;
        }
    }

    private void PushEventsDown()
    {
        foreach(GameObject e in eventQueue){
            e.GetComponent<GameEvent>().MoveEventPosition(true);
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

    public void PushNextEventDown()
    {
        if(currentEvent < eventQueue.Count)
        {
            eventQueue[currentEvent].GetComponent<GameEvent>().MoveEventPosition(true);
            currentEvent++;
        }
        else if(currentEvent >= eventQueue.Count)
        {
            currentEvent = 0;
        }
    }
}
