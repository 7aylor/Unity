using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class NotifiationManager : MonoBehaviour {

    private Text notificationText;
    private int delayToClear = 3;

    private void Start()
    {
        notificationText = GetComponent<Text>();
    }

    /// <summary>
    /// Updates the notification text and clears after a set time
    /// </summary>
    /// <param name="notification"></param>
    public void UpdateNotificationText(string notification)
    {
        notificationText.text = notification;
        StartCoroutine("ClearNotification");
    }

    /// <summary>
    /// Waits for delayToClear seconds and then clears the notification text
    /// </summary>
    /// <returns></returns>
    private IEnumerator ClearNotification()
    {
        yield return new WaitForSeconds(delayToClear);
        notificationText.text = "";
    }
}
