using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

    private Animator animator;
    private float animatorPlaySpeed;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animatorPlaySpeed = animator.speed;
	}

    public void ChangePlaySpeed(float deltaSpeed)
    {
        animatorPlaySpeed += deltaSpeed;
    }

    public void PauseConveyorBelt()
    {
        animator.speed = 0;
    }

    public void UnpauseConveyorBelt()
    {
        animator.speed = animatorPlaySpeed;
    }
}
