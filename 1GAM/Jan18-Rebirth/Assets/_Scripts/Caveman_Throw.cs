using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Caveman_Throw : MonoBehaviour {

    public GameObject rock;
    public static bool isThrowing = false;
    public AudioClip throwSound;

    private Animator animator;
    private AnimatorStateInfo currentAnimation;
    private bool canThrow;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
        CanThrow(true);
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canThrow == true && Input.GetMouseButtonDown(0) && isThrowing == false)
        {
            isThrowing = true;
            animator.SetTrigger("Throw");
            audio.clip = throwSound;
            audio.Play();
        }
	}

    public void ThrowProjectile()
    {
        Debug.Log("Throwing");
        GameObject clone = Instantiate(rock, transform.position, Quaternion.identity);
    }

    public void StopThrowing()
    {
        Debug.Log("Done Throwing");
        isThrowing = false;
    }

    public void CanThrow(bool canIThrow)
    {
        canThrow = canIThrow;
    }
}
