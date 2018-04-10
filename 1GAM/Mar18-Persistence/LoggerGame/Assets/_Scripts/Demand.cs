using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Demand : MonoBehaviour {

    public float minTimeToIncrease;
    public float maxTimeToIncrease;
    public int minDemand;
    public int maxDemand;

    private TMP_Text text;

    private float timeToNextDemandIncrease;
    private float timeSinceLastIncrease;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Use this for initialization
    void Start()
    {
        UpdateDemandText();
        timeSinceLastIncrease = 0;
        timeToNextDemandIncrease = GetNewTimeToIncrease();
    }

    private void Update()
    {
        //check if it is time to increase the demand
        if(timeSinceLastIncrease >= timeToNextDemandIncrease)
        {
            //increase demand
            GameManager.instance.demand += GetNewDemand();

            //update text
            UpdateDemandText();

            //get new time
            timeToNextDemandIncrease = GetNewTimeToIncrease();

            //reset the timer
            timeSinceLastIncrease = 0;

            //maybe throw an event here?
        }
        else
        {
            timeSinceLastIncrease += Time.deltaTime;
        }
    }

    private float GetNewTimeToIncrease()
    {
        return Random.Range(minTimeToIncrease, maxTimeToIncrease);
    }

    private int GetNewDemand()
    {
        return Random.Range(minDemand, maxDemand);
    }

    private void UpdateDemandText()
    {
        text.text = GameManager.instance.demand.ToString();
    }
}

