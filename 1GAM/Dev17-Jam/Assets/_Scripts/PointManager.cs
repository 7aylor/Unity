using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PointManager : MonoBehaviour {

    public Text pointsIncreasedNotifcationText;
    private NotifiationManager notificationManager;
    private Text pointText;
    private int points;

    public static PointManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        points = 0;
        pointText = GetComponent<Text>();
        notificationManager = FindObjectOfType<NotifiationManager>();
        pointText.text = points.ToString();
	}
	
    /// <summary>
    /// increase points by 1, add that the ui, then update the notifcation status
    /// </summary>
    public void IncreasePoints()
    {
        points++;
        GameManager.instance.SetCanChangeSpeed(true);
        pointText.text = points.ToString();
        notificationManager.UpdateNotificationText("+1 jars filled. Hey, great job filling the jar!");
    }

    public int GetPoints()
    {
        return points;
    }
}
