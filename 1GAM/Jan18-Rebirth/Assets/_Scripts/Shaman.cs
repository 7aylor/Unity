using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
	}
	
    public void Summon(bool enabled)
    {
        animator.SetBool("Summon", enabled);
    }

}
