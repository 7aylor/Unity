using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bird : MonoBehaviour {

    public float speed;
    public bool isRight; //set by the spawner
    private int direction;
    private SpriteRenderer sprite;
    private float timeToLive;
    private float timeSinceBirth;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        //spawning from the right
		if(isRight == true)
        {
            direction = -1;
            sprite.flipX = true;
        }
        //spawning from the left
        else
        {
            direction = 1;
        }

        timeToLive = 60;
        timeSinceBirth = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(direction, Random.Range(-1f, 1f)) * speed * Time.deltaTime);

        if (timeSinceBirth >= timeToLive)
        {
            Destroy(gameObject);
        }

        timeSinceBirth += Time.deltaTime;
	}


}
