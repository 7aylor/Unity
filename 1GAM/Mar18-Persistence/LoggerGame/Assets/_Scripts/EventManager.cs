using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public GameObject ourEvent;

    private List<GameObject> eventQueue;

    private float timeSinceLastEvent;
    private int timeToNextEvent;

    private void Awake()
    {
        eventQueue = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {
        timeSinceLastEvent = 0f;
        timeToNextEvent = GetRandomVal(3, 5);
    }
	
	// Update is called once per frame
	void Update () {
        if (CheckTimeElapsed())
        {
            GameObject newEvent = Instantiate(ourEvent, transform.parent);
            eventQueue.Add(newEvent);
            PushEventsDown();
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
}
