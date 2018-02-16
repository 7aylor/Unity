using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    private float jumpVelocity = 10;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
	}
}
