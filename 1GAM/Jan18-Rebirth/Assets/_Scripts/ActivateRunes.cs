using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRunes : MonoBehaviour {

    private SpriteRenderer[] runes;
    private DialogueWindow dw;
    private int currentRune = 0;
    private Shaman shaman;
    private SpawnPortal portal;

	// Use this for initialization
	void Start () {
        runes = transform.GetComponentsInChildren<SpriteRenderer>();
        dw = FindObjectOfType<DialogueWindow>();
        shaman = FindObjectOfType<Shaman>();
        portal = FindObjectOfType<SpawnPortal>();
	}

    public void EnableRune(bool enable)
    {
        if(enable == true)
        {
            if(currentRune < runes.Length)
            {
                Debug.Log("Spawning Rune" + currentRune);
                //if I get to it, do an animation here
                shaman.Talk(false);
                shaman.Summon(true);
                StartCoroutine("FadeInAlpha", currentRune);
                currentRune++;
                Debug.Log("After Spawn Rune" + currentRune);
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
                Debug.Log("Destroying Rune" + currentRune);
                //if I get to it, do an animation here
                shaman.Talk(false);
                shaman.Summon(true);
                currentRune--;
                StartCoroutine("FadeOutAlpha", currentRune);
                Debug.Log("After Destroy Rune" + currentRune);
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

    private IEnumerator FadeInAlpha(int index)
    {
        runes[index].enabled = true;
        Color spriteColor = runes[currentRune].color;
        for (int i = 1; i <= 100; i++)
        {
            spriteColor.a = i / 100f;
            runes[index].color = spriteColor;
            yield return new WaitForEndOfFrame();
        }
        shaman.Summon(false);
        if(currentRune >= 4)
        {
            Debug.Log("Spawning Portal");
            dw.SetWordTracker(12);
            dw.EnablePanel(true);
            portal.EnablePortal();
        }
    }

    private IEnumerator FadeOutAlpha(int index)
    {
        Debug.Log("FadeOutAlpha");
        Color spriteColor = runes[index].color;
        for (int i = 100; i >= 0; i--)
        {
            spriteColor.a = i / 100f;
            runes[index].color = spriteColor;
            yield return new WaitForEndOfFrame();
        }
        runes[index].enabled = false;
        //shaman.Summon(false);
    }
}
