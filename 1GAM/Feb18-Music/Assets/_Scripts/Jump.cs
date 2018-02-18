using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    private float jumpVelocity = 10;
    private Rigidbody2D rb;
    private int jumpCount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            if(jumpCount < 2)
            {
                jumpCount++;
                rb.velocity = Vector2.up * jumpVelocity;
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //layer 8 is a barrier
        if(collision.gameObject.layer != 8)
        {
            jumpCount = 0;
        }
    }
}
