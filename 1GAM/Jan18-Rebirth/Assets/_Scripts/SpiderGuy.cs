﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderGuy : MonoBehaviour {

    public AnimatorOverrideController up;
    public AnimatorOverrideController horizontal;
    public AnimatorOverrideController down;
    public float speed = 1;
    public float distanceToPlayer;
    public float distanceToChasePlayer;
    public GameObject soul;

    private Animator animator;
    private float timeToChangeState;
    private float timeSinceLastStateChange;
    private enum direction { up, down, left, right }
    private direction SpiderGuyDirection;
    public enum state { run, idle, attack };
    public state SpiderGuyState;
    private SpriteRenderer sprite;
    private bool isMoving = false;
    private bool chasingPlayer = false;
    private Transform playerTransform = null;
    private float timeSinceLastTargetStateChange = 0;
    private float timeToChangeTargetState = 1f;
    private int health = 3;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        ChangeStates();
    }

    // Update is called once per frame
    void Update ()
    {
        CheckDistanceToPlayer();

        if (chasingPlayer == false && timeSinceLastStateChange >= timeToChangeState)
        {
            ChangeStates();
        }
        else
        {
            timeSinceLastStateChange += Time.deltaTime;
            if (chasingPlayer == true)
            {
                timeSinceLastTargetStateChange += Time.deltaTime;
                TargetPlayer(playerTransform);
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckDistanceToPlayer()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= distanceToChasePlayer)
        {
            chasingPlayer = true;
            //Debug.DrawLine(transform.position, playerTransform.position, Color.red);
        }
        else
        {
            chasingPlayer = false;
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
        if(SpiderGuyState == state.run && isMoving == true)
        {
            Vector2 left = -transform.right;
            Vector2 right = transform.right;
            Vector2 up = transform.up;
            Vector2 down = -transform.up;

            if (SpiderGuyDirection == direction.left)
            {
                //transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                rb.MovePosition(rb.position + left * speed * Time.fixedDeltaTime);
            }
            if (SpiderGuyDirection == direction.right)
            {
                //transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                rb.MovePosition(rb.position + right * speed * Time.fixedDeltaTime);
            }
            if (SpiderGuyDirection == direction.up)
            {
                //transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
                rb.MovePosition(rb.position + up * speed * Time.fixedDeltaTime);
            }
            if (SpiderGuyDirection == direction.down)
            {
                //transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
                rb.MovePosition(rb.position + down * speed * Time.fixedDeltaTime);
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
        else if (SpiderGuyState == state.attack)
        {
            //Debug.Log("Attack Animation");
            isMoving = false;
            animator.SetBool("Run", false);
            animator.SetTrigger("Attack");
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

        playerTransform = caveManTransform;

        chasingPlayer = true;

        if(Vector2.Distance(transform.position, caveManTransform.position) < distanceToPlayer)
        {
            //Debug.DrawLine(transform.position, playerTransform.position, Color.red);
            SpiderGuyState = state.attack;
        }
        else
        {
            SpiderGuyState = state.run;
        }

        SetAnimations();
        CalculateDirectionToPlayer(playerTransform);
        SetAnimatorOverride();
    }

    private void UnTargetPlayer()
    {
        Debug.Log("Player out of Sight");
        chasingPlayer = false;
        ChangeStates();
    }

    private void CalculateDirectionToPlayer(Transform playerTransform)
    {
        float xDiff = transform.position.x - playerTransform.position.x;
        float yDiff = transform.position.y - playerTransform.position.y;

        if(timeSinceLastTargetStateChange >= timeToChangeTargetState)
        {
            timeSinceLastTargetStateChange = 0;

            if (Mathf.Abs(xDiff) > Mathf.Abs(yDiff))
            {
                if (xDiff > 0)
                {
                    SpiderGuyDirection = direction.left;
                }
                else
                {
                    SpiderGuyDirection = direction.right;
                }
            }
            else
            {
                if (yDiff > 0)
                {
                    SpiderGuyDirection = direction.down;
                }
                else
                {
                    SpiderGuyDirection = direction.up;
                }
            }
        }
    }

    public void InflictDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Instantiate(soul,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void CheckHitPlayer()
    {
        Vector2 boxSize = new Vector2(0.7f, 0.7f);
        RaycastHit2D hit;
        if(SpiderGuyDirection == direction.down && sprite.flipX == true) // looking right
        {
            hit = Physics2D.BoxCast(transform.position + (Vector3.right * 0.5f), boxSize, 0, Vector2.down, 8); //8 is player layer
            //Debug.DrawRay(transform.position - (Vector3.right * 0.5f) + new Vector3(boxSize.x / 2, 0), Vector2.down * boxSize.y, Color.red, 1);
            //Debug.DrawRay(transform.position - (Vector3.right * 0.5f) - new Vector3(boxSize.x / 2, 0), Vector2.down * boxSize.y, Color.red, 1);
        }
        else if (SpiderGuyDirection == direction.down && sprite.flipX == false) //looking left
        {
            hit = Physics2D.BoxCast(transform.position - (Vector3.right * 0.5f), boxSize, 0, Vector2.down, 8); //8 is player layer
            Debug.DrawRay(transform.position + (Vector3.right * 0.5f) + new Vector3(boxSize.x / 2, 0), Vector2.down * boxSize.y, Color.red, 1);
            Debug.DrawRay(transform.position + (Vector3.right * 0.5f) - new Vector3(boxSize.x / 2, 0), Vector2.down * boxSize.y, Color.red, 1);
        }
        else if (SpiderGuyDirection == direction.up && sprite.flipX == true) //right
        {
            hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.up, 8); //8 is player layer
            Debug.DrawRay(transform.position + new Vector3(boxSize.x / 2, 0), Vector2.up * boxSize.y, Color.red, 1);
            Debug.DrawRay(transform.position - new Vector3(boxSize.x / 2, 0), Vector2.up * boxSize.y, Color.red, 1);
        }
        else if (SpiderGuyDirection == direction.up && sprite.flipX == false) //left
        {
            hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.up, 8); //8 is player layer
            //Debug.DrawRay(transform.position + new Vector3(boxSize.x / 2, 0), Vector2.up * boxSize.y, Color.red, 1);
            //Debug.DrawRay(transform.position - new Vector3(boxSize.x / 2, 0), Vector2.up * boxSize.y, Color.red, 1);
        }
        else if (SpiderGuyDirection == direction.left)
        {
            hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.left, 8); //8 is player layer
            //Debug.DrawRay(transform.position + new Vector3(boxSize.y / 2, 0), Vector2.left * boxSize.x, Color.red, 1);
            //Debug.DrawRay(transform.position - new Vector3(boxSize.y / 2, 0), Vector2.left * boxSize.x, Color.red, 1);
        }
        else //if (SpiderGuyDirection == direction.right)
        {
            hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.right, 8); //8 is player layer
            //Debug.DrawRay(transform.position + new Vector3(boxSize.y / 2, 0), Vector2.right * boxSize.x, Color.red, 1);
            //Debug.DrawRay(transform.position - new Vector3(boxSize.y / 2, 0), Vector2.right * boxSize.x, Color.red, 1);
        }

        if(hit == true)
        {
            Debug.Log("Hit Player");
            //trigger hit animation and check for player death
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isMoving = false;
    }
}