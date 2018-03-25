using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Tree : MonoBehaviour {

    public int health;
    public enum maturity { seed, small, medium, large, multiple}
    public maturity treeState;
    public int lumberYielded;
    public bool canChopDown;
    
    //these will need to be set and changed when the maturity changes
    public AnimatorOverrideController seedAnim;
    public AnimatorOverrideController smallAnim;
    public AnimatorOverrideController mediumAnim;
    public AnimatorOverrideController largeAnim;
    //public AnimatorOverrideController multipleAnim;

    private Player lumberjack;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        //InitializeTree();
        treeState = maturity.small;
        UpdateTreeStats();
	}

    private void InitializeTree()
    {
        treeState = (maturity)(Random.Range(0, 4));
        UpdateTreeStats();
    }

    private void UpdateTreeStats()
    {
        canChopDown = true;

        switch (treeState)
        {
            case maturity.seed:
                health = -1;
                lumberYielded = 0;
                canChopDown = false;
                //animator.runtimeAnimatorController = seedAnim;
                break;
            case maturity.small:
                health = 2;
                lumberYielded = 10;
                //animator.runtimeAnimatorController = smallAnim;
                break;
            case maturity.medium:
                health = 4;
                lumberYielded = 20;
                //animator.runtimeAnimatorController = mediumAnim;
                break;
            case maturity.large:
                health = 8;
                lumberYielded = 50;
                //animator.runtimeAnimatorController = largeAnim;
                break;
        }
    }

    private void DealDamage()
    {
        health -= 1;

        if(health <= 0)
        {
            lumberjack = GameObject.FindGameObjectWithTag("Lumberjack").GetComponent<Player>();
            animator.SetTrigger("Falling");
            lumberjack.ClearLumberjackAnimations();
        }
    }
}
