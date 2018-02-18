using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private float moveSpeed = 0.075f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool facingX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        facingX = true;
    }

    // Update is called once per frame
    void Update () {
        //right
        if(Input.GetAxis("Horizontal") > 0)
        {
            if(facingX == true)
            {
                facingX = false;
                sprite.flipX = facingX;
            }
            
            transform.Translate(Vector2.right * moveSpeed);
        }
        //left
        if (Input.GetAxis("Horizontal") < 0)
        {
            if(facingX == false)
            {
                facingX = true;
                sprite.flipX = facingX;
            }
            
            transform.Translate(Vector2.left * moveSpeed);
        }
	}
}
