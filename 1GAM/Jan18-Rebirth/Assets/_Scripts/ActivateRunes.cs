using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRunes : MonoBehaviour {

    private SpriteRenderer[] runes;
    private DialogueWindow dw;
    private int currentRune = 0;

	// Use this for initialization
	void Start () {
        runes = transform.GetComponentsInChildren<SpriteRenderer>();
        dw = FindObjectOfType<DialogueWindow>();
	}

    public void EnableRune(bool enable)
    {
        if(enable == true)
        {
            if(currentRune < runes.Length)
            {
                //if I get to it, do an animation here
                runes[currentRune].enabled = true;
                currentRune++;
                dw.DecreaseWordTracker();
            }
            else
            {
                Debug.Log("CurrentRune out of bounds");
            }
            
        }
        else
        {
            if (currentRune > 0)
            {
                //if I get to it, do an animation here
                runes[currentRune].enabled = false;
                currentRune--;
            }
            else
            {
                Debug.Log("CurrentRune out of bounds");
            }
        }
    }

    public int GetActiveRuneCount()
    {
        return currentRune;
    }

}
