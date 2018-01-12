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
            if (Caveman_Move.CavemanDirection == Caveman_Move.direction.down)
            {
                animator.SetTrigger("ThrowDown");
            }
            if (Caveman_Move.CavemanDirection == Caveman_Move.direction.up)
            {
                animator.SetTrigger("ThrowUp");
            }
            if (Caveman_Move.CavemanDirection == Caveman_Move.direction.left || Caveman_Move.CavemanDirection == Caveman_Move.direction.right)
            {
                animator.SetTrigger("ThrowHoriz");
            }
        }
	}

    public void ThrowProjectile()
    {
        GameObject clone = Instantiate(rock, transform.position, Quaternion.identity);
    }

    public void StopThrowing()
    {
        isThrowing = false;
    }
}
