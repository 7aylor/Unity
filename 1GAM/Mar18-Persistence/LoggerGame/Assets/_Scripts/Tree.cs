using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Tree : MonoBehaviour {

    public int health;
    public enum maturity { seed, small, medium, large, multiple}
    public maturity treeState;
    public int lumberYielded;
    
    //these will need to be set and changed when the maturity changes
    public AnimatorOverrideController seedAnim;
    public AnimatorOverrideController smallAnim;
    public AnimatorOverrideController mediumAnim;
    public AnimatorOverrideController largeAnim;
    public AnimatorOverrideController multipleAnim;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        UpdateTreeStats();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateTreeStats()
    {
        switch (treeState)
        {
            case maturity.seed:
                health = -1;
                lumberYielded = 0;
                break;
            case maturity.small:
                health = 2;
                lumberYielded = 10;
                break;
            case maturity.medium:
                health = 4;
                lumberYielded = 20;
                break;
            case maturity.large:
                health = 8;
                lumberYielded = 50;
                break;
        }
    }

    private void DealDamage()
    {
        health -= 1;

        if(health <= 0)
        {
            //trigger falling animation
        }
    }
}
