using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Shaman : MonoBehaviour {

    private Animator animator;
    private Exclamation exclamation;
    private bool changeCursor = false;
    private ShamanDialogue dialogue;

	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
        exclamation = GetComponentInChildren<Exclamation>();
        dialogue = GetComponent<ShamanDialogue>();
	}
	
    public void Summon(bool enabled)
    {
        animator.SetBool("Summon", enabled);
    }

    private void Update()
    {
        if(SoulCounter.soulCount >= 5)
        {

            exclamation.Enabled(true);
            changeCursor = true;
        }
        else
        {
            exclamation.Enabled(false);

        }
    }

    private void OnMouseEnter()
    {
        if (changeCursor == true)
        {
            dialogue.SetDialogueCursor(true);
        }
    }

    private void OnMouseExit()
    {
        dialogue.SetDialogueCursor(false);
    }
}
