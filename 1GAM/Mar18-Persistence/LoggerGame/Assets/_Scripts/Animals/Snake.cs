using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    private Animator animator;
    private float waitToSpawn;
    private float timeKeeper;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        waitToSpawn = GetRandomTime();
        timeKeeper = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        //play the snake animation with random intervals
        if (timeKeeper >= waitToSpawn)
        {
            animator.SetTrigger("Slither");
            waitToSpawn = GetRandomTime();
            timeKeeper = 0;
        }
        timeKeeper += Time.deltaTime;
	}

    private float GetRandomTime()
    {
        return Random.Range(5, 30f);
    }
}
