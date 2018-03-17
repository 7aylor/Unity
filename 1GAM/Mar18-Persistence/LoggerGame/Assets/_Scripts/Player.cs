using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour, IPointerClickHandler {

    public AnimatorOverrideController up;
    public AnimatorOverrideController down;
    public AnimatorOverrideController side;
    public GameObject flag;

    private float jumpSpeed = 0.05f;
    private bool isSelected;
    private bool hasTarget; //used to determine if the lumberjack is walking toward a tree
    private Vector3 targetXY; //the target location of the next tree;
    private Animator animator;
    private SpriteRenderer sprite;
    private GameObject selectionIndicator;
    private GameObject tempFlag;

    private enum direction { up, down, left, right };
    private direction travelDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        selectionIndicator = transform.GetChild(0).gameObject; //Gets the indicator child game object
    }

    void Start () {
        hasTarget = false;
        isSelected = false;
	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0) && isSelected == true && hasTarget == false)
        {
            Debug.Log("Clicked");
            //get the distance between the mouse and the player
            Vector3 distanceToMouse = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hasTarget = true;
            targetXY = Input.mousePosition;

            //check if we haven't clicked the player again
            if (Mathf.Abs(distanceToMouse.x) >= 0.5f || Mathf.Abs(distanceToMouse.y) >= 0.5f)
            {
                Debug.Log("Not clicked self");
                //travel on X
                if (Mathf.Abs(distanceToMouse.x) > Mathf.Abs(distanceToMouse.y))
                {
                    animator.runtimeAnimatorController = side;

                    if (distanceToMouse.x > 0)
                    {
                        tempFlag = Instantiate(flag, new Vector3(transform.position.x - 1, transform.position.y, 0), Quaternion.identity);
                        sprite.flipX = true;
                        StartCoroutine(TravelToTarget(direction.left));
                    }
                    else
                    {
                        tempFlag = Instantiate(flag, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity);
                        sprite.flipX = false;
                        StartCoroutine(TravelToTarget(direction.right));
                    }
                }
                //travel on Y
                else
                {
                    if (distanceToMouse.y > 0)
                    {
                        tempFlag = Instantiate(flag, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
                        animator.runtimeAnimatorController = down;
                        StartCoroutine(TravelToTarget(direction.down));
                    }
                    else
                    {
                        tempFlag = Instantiate(flag, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                        animator.runtimeAnimatorController = up;
                        StartCoroutine(TravelToTarget(direction.up));
                    }
                }

                animator.SetTrigger("Jump");
            }
        }
    }

    private IEnumerator TravelToTarget(direction dir)
    {
        Vector3 changeVector;
        if(dir == direction.down)
        {
            changeVector = new Vector3(0, -jumpSpeed, 0);
        }
        else if(dir == direction.up)
        {
            changeVector = new Vector3(0, jumpSpeed, 0);
        }
        else if (dir == direction.left)
        {
            changeVector = new Vector3(-jumpSpeed, 0, 0);
        }
        else
        {
            changeVector = new Vector3(jumpSpeed, 0, 0);
        }

        for (int i = 0; i < 20; i++)
        {
            transform.Translate(changeVector);
            yield return new WaitForSeconds(0.025f);
        }
        hasTarget = false;
        Destroy(tempFlag);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick called");

        if(GameManager.instance.playerSelected == false)
        {
            if (isSelected == true)
            {
                isSelected = false;
                GameManager.instance.playerSelected = false;
                hasTarget = false;
                selectionIndicator.SetActive(false);
            }
            else
            {
                GameManager.instance.playerSelected = true;
                isSelected = true;
                selectionIndicator.SetActive(true);
            }
        }
        else
        {
            //play can't select noise
            if (isSelected == true)
            {
                isSelected = false;
                GameManager.instance.playerSelected = false;
                hasTarget = false;
                selectionIndicator.SetActive(false);
            }
        }
    }
}
