using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Deer : MonoBehaviour {

    public float raycastDistance;
    public float runSpeed;

    private Animator animator;
    private SpriteRenderer sprite;
    private float destroyTime = 10;
    private float timeUntilDestroy = 0;

    private bool hit = false;
    private int xPos;
    private int yPos;
    private int playerLayerMask = 1 << 10;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
        if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), raycastDistance, playerLayerMask), Vector2.right))
        {
            return;
        }
        //down
        if (CheckHit(Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), raycastDistance, playerLayerMask), Vector2.left))
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

    private void CheckForRiver(Vector2 direction)
    {
        //if(Physics2D.Raycast(transform.position, direction, 1, playerLayerMask) )
    }

    private bool CheckHit(RaycastHit2D hitPlayer, Vector2 direction)
    {
        //right
        if (hitPlayer.collider != null && hitPlayer.collider.GetComponent<Player>() != null)
        {
            hit = true;

            if(direction == Vector2.right)
            {
                sprite.flipX = true;
            }

            StartCoroutine(Run(direction));
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Run(Vector2 runDirection)
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

    //private Vector2Int GetDeerPositionInMap()
    //{
    //    return new Vector2Int(
    //      x + GameManager.instance.sizeX * 2,
    //    );
    //    yPos = (float)y - sizeY / 2 + 0.8f;
    //}

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
