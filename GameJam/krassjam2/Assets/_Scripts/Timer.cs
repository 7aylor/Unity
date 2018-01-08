﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour {

    private Text timerText;
    private int timeInSeconds;
    private float timeSinceLastTick = 0;
    private int round = 1;
    private int speed = 1;
    private bool endOfRound;
    private CheckBelowTimer roundWinCollider;
    private AudioSource boop;

    public EndOfRoundPanel endPanel;


    // Use this for initialization
    void Start () {
        boop = GetComponent<AudioSource>();
        timerText = GetComponent<Text>();
        roundWinCollider = FindObjectOfType<CheckBelowTimer>();
        InitializeTimer();
	}
	
	// Update is called once per frame
	void Update () {
        if(timeSinceLastTick >= 1f && timeInSeconds > 0)
        {
            timeSinceLastTick = 0;
            StartCoroutine("CountDownTimer");
        }
        else
        {
            timeSinceLastTick += Time.deltaTime;
        }

        if(timeInSeconds > 0)
        {
            transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        else
        {
            HandleTimeExpired();
        }
    }

    private IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(1f);
        boop.Play();
        timeInSeconds--;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        string formattedSeconds = "";
        if(timeInSeconds < 10)
        {
            formattedSeconds = "0" + timeInSeconds.ToString();
        }
        else
        {
            formattedSeconds = timeInSeconds.ToString();
        }
        timerText.text = string.Format("0:{0}", formattedSeconds);
    }

    public void InitializeTimer()
    {
        timeInSeconds = 10;
        endOfRound = false;
        UpdateTimerText();
        transform.rotation = PickRandomSpawnRotation();
        transform.localScale = PickRandomSpawnScale();
    }

    private Vector3 PickRandomSpawnScale()
    {
        float newScale = Random.Range(0.4f, 0.6f);

        Debug.Log("Spawn Scale: " + newScale);

        return new Vector3(newScale, newScale, newScale);
    }

    private Quaternion PickRandomSpawnRotation()
    {
        float newZRot = Random.Range(-90, 90);

        return Quaternion.Euler(new Vector3(0, 0, newZRot));
    }

    private void HandleTimeExpired()
    {
        if(endOfRound == false)
        {
            FriendNames.ClearCurrentNameList();
            endOfRound = true;
            endPanel.gameObject.SetActive(true);
            IncreaseSpeed();

            if (roundWinCollider.FriendsBelowTimer() >= FriendsDeck.NumFriends)
            {
                endPanel.SetTitleText(false, round);
            }
            else
            {
                endPanel.SetTitleText(transform, round);

            }
            round++;
        }
    }

    private void IncreaseSpeed()
    {
        speed++;
    }
}
