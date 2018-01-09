﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caveman_Move : MonoBehaviour {

    public float speed = 1;

    private bool run_horiz = false;
    private bool run_down = false;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool run_up = false;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckForInput();
        HandleRunningAnimation();
    }

    private void HandleRunningAnimation()
    {
        if(run_horiz == true)
        {
            animator.SetBool("RunHoriz", true);
        }
        else
        {
            animator.SetBool("RunHoriz", false);
        }
        if (run_down == true)
        {
            animator.SetBool("RunDown", true);
        }
        else
        {
            animator.SetBool("RunDown", false);
        }
        if (run_up == true)
        {
            animator.SetBool("RunUp", true);
        }
        else
        {
            animator.SetBool("RunUp", false);
        }
    }

    private void CheckForInput()
    {
        if((Input.anyKey == true)){
            //left
            if (Input.GetKey(KeyCode.A))
            {
                run_horiz = true;
                run_down = false;
                run_up = false;
                sprite.flipX = true;
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            //right
            if (Input.GetKey(KeyCode.D))
            {
                run_horiz = true;
                run_down = false;
                run_up = false;
                sprite.flipX = false;
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            //down
            if (Input.GetKey(KeyCode.S))
            {
                run_down = true;
                run_horiz = false;
                run_up = false;
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            //up
            if (Input.GetKey(KeyCode.W))
            {
                run_up = true;
                run_horiz = false;
                run_down = false;
                transform.Translate(Vector3.up * speed * Time.deltaTime);
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