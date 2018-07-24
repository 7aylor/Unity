using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleWeed : MonoBehaviour
{
    public Vector3 direction;
    public bool flipSprite;
    private float speed;
    private float timeSinceSpawn;
    private bool moving;
    private float startX;

    private SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        startX = transform.position.x;
        sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = flipSprite;
        ResetPosition();
    }

    private void OnBecameInvisible()
    {
        ResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(moving == true)
        {
            timeSinceSpawn += Time.deltaTime;
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

    private void ResetPosition()
    {
        timeSinceSpawn = 0;
        speed = Random.Range(2, 4f);
        moving = false;
        transform.position = new Vector3(startX, Mathf.Floor(Random.Range(-3f, 5f)), 0);
    }
}
