using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebirth : MonoBehaviour {

    private Animator animator;
    private DialogueWindow dw;
    private Shaman shaman;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        dw = FindObjectOfType<DialogueWindow>();
        shaman = FindObjectOfType<Shaman>();
	}

    public void StartRebirthAnimation()
    {
        animator.SetTrigger("Rebirth");
        shaman.Summon(true);
        dw.SetWordTracker(9);
    }
}