using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ActionPanel : MonoBehaviour {

    private Animator animator;
    private bool isUp;
    private GameObject hireActions;
    private GameObject lumberjackActions;
    private GameObject planterActions;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hireActions = transform.GetChild(0).gameObject;
        lumberjackActions = transform.GetChild(1).gameObject;
        planterActions = transform.GetChild(2).gameObject;
    }

    // Use this for initialization
    void Start () {
        isUp = false;
        EnableHireActionPanel();
	}
	
    public void Animate()
    {
        if(isUp == true)
        {
            animator.SetTrigger("Slide_Down");
            isUp = false;
        }
        else
        {
            animator.SetTrigger("Slide_Up");
            isUp = true;
        }
    }

    public void EnableHireActionPanel()
    {
        hireActions.SetActive(true);
        lumberjackActions.SetActive(false);
        planterActions.SetActive(false);
    }

    public void EnableLumberJackActionPanel()
    {
        hireActions.SetActive(false);
        lumberjackActions.SetActive(true);
        planterActions.SetActive(false);
    }
    public void EnablePlanterActionPanel()
    {
        hireActions.SetActive(false);
        lumberjackActions.SetActive(false);
        planterActions.SetActive(true);
    }
}
