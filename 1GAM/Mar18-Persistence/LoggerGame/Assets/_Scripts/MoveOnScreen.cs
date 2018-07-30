using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnScreen : MonoBehaviour
{
    public Vector3 direction;
    public bool randomizePosition;
    public bool waitToSpawn;
    private float speed;
    private bool moving;
    private float startX;
    private Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        startX = transform.position.x;
        startPosition = transform.position;
        ResetPosition();
    }

    private void OnBecameInvisible()
    {
        ResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(waitToSpawn == true)
        {
            if(moving == true)
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else
            {
                if(Random.Range(0, 1f) < 0.001f)
                {
                    moving = true;
                }
            }
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void ResetPosition()
    {
        speed = Random.Range(2, 4f);
        moving = false;
        if(randomizePosition == true)
        {
            transform.position = new Vector3(startX, Mathf.Floor(Random.Range(-3f, 5f)), 0);
        }
        else
        {
            transform.position = startPosition;
        }
    }
}
