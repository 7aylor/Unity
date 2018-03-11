using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TreeSwayAnimation : MonoBehaviour {

    private Animator animator;
    private float startSway;
    private float timeSinceSpawn;
    private bool swaying;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        startSway = Random.Range(0, 0.5f);
        swaying = false;
        timeSinceSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!swaying && timeSinceSpawn > startSway)
        {
            animator.SetBool("Swaying", true);
            swaying = true;
        }
        timeSinceSpawn += Time.deltaTime;
	}
}
