using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Tree : MonoBehaviour {

    public int health;
    public enum maturity { seed, small, medium, large}
    public maturity treeState;
    public int lumberYielded;
    public bool canChopDown;
    
    //these will need to be set and changed when the maturity changes
    public AnimatorOverrideController seedAnim;
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        lumberCount = FindObjectOfType<Lumber>();
    }

    // Use this for initialization
    void Start () {
        InitializeTree();
        UpdateTreeStats();
        PickAnimationStartFrame();
    }

    private void PickAnimationStartFrame()
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);//could replace 0 by any other animation layer index
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
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
                health = 1;
                lumberYielded = 1;
                canChopDown = false;
                //sprite.sprite = seedSprite;
                animator.runtimeAnimatorController = seedAnim;
                break;
            case maturity.small:
                health = 3;
                lumberYielded = 10;
                //sprite.sprite = smallSprite;
                animator.runtimeAnimatorController = smallAnim;
                break;
            case maturity.medium:
                health = 6;
                lumberYielded = 20;
                //sprite.sprite = mediumSprite;
                animator.runtimeAnimatorController = mediumAnim;
                break;
            case maturity.large:
                health = 8;
                lumberYielded = 50;
                //sprite.sprite = largeSprite;
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
        }
    }
}
