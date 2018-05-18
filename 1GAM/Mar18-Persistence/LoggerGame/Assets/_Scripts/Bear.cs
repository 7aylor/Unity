using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour {

    private SpriteRenderer sprite;
    private BearController bearController;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        bearController = transform.parent.GetComponent<BearController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null && player.doneJumping == true)
        {
            sprite.enabled = true;
            sprite.sortingOrder = 1;
            Invoke("DisableSpriteAndMoveBear", 3f);
            player.PlayerRunsFromBear();
        }
    }

    private void DisableSpriteAndMoveBear()
    {
        sprite.enabled = false;
        sprite.sortingOrder = -1;
        bearController.MoveBear();
    }
}