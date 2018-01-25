using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = collider.bounds.center;

        bool right = contactPoint.x > center.x;
        bool top = contactPoint.y > center.y;

        Debug.Log("Right " + right + ", Top: " + top);

        if(right == true)
        {
            
            collision.gameObject.transform.Translate(Vector2.right * 2);
        }
        else
        {
            collision.gameObject.transform.Translate(Vector2.left * 2);
        }

        if (top == true)
        {
            collision.gameObject.transform.Translate(Vector2.up * 2);
        }
        else
        {
            collision.gameObject.transform.Translate(Vector2.down * 2);
        }
    }
}
