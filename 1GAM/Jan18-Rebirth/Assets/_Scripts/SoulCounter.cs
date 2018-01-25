using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulCounter : MonoBehaviour {

    private static Text text;
    public static int soulCount = 0;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
    public static void IncreaseSoulCounter()
    {
        soulCount++;
        text.text = soulCount.ToString();
    }
}
