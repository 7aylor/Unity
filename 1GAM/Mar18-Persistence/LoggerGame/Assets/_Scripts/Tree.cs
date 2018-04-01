using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Tree : MonoBehaviour {

    public int health;
    public enum maturity { seed, tiny, small, medium, large}
    public maturity treeState;
    public int lumberYielded;
    public bool canChopDown;
    public float deathChance;
    public float minTimeToGrow;
    public float maxTimeToGrow;
    public int waterCount;


    //these will need to be set and changed when the maturity changes
    public AnimatorOverrideController seedAnim;
    public AnimatorOverrideController tinyAnim;
    public AnimatorOverrideController smallAnim;
    public AnimatorOverrideController mediumAnim;
    public AnimatorOverrideController largeAnim;
    //public AnimatorOverrideController multipleAnim;

    //public Sprite seedSprite;
    //public Sprite smallSprite;
    //public Sprite mediumSprite;
    //public Sprite largeSprite;

    private Player lumberjack;

    private Animator animator;
    private SpriteRenderer sprite;
    private Lumber lumberCount;
    private IncreaseResource increaseLumberObj;

    private float timeSinceLastGrowth;
    private float timeToGrow;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        lumberCount = FindObjectOfType<Lumber>();
        sprite = GetComponent<SpriteRenderer>();
        increaseLumberObj = FindObjectOfType<IncreaseResource>();
        InitializeTree();
        UpdateTreeStats();
        PickAnimationStartFrame();
    }

    private void Start()
    {
        timeSinceLastGrowth = 0;
        timeToGrow = GetNewGrowthTime();
        waterCount = 0;
    }

    private void Update()
    {
        if(treeState != maturity.seed && treeState != maturity.large)
        {
            //check for growth time, then grow and reset timers
            if(timeSinceLastGrowth >= timeToGrow)
            {
                GrowTree();
                timeSinceLastGrowth = 0;
                timeToGrow = GetNewGrowthTime();
            }
            timeSinceLastGrowth += Time.deltaTime;
        }

        //check if the tree dies
        if(treeState == maturity.large && CheckTreeDeath() == true)
        {
            animator.SetTrigger("Falling");
        }
    }

    private float GetNewGrowthTime()
    {
        return Random.Range(minTimeToGrow, maxTimeToGrow);
    }

    private bool CheckTreeDeath()
    {
        return Random.Range(0, 1f) < deathChance;
    }

    /// <summary>
    /// Used by the planter when he plants a tree
    /// </summary>
    public void StartAsSeed()
    {
        treeState = maturity.seed;
        UpdateTreeStats();
    }

    private void PickAnimationStartFrame()
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);//could replace 0 by any other animation layer index
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }

    private void InitializeTree()
    {
        treeState = (maturity)(Random.Range(1, 5));
        UpdateTreeStats();
    }

    private void UpdateTreeStats()
    {
        canChopDown = true;

        switch (treeState)
        {
            case maturity.seed:
                health = 1;
                lumberYielded = 0;
                canChopDown = false;
                animator.runtimeAnimatorController = seedAnim;
                //sprite.sortingOrder = 2;
                break;
            case maturity.tiny:
                health = 2;
                lumberYielded = 3;
                canChopDown = false;
                animator.runtimeAnimatorController = tinyAnim;
                //sprite.sortingOrder = 2;
                break;
            case maturity.small:
                health = 3;
                lumberYielded = 10;
                animator.runtimeAnimatorController = smallAnim;
                break;
            case maturity.medium:
                health = 6;
                lumberYielded = 20;
                animator.runtimeAnimatorController = mediumAnim;
                break;
            case maturity.large:
            default:
                health = 8;
                lumberYielded = 50;
                animator.runtimeAnimatorController = largeAnim;
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
            lumberCount.UpdateLumberCount(lumberYielded);
            increaseLumberObj.SetIncreaseResourceText(lumberYielded);
        }
    }

    public void GrowTree()
    {
            treeState += 1;
            UpdateTreeStats();
    }
}
