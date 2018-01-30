using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebirth : MonoBehaviour {

    private Animator animator;
    private DialogueWindow dw;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        dw = FindObjectOfType<DialogueWindow>();
	}

    public void StartRebirthAnimation()
    {
        animator.SetTrigger("Rebirth");
        dw.SetWordTracker(11);
    }

}
