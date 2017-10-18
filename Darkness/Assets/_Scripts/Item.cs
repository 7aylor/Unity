using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    private Player player;
    private Renderer r;
    private Color highlight = Color.red;
    private Material startColor;
    public Material[] colors;
    private bool canPickUp = false;
    private bool pickedUp = false;
    private Rigidbody rb;
    private float lockedY = 1;


    void Awake () {
        r = GetComponent<Renderer>();
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
        SetRandomColor();
	}

    private void Update()
    {
        //rotate the item for visual effect
        transform.Rotate(new Vector3(0.5f, 0.5f, 0.5f));

        PlayerInRange();
        PickUpItem();
    }

    /// <summary>
    /// Used on start to choose a random color for the item.
    /// </summary>
    private void SetRandomColor()
    {
        int index = Random.Range(0, colors.Length);
        startColor = colors[index];
        r.material = startColor;
    }

    /// <summary>
    /// Determines if an item can be picked up and performs that action, also dropping an item
    /// </summary>
    private void PickUpItem()
    {
        //if we hit the pickup button
        if (Input.GetButtonDown("PickUp"))
        {
            //if we are within range and hightlighted, and the mouse is clicked, pick up item
            if (canPickUp == true)
            {
                player.pickedUpItems.Add(gameObject);
                gameObject.transform.parent = player.transform;
                pickedUp = true;
                canPickUp = false;
                r.material = startColor;
                rb.velocity = Vector3.zero;
                transform.position = player.transform.position;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1.5f);
            }
            else if (pickedUp == true)
            {
                gameObject.transform.parent = null;
                pickedUp = false;
            }
        }
    }

    /// <summary>
    /// Checks if the player is in range of the item and allows you to pick up and drop items
    /// </summary>
    private void PlayerInRange()
    {

        if (pickedUp == false && player.IsFacingItem(this.gameObject) 
            && Vector3.Distance(player.transform.position, transform.position) < player.range)
        {
            canPickUp = true;
            r.material.color = highlight;
        }
        else
        {
            canPickUp = false;
            r.material = startColor;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Item collisionItem = collision.gameObject.GetComponent<Item>();

        if (collisionItem != null && collisionItem.startColor == startColor) {
            FindObjectOfType<VignetteController>().CombineItemsEvent();
            Destroy(collisionItem);
            Destroy(gameObject);
        }
    }
}
