using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossAnimator : MonoBehaviour {

    private enum Direction { Right, Left}
    private Direction direction;
    private Animator animator;
    private float scaleX;
    private bool flipDirection = false;


	// Use this for initialization
	void Start () {
        direction = GetRandomDirection();
        animator = GetComponent<Animator>();
        scaleX = transform.localScale.x;
	}

    private void Update()
    {
        HandleWalk();
    }

    private Direction GetRandomDirection()
    {
        int randDir = Random.Range(0, 2);
        return (Direction)randDir;
    }

    private void HandleWalk()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            if (direction == Direction.Right)
            {
                transform.Translate(Vector3.right * Time.deltaTime);

                if(flipDirection == false)
                {
                    transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
                    direction = Direction.Left;
                    flipDirection = true;
                }
            }
            else if((direction == Direction.Left))
            {
                transform.Translate(Vector3.left * Time.deltaTime);
                if(flipDirection == false)
                {
                    transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
                    direction = Direction.Right;
                    flipDirection = true;
                }
            }
        }
        else
        {
            flipDirection = false;
        }
    }
}
