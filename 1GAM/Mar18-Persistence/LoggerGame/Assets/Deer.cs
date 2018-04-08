using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour {

    public float raycastDistance;
    public float runSpeed;

    private Vector2 runDirection;

    private float destroyTime = 10;
    private float timeUntilDestroy = 0;

    private bool hit = false;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(hit == false)
        {
            //CheckToRun();
        }
	}

    /// <summary>
    /// Checks in all four directions if a player is near, then triggers a run
    /// </summary>
    private void CheckToRun()
    {

        RaycastHit2D hitPlayer;

        hitPlayer = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance);
        
        //up
        if (hitPlayer.collider.gameObject.GetComponent<Player>() != null)
        {
            hit = true;
            Debug.Log("Up");
            runDirection = Vector2.down;
            StartCoroutine(Run());
            return;
        }

        hitPlayer = Physics2D.Raycast(transform.position, Vector2.down);
        
        //down 
        if (hitPlayer.collider.gameObject.GetComponent<Player>() != null)
        {
            hit = true;
            Debug.Log("Down");
            runDirection = Vector2.up;
            StartCoroutine(Run());
            return;
        }

        hitPlayer = Physics2D.Raycast(transform.position, Vector2.left, raycastDistance);

        //left
        if (hitPlayer.collider.gameObject.GetComponent<Player>() != null)
        {
            hit = true;
            Debug.Log("Left");
            runDirection = Vector2.right;
            StartCoroutine(Run());
            return;
        }

        hitPlayer = Physics2D.Raycast(transform.position, Vector2.right, raycastDistance);

        if (hitPlayer.collider.gameObject.GetComponent<Player>() != null)
        {
            hit = true;
            Debug.Log("Right");
            runDirection = Vector2.left;
            StartCoroutine(Run());
            return;
        }

    }

    private IEnumerator Run()
    {
        //play animation
        while(timeUntilDestroy <= destroyTime)
        {
            transform.Translate(runDirection * runSpeed * Time.deltaTime);
            timeUntilDestroy += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float distanceX = transform.position.x - collision.gameObject.transform.position.x;
        float distanceY = transform.position.y - collision.gameObject.transform.position.y;

        //run left
        if (distanceX >= 0 && distanceX > distanceY)
        {
            runDirection = Vector2.left;
        }
        //run right
        else if (distanceX < 0 && distanceX > distanceY)
        {
            runDirection = Vector2.right;
        }
        //run down
        else if (distanceY >= 0 && distanceX < distanceY)
        {
            runDirection = Vector2.down;
        }
        else if(distanceY < 0 && distanceX < distanceY)
        {
            runDirection = Vector2.up;
        }

        StartCoroutine(Run());
    }


}
