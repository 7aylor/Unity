using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {

    public AnimatorOverrideController up;
    public AnimatorOverrideController down;
    public AnimatorOverrideController side;

    public float speed;
    public float raycastDistance;

    private Animator animator;
    private SpriteRenderer sprite;

    private enum direction { up, down, left, right, none}

    [SerializeField]
    private direction myDirection;

    private Vector2 dirForce;

    private int maxTime = 3;
    private float spawnTime;
    private float timeSinceLastChange;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        animator.runtimeAnimatorController = side;
        myDirection = GetRandomDirection();
        UpdateAnimator();
        timeSinceLastChange = 0;
        spawnTime = GetRandomFloat();
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastChange += Time.deltaTime;

        //if(myDirection == direction.down && transform.position.y <= GameManager.instance.minWorldSpaceY && timeSinceLastChange > 1)
        //{
        //    RabbitReset(false);
        //}
        //else if (myDirection == direction.up && transform.position.y >= GameManager.instance.maxWorldSpaceY && timeSinceLastChange > 1)
        //{
        //    RabbitReset(false);
        //}
        //else if (myDirection == direction.left && transform.position.x <= GameManager.instance.minWorldSpaceX && timeSinceLastChange > 1)
        //{
        //    RabbitReset(false);
        //}
        //else if (myDirection == direction.right && transform.position.x >= GameManager.instance.maxWorldSpaceX && timeSinceLastChange > 1)
        //{
        //    RabbitReset(false);
        //}

        //move in the direction you are going, unless you have no direction
        if (myDirection != direction.none)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirForce, 0.25f, LayerMask.GetMask("River"));

            Debug.DrawLine(transform.position, transform.position + (Vector3)dirForce * 0.25f, Color.red);

            if (hit)
            {
                Debug.Log("River hit");

                //Vector2 tempDir = myDirection;
                RabbitReset(true);
            }

            transform.Translate(dirForce * Time.deltaTime);
        }

        //if time has elapsed, pick new direction
        if (timeSinceLastChange >= spawnTime)
        {
            RabbitReset(true);
        }
    }

    private void RabbitReset(bool random)
    {
        if (random)
        {
            myDirection = GetRandomDirection();
        }
        else
        {
            myDirection = GetOppositeDirection(myDirection);
        }
        
        UpdateAnimator();
        spawnTime = GetRandomFloat();
        timeSinceLastChange = 0;
    }

    private direction GetOppositeDirection(direction currentDirection)
    {
        switch (currentDirection)
        {
            case direction.up:
                return direction.down;
            case direction.down:
                return direction.up;
            case direction.left:
                return direction.right;
            case direction.right:
                return direction.left;
            default:
                return GetRandomDirection();
        }
    }

    /// <summary>
    /// returns up, down, left or right
    /// </summary>
    /// <returns></returns>
    private direction GetRandomDirection()
    {
        int rand = Random.Range(0, 10);

        if(rand >= 4)
        {
            return direction.none;
        }

        return (direction)rand;
    }

    /// <summary>
    /// Updates the animator and sprite direction
    /// </summary>
    private void UpdateAnimator()
    {
        animator.SetBool("Idle", false);
        if (myDirection == direction.right)
        {
            animator.runtimeAnimatorController = side;
            dirForce = Vector2.right * speed;
            sprite.flipX = false;
        }
        else if (myDirection == direction.left)
        {
            animator.runtimeAnimatorController = side;
            dirForce = Vector2.left * speed;
            sprite.flipX = true;
        }
        else if (myDirection == direction.down)
        {
            animator.runtimeAnimatorController = down;
            dirForce = Vector2.down * speed;
        }
        else if (myDirection == direction.up)
        {
            animator.runtimeAnimatorController = up;
            dirForce = Vector2.up * speed;
        }
        else if (myDirection == direction.none)
        {
            animator.SetBool("Idle", true);
            dirForce = Vector2.zero;
        }
    }

    private float GetRandomFloat()
    {
        return Random.Range(1, maxTime);
    }
}
