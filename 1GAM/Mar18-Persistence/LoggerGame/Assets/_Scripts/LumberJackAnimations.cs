using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class LumberJackAnimations : MonoBehaviour, IPointerClickHandler {

    public AnimatorOverrideController up;
    public AnimatorOverrideController down;
    public AnimatorOverrideController side;

    private bool hasTarget; //used to determine if the lumberjack is walk toward a tree
    private Vector3 targetXY; //the target location of the next tree;
    private Animator animator;
    private GameObject selectionIndicator;
    private bool isSelected;
    private SpriteRenderer sprite;

    private enum direction { up, down, left, right };
    private direction travelDirection;
    private float jumpSpeed = 0.05f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        selectionIndicator = transform.GetChild(0).gameObject; //Gets the indicator child game object
    }

    // Use this for initialization
    void Start () {
        hasTarget = false;
        isSelected = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (isSelected == true && hasTarget == false && Input.GetMouseButtonDown(0))
        {
            Vector3 distanceToMouse = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hasTarget = true;
            targetXY = Input.mousePosition;
            animator.SetTrigger("Jump");

            //travel on X
            if (Mathf.Abs(distanceToMouse.x) > Mathf.Abs(distanceToMouse.y))
            {
                animator.runtimeAnimatorController = side;

                if (distanceToMouse.x > 0)
                {
                    sprite.flipX = true;
                    StartCoroutine(TravelToTarget(direction.left));
                }
                else
                {
                    sprite.flipX = false;
                    StartCoroutine(TravelToTarget(direction.right));
                }
                
            }
            //travel on Y
            else
            {
                if (distanceToMouse.y > 0)
                {
                    animator.runtimeAnimatorController = down;
                    StartCoroutine(TravelToTarget(direction.down));
                }
                else
                {
                    animator.runtimeAnimatorController = up;
                    StartCoroutine(TravelToTarget(direction.up));
                }
            }

            animator.SetBool("Jump", true);
        }
    }

    private IEnumerator TravelToTarget(direction dir)
    {
        Vector3 changeVector;
        if(dir == direction.down)
        {
            changeVector = new Vector3(0, -jumpSpeed, 0);
            Debug.Log(animator.runtimeAnimatorController);
        }
        else if(dir == direction.up)
        {
            changeVector = new Vector3(0, jumpSpeed, 0);
            Debug.Log(animator.runtimeAnimatorController);
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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSelected == true)
        {
            isSelected = false;
            selectionIndicator.SetActive(false);
        }
        else
        {
            isSelected = true;
            selectionIndicator.SetActive(true);
        }
    }
}
