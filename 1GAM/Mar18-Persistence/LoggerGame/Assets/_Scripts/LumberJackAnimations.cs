using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LumberJackAnimations : MonoBehaviour {

    private bool hasTarget; //used to determine if the lumberjack is walk toward a tree
    private Vector3 targetXY; //the target location of the next tree;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        hasTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(hasTarget == false && Input.GetMouseButtonDown(0))
        {
            hasTarget = true;
            targetXY = Input.mousePosition;
            animator.SetTrigger("JumpForward");
            StartCoroutine(TravelToTarget());
        }
	}

    private IEnumerator TravelToTarget()
    {
        for(int i = 0; i < 20; i++)
        {
            transform.Translate(new Vector3(0, -0.05f, 0));
            yield return new WaitForSeconds(0.05f);
        }
    }
}
