using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationAnimation : MonoBehaviour {

    private Animator animator;
    private bool isDown = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && isDown == false)
        {
            isDown = true;
            animator.SetTrigger("CrawlDown");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isDown == true)
        {
            isDown = false;
            animator.SetTrigger("CrawlUp");
        }

    }
}
