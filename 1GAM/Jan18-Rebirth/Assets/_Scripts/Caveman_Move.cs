using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caveman_Move : MonoBehaviour {

    public enum direction { up, down, left, right }
    public static direction CavemanDirection { get; private set; }
    public float speed;
    public AnimatorOverrideController up;
    public AnimatorOverrideController horizontal;
    public AnimatorOverrideController down;
    public AudioClip leftFoot;
    public AudioClip rightFoot;

    private bool run_horiz = false;
    private bool run_down = false;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool run_up = false;
    private Rigidbody2D rb;
    private AudioSource audio;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        CavemanDirection = direction.down;
        animator.runtimeAnimatorController = down;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
        HandleRunningAnimation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void PlayLeftFootSound()
    {
        audio.clip = leftFoot;
        audio.Play();
    }

    public void PlayRightFootSound()
    {
        audio.clip = rightFoot;
        audio.Play();
    }

    private void HandleRunningAnimation()
    {
        if (run_horiz == true)
        {
            animator.runtimeAnimatorController = horizontal;
            animator.SetBool("Run", true);
        }
        else if (run_down == true)
        {
            animator.runtimeAnimatorController = down;
            animator.SetBool("Run", true);
        }
        else if (run_up == true)
        {
            animator.runtimeAnimatorController = up;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void Move()
    {
        Vector2 left = -transform.right;
        Vector2 right = transform.right;
        Vector2 up = transform.up;
        Vector2 down = -transform.up;

        if(run_horiz == true && CavemanDirection == direction.left)
        {
            rb.MovePosition(rb.position + left * speed * Time.fixedDeltaTime);
        }
        else if (run_horiz == true && CavemanDirection == direction.right)
        {
            rb.MovePosition(rb.position + right * speed * Time.fixedDeltaTime);
        }
        else if (run_up == true && CavemanDirection == direction.up)
        {
            rb.MovePosition(rb.position + up * speed * Time.fixedDeltaTime);
        }
        else if (run_down == true && CavemanDirection == direction.down)
        {
            rb.MovePosition(rb.position + down * speed * Time.fixedDeltaTime);
        }
    }

    private void CheckForInput()
    {
        if ((Input.anyKey == true && Caveman_Throw.isThrowing == false))
        {
            //left
            if (Input.GetAxis("Horizontal") < 0)
            {
                run_horiz = true;
                run_down = false;
                run_up = false;
                sprite.flipX = true;
                CavemanDirection = direction.left;
                //transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            }
            //right
            else if (Input.GetAxis("Horizontal") > 0)
            {
                run_horiz = true;
                run_down = false;
                run_up = false;
                sprite.flipX = false;
                CavemanDirection = direction.right;
                //transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            }
            //down
            else if (Input.GetAxis("Vertical") < 0)
            {
                run_down = true;
                run_horiz = false;
                run_up = false;
                CavemanDirection = direction.down;
                //transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            }
            //up
            if (Input.GetAxis("Vertical") > 0)
            {
                run_up = true;
                run_horiz = false;
                run_down = false;
                CavemanDirection = direction.up;
                //transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            run_horiz = false;
            run_down = false;
            run_up = false;
        }
    }
}
