using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 15f;
    public float rotateSpeed = 1f;
    public List<GameObject> pickedUpItems;
    public bool canTurn = true;
    public float range = 2f;
    public float sprintSpeed;
    private Rigidbody rb;
    private RaycastHit hit;
    private Color itemStartColor;
    private Renderer itemRenderer;
    private Color itemColor;
    


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickedUpItems = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.rotation * Vector3.forward, Color.cyan);
        MovePlayer();
        ChangeDirectionOnClick();
    }

    /// <summary>
    /// Moves the player with the keyboard inputs
    /// </summary>
    void MovePlayer()
    {
        float translationZ = Input.GetAxis("Vertical") * IsSprinting() * Time.deltaTime;
        float translationX = Input.GetAxis("Horizontal") * IsSprinting() * Time.deltaTime;
        transform.Translate(translationX, 0, translationZ);
    }

    /// <summary>
    /// rotates the player by left clicking
    /// </summary>
    void ChangeDirectionOnClick()
    {
        //on left click, rotate counter-clockwise
        if (canTurn == true && Input.GetMouseButton(0))
        {
            transform.Rotate(new Vector3(0, -rotateSpeed, 0));
        }
        //on right click, rotate clockwise
        if (canTurn == true && Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(0, rotateSpeed, 0));
        }
    }

    private void FixedUpdate()
    {
        IsFacingItem();
    }

    /// <summary>
    /// Checks to see if the player is facing an item and sets the items color to red if it is
    /// </summary>
    public void IsFacingItem()
    {

        Ray r = new Ray(transform.position, transform.rotation * Vector3.forward);
        RaycastHit hit;

        //Checks if we hit an item within our range and excluding the Spawner Layer maks
        if (Physics.Raycast(r, out hit, range, 9) && hit.collider.gameObject.GetComponent<Item>())
        {
            //Debug.DrawLine(r.origin, hit.point);
            //Debug.Log(hit.collider.gameObject.name);

            //set the item renderer stored locally
            itemRenderer = hit.collider.gameObject.GetComponent<Renderer>();

            //set the color to red
            if (itemRenderer.material.color != Color.red && itemRenderer.gameObject.GetComponent<Item>().pickedUp == false)
            {
                Debug.Log("changing colors");
                itemColor = itemRenderer.material.color;
                itemRenderer.material.color = Color.red;
                itemRenderer.gameObject.GetComponent<Item>().canPickUp = true;
            }
        }
        //otherwise we set the color back to the Item's startColor
        else
        {
            if(itemRenderer != null)
            {
                itemRenderer.material = itemRenderer.gameObject.GetComponent<Item>().startColor;
                itemRenderer.gameObject.GetComponent<Item>().canPickUp = false;
            }
        }
    }

    /// <summary>
    /// Implements sprinting
    /// </summary>
    /// <returns></returns>
    private float IsSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
        {
            return sprintSpeed;
        }
        else
        {
            return moveSpeed;
        }
    }
}
