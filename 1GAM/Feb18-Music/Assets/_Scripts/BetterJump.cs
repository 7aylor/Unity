using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour {

    private float fallMultiplyer = 2.5f;
    private float lowJumpMultiplyer = 2f;
    private float fallHarder = 4f;
    private Rigidbody2D rb;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplyer - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplyer - 1) * Time.deltaTime;
        }

        //down
        if (Input.GetAxis("Vertical") < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallHarder - 1) * Time.deltaTime; ;
        }
    }
}