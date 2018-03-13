using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class LumberJackAnimations : MonoBehaviour, IPointerClickHandler {

    private bool hasTarget; //used to determine if the lumberjack is walk toward a tree
    private Vector3 targetXY; //the target location of the next tree;
    private Animator animator;
    private GameObject selectionIndicator;
    private bool isSelected;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
            hasTarget = true;
            targetXY = Input.mousePosition;
            animator.SetTrigger("Jump");
            StartCoroutine(TravelToTarget());
        }
    }

    private IEnumerator TravelToTarget()
    {
        for(int i = 0; i < 20; i++)
        {
            transform.Translate(new Vector3(0, -0.05f, 0));
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
