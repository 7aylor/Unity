using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caveman_Move : MonoBehaviour {

    public float speed = 1;

    private bool run_horiz = false;
    private Animator animator;
    private SpriteRenderer sprite;

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
    }

    private void CheckForInput()
    {
        //left
        if (Input.GetKey(KeyCode.A))
        {
            run_horiz = true;
            sprite.flipX = true;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        //right
        else if (Input.GetKey(KeyCode.D))
        {
            run_horiz = true;
            sprite.flipX = false;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            run_horiz = false;
        }
        
    }



}
