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
    private Rigidbody rb;
    private RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickedUpItems = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
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

    /// <summary>
    /// Checks to see if the player is facing an item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool IsFacingItem(GameObject item)
    {
        return Physics.Raycast(transform.position, transform.forward, range);
    }

    /// <summary>
    /// Implements sprinting
    /// </summary>
    /// <returns></returns>
    private float IsSprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
        {
            return 20f;
        }
        else
        {
            return moveSpeed;
        }
    }
}
