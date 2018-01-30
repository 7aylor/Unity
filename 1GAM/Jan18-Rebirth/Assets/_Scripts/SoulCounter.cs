using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulCounter : MonoBehaviour {

    private static Text text;
    public static int soulCount = 0;
    private Exclamation exclamation;
    private ActivateRunes activateRunes;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        exclamation = FindObjectOfType<Exclamation>();
        activateRunes = FindObjectOfType<ActivateRunes>();
	}
	
    public void IncreaseSoulCounter()
    {
        soulCount++;
        text.text = soulCount.ToString();
        if(soulCount >= 5)
        {
            exclamation.Enabled(true);
            Shaman.ChangeCursor = true;
        }
        else
        {
            exclamation.Enabled(false);
            Shaman.ChangeCursor = false;
        }
    }

    public void BuildRune()
    {
        soulCount -= 5;
        text.text = soulCount.ToString();
        Shaman.ChangeCursor = false;
        activateRunes.EnableRune(true);
    }

    public void DestroyRune()
    {
        Shaman.ChangeCursor = false;
        activateRunes.EnableRune(false);
    }
}
