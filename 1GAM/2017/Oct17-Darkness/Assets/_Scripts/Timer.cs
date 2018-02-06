using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text timerText;
    public bool count = true;
    public int timeInSeconds = 0;


	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(count == true)
        {
            timeInSeconds = (int)Mathf.Round(Time.timeSinceLevelLoad);
            timerText.text = timeInSeconds.ToString();
        }
	}
}
