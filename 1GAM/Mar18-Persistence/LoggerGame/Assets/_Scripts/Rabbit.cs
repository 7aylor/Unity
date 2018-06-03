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

    private int maxTime = 5;
    private int numHitsInTimePeriod = 0;
    private float timeToChangeDir;
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
        timeToChangeDir = GetRandomFloat();
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceLastChange += Time.deltaTime; 

        //move in the direction you are going, unless you have no direction
        if (myDirection != direction.none)
        {
            float distance = 0.1f;

            //check what is in front of the rabbit with a raycast
            RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dirForce * distance, dirForce, distance);

            Debug.DrawLine(transform.position, transform.position + (Vector3)dirForce * distance, Color.red);

            //if we don't hit a grass tile, change directions
            if (hit.collider != null && hit.collider.tag != "Grass")
            {
                numHitsInTimePeriod++;

                if(numHitsInTimePeriod > 2)
                {
                    SetDirection(direction.none);
                }
                else
                {
                    SetRandomDirection();
                }
                return;
            }

            transform.Translate(dirForce * Time.deltaTime);
        }

        //if time has elapsed, pick new direction
        if (timeSinceLastChange >= timeToChangeDir)
        {
            SetRandomDirection();
        }
    }

    /// <summary>
    /// Resets animator and values when an animation changes. Should be called from direction change methods
    /// </summary>
    private void Reset()
    {   
        UpdateAnimator();
        timeToChangeDir = GetRandomFloat();
        timeSinceLastChange = 0;
        numHitsInTimePeriod = 0;
    }

    /// <summary>
    /// Set rabbit direction to a random direction
    /// </summary>
    private void SetRandomDirection()
    {
        myDirection = GetRandomDirection();
        Reset();
    }

    /// <summary>
    /// Set rabbit direction to given argument
    /// </summary>
    /// <param name="newDir"></param>
    private void SetDirection(direction newDir)
    {
        myDirection = newDir;
        Reset();
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
