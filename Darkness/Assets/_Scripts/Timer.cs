using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text timer;
    public bool count = true;


	// Use this for initialization
	void Start () {
        timer = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(count == true)
        {
            timer.text = Mathf.Round(Time.timeSinceLevelLoad).ToString();
        }
	}
}
