using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PointManager : MonoBehaviour {

    public Text pointsIncreasedNotifcationText;
    private NotifiationManager notificationManager;
    private Text pointText;
    private int points = 0;

	// Use this for initialization
	void Start () {
        pointText = GetComponent<Text>();
        notificationManager = FindObjectOfType<NotifiationManager>();
        pointText.text = points.ToString();
	}
	
    public void IncreasePoints()
    {
        points++;
        pointText.text = points.ToString();
        notificationManager.UpdateNotificationText("+1 jars filled. Hey, great job filling the jar!");
    }
}
