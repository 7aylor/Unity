using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {

    public AnimatorOverrideController up;
    public AnimatorOverrideController down;
    public AnimatorOverrideController side;

    public float speed;

    private Animator animator;
    private SpriteRenderer sprite;

    private enum direction { up, down, left, right}

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

        transform.Translate(dirForce * Time.deltaTime);

        if (timeSinceLastChange >= spawnTime)
        {
            myDirection = GetRandomDirection();
            UpdateAnimator();
            spawnTime = GetRandomFloat();
            timeSinceLastChange = 0;
        }
    }

    /// <summary>
    /// returns up, down, left or right
    /// </summary>
    /// <returns></returns>
    private direction GetRandomDirection()
    {
        return (direction)Random.Range(0, 4);
    }

    /// <summary>
    /// Updates the animator and sprite direction
    /// </summary>
    private void UpdateAnimator()
    {
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
    }

    private float GetRandomFloat()
    {
        return Random.Range(1, maxTime);
    }
}
