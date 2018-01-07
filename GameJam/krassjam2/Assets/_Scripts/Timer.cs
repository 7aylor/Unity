using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour {

    private Text timerText;
    private int timeInSeconds;
    private float timeSinceLastTick = 0;
    private int round = 1;
    private bool endOfRound;
    private CheckBelowTimer roundWinCollider;

    public EndOfRoundPanel endPanel;

    // Use this for initialization
    void Start () {
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
            transform.Rotate(new Vector3(0, 0, 1 * Time.deltaTime));
        }
        else
        {
            HandleTimeExpired();
        }
    }

    private IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(1f);
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
        float newScale = Random.Range(0.75f, 1.25f);

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
            endOfRound = true;
            endPanel.gameObject.SetActive(true);

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
}
