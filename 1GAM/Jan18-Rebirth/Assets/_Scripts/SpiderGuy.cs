using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderGuy : MonoBehaviour {

    public AnimatorOverrideController up;
    public AnimatorOverrideController horizontal;
    public AnimatorOverrideController down;
    public float speed = 1;

    private Animator animator;
    private float timeToChangeState;
    private float timeSinceLastStateChange;
    private enum direction { up, down, left, right }
    private direction SpiderGuyDirection;
    private enum state { run, idle, attack };
    private state SpiderGuyState;
    private SpriteRenderer sprite;
    private bool isMoving = false;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        ChangeStates();
    }

    // Update is called once per frame
    void Update () {
        if(timeSinceLastStateChange >= timeToChangeState)
        {
            ChangeStates();
        }
        else
        {
            timeSinceLastStateChange += Time.deltaTime;
            Move();
        }
	}

    private void ChangeStates()
    {
        timeSinceLastStateChange = 0;
        timeToChangeState = GetRandomTime();
        SpiderGuyDirection = GetRandomDirection();
        SpiderGuyState = GetRandomState();
        SetAnimatorOverride();
        SetAnimations();
    }

    private float GetRandomTime()
    {
        return Random.Range(2f, 10f);
    }

    private direction GetRandomDirection()
    {
        int randomIndex = Random.Range(0, 4);
        return (direction)randomIndex;
    }

    private state GetRandomState()
    {
        int randomIndex = Random.Range(0, 3);
        return (state)randomIndex;
    }

    private void SetAnimatorOverride()
    {
        if(SpiderGuyDirection == direction.down)
        {
            animator.runtimeAnimatorController = down;
        }
        else if(SpiderGuyDirection == direction.up)
        {
            animator.runtimeAnimatorController = up;
        }
        else if(SpiderGuyDirection == direction.right)
        {
            sprite.flipX = false;
            animator.runtimeAnimatorController = horizontal;
        }
        else if (SpiderGuyDirection == direction.left)
        {
            sprite.flipX = true;
            animator.runtimeAnimatorController = horizontal;
        }
    }

    private void Move()
    {
        if(SpiderGuyState == state.run && isMoving)
        {
            if(SpiderGuyDirection == direction.left)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            }
            if (SpiderGuyDirection == direction.right)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            }
            if (SpiderGuyDirection == direction.up)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            }
            if (SpiderGuyDirection == direction.down)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            }
        }
    }

    private void SetAnimations()
    {
        if(SpiderGuyState == state.run)
        {
            isMoving = true;
            animator.SetBool("Run", true);
        }
        else
        {
            isMoving = false;
            animator.SetBool("Run", false);
        }
    }

    private void TargetPlayer(Transform caveManTransform)
    {
        Debug.Log("Player in Sight");

        SpiderGuyState = state.run;
        SetAnimations();

        //get the direction ie spiderGuy position minus caveman position to determine direction
        //set the animation
    }
}
