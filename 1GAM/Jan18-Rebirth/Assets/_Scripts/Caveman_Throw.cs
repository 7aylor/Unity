using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Caveman_Throw : MonoBehaviour {

    public GameObject rock;
    public static bool isThrowing = false;

    private Animator animator;
    private AnimatorStateInfo currentAnimation;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && isThrowing == false)
        {
            isThrowing = true;
            animator.SetTrigger("Throw");
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
}
