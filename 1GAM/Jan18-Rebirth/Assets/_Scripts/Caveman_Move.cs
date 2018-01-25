using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caveman_Move : MonoBehaviour {

    public enum direction { up, down, left, right }
    public static direction CavemanDirection { get; private set; }
    public float speed = 1;
    public AnimatorOverrideController up;
    public AnimatorOverrideController horizontal;
    public AnimatorOverrideController down;

    private bool run_horiz = false;
    private bool run_down = false;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool run_up = false;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        CavemanDirection = direction.down;
        animator.runtimeAnimatorController = down;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
        HandleRunningAnimation();
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
                rb.MovePosition(Vector2.left * speed * Time.deltaTime);
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
                rb.MovePosition(Vector2.right * speed * Time.deltaTime);

            }
            //down
            else if (Input.GetAxis("Vertical") < 0)
            {
                run_down = true;
                run_horiz = false;
                run_up = false;
                CavemanDirection = direction.down;
                //transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
                rb.MovePosition(Vector2.down * speed * Time.deltaTime);

            }
            //up
            if (Input.GetAxis("Vertical") > 0)
            {
                run_up = true;
                run_horiz = false;
                run_down = false;
                CavemanDirection = direction.up;
                //transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
                rb.MovePosition(Vector2.up * speed * Time.deltaTime);
            }
        }
        else
        {
            run_horiz = false;
            run_down = false;
            run_up = false;
        }
    }
}
