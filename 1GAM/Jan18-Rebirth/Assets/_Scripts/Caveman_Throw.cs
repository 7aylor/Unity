using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Caveman_Throw : MonoBehaviour {

    private Animator animator;
    private AnimatorStateInfo currentAnimation;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
