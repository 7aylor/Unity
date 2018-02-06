using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamDispenser : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
    public void PlayDispenseAnimation(bool play)
    {
        animator.SetBool("Dispensing", play);
    }

    public void SetPlaySpeed(float newSpeed)
    {
        animator.speed = newSpeed;
    }
}
