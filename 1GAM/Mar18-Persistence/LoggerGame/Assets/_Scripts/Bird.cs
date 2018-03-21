using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public float speed;
    public bool isRight = true; //set by the spawner
    private int direction;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		if(isRight == true)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
            sprite.flipX = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(direction, Random.Range(-0.25f, 0.25f)) * speed * Time.deltaTime);
	}
}
