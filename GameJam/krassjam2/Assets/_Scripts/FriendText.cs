using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FriendText : MonoBehaviour {

    private Text friendName;

	// Use this for initialization
	void Start () {
        friendName = GetComponent<Text>();
        friendName.text = FriendNames.GetRandomFriendName();
        friendName.color = new Color32((byte)Random.Range(0, 255), 
                           (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
    }
}
