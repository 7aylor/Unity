using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NotifiationManager))]
public class Tutorial : MonoBehaviour {

    private NotifiationManager nm;

	// Use this for initialization
	void Start () {
        nm = GetComponent<NotifiationManager>();
        StartCoroutine("TutorialText");
    }

    private IEnumerator TutorialText()
    {
        yield return new WaitForSeconds(1);
        nm.UpdateNotificationText("Welcome to the Jam Factory.");
        yield return new WaitForSeconds(3);
        nm.UpdateNotificationText("Package as many jars of jam as you can!");
        yield return new WaitForSeconds(3);
        nm.UpdateNotificationText("Be careful not to waste too much jam!");
        yield return new WaitForSeconds(3);
        nm.UpdateNotificationText("Make sure each Jar has a lid on it!");
        yield return new WaitForSeconds(3);
        nm.UpdateNotificationText("Fill the jars with enough jam and of the right type!");
        yield return new WaitForSeconds(3);
        nm.UpdateNotificationText("And don't make your boss mad!");
        FindObjectOfType<SpawnJars>().CanSpawnJars = true;
        yield return new WaitForSeconds(3);
        nm.UpdateNotificationText("Good Luck!");
        yield return new WaitForSeconds(1);
    }
}