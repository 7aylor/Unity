using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRunes : MonoBehaviour {

    private SpriteRenderer[] runes;
    private DialogueWindow dw;
    private int currentRune = 0;
    private Shaman shaman;

	// Use this for initialization
	void Start () {
        runes = transform.GetComponentsInChildren<SpriteRenderer>();
        dw = FindObjectOfType<DialogueWindow>();
        shaman = FindObjectOfType<Shaman>();
	}

    public void EnableRune(bool enable)
    {
        if(enable == true)
        {
            if(currentRune < runes.Length)
            {
                //if I get to it, do an animation here
                runes[currentRune].enabled = true;
                shaman.Summon(true); ///////CHECK ON THIS
                StartCoroutine("LerpColorAlpha", currentRune);
                currentRune++;
                dw.DecreaseWordTracker();
            }
            else
            {
                Debug.Log("CurrentRune out of bounds");
                //maybe spawn portal here?
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

    private IEnumerator LerpColorAlpha(int index)
    {

        Color spriteColor = runes[index].color;
        for (int i = 1; i <= 100; i++)
        {
            spriteColor.a = i / 100f;
            runes[index].color = spriteColor;
            yield return new WaitForEndOfFrame();
        }
        shaman.Summon(false);
    }

}
