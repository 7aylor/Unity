using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private float moveSpeed = 0.075f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(Vector2.right * moveSpeed);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(Vector2.left * moveSpeed);
        }
	}
}
