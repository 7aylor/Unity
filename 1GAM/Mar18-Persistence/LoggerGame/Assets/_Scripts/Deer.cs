using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Deer : MonoBehaviour {

    public float raycastDistance;
    public float runSpeed;

    private Vector2 runDirection;
    private Animator animator;
    private float destroyTime = 10;
    private float timeUntilDestroy = 0;

    private bool hit = false;

    private int playerLayerMask = 1 << 10;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(hit == false)
        {
            CheckToRun();
        }
	}

    /// <summary>
    /// Checks in all four directions if a player is near, then triggers a run
    /// </summary>
    private void CheckToRun()
    {

        //NEED TO WORK ON HOW TO DEAL WITH RIVER COLLISOIN

        //up
        if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), raycastDistance, playerLayerMask), Vector2.down))
        {
            return;
        }
        //down
        if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), raycastDistance, playerLayerMask), Vector2.up))
        {
            return;
        }
        //left
        else if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), raycastDistance, playerLayerMask), Vector2.right))
        {
            return;
        }
        //right
        else if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), raycastDistance, playerLayerMask), Vector2.left)){
            return;
        }
        //upper right
        else if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector3(1.414f, 1.414f)), raycastDistance, playerLayerMask), Vector2.left))
        {
            return;
        }
        //upper left
        else if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector3(-1.414f, 1.414f)), raycastDistance, playerLayerMask), Vector2.right))
        {
            return;
        }
        //lower left
        else if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector3(-1.414f, -1.414f)), raycastDistance, playerLayerMask), Vector2.right))
        {
            return;
        }
        //lower right
        else if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(new Vector3(1.414f, -1.414f)), raycastDistance, playerLayerMask), Vector2.left))
        {
            return;
        }
    }

    private bool CheckHit(RaycastHit2D hitPlayer, Vector2 direction)
    {
        //right
        if (hitPlayer.collider != null && hitPlayer.collider.GetComponent<Player>() != null)
        {
            hit = true;
            runDirection = direction;
            StartCoroutine(Run());
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Run()
    {
        //play animation
        animator.SetBool("Running", true);

        while(timeUntilDestroy <= destroyTime)
        {
            transform.Translate(runDirection * runSpeed * Time.deltaTime);
            timeUntilDestroy += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    float distanceX = transform.position.x - collision.gameObject.transform.position.x;
    //    float distanceY = transform.position.y - collision.gameObject.transform.position.y;

    //    //run left
    //    if (distanceX >= 0 && distanceX > distanceY)
    //    {
    //        runDirection = Vector2.left;
    //    }
    //    //run right
    //    else if (distanceX < 0 && distanceX > distanceY)
    //    {
    //        runDirection = Vector2.right;
    //    }
    //    //run down
    //    else if (distanceY >= 0 && distanceX < distanceY)
    //    {
    //        runDirection = Vector2.down;
    //    }
    //    else if(distanceY < 0 && distanceX < distanceY)
    //    {
    //        runDirection = Vector2.up;
    //    }

    //    StartCoroutine(Run());
    //}


}
