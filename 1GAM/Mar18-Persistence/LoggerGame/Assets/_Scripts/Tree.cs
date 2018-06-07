using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tree : MonoBehaviour {

    public int health;
    public enum maturity { seed, tiny, small, medium, large, stump}
    [SerializeField]
    public maturity treeState;
    public int lumberYielded;
    public bool canChopDown;
    public float deathChance;
    public float minTimeToGrow;
    public float maxTimeToGrow;
    public int waterCount;
    public GameObject grassTile;

    public AnimatorOverrideController seedAnim;
    public AnimatorOverrideController tinyAnim;
    public AnimatorOverrideController smallAnim;
    public AnimatorOverrideController mediumAnim;
    public AnimatorOverrideController largeAnim;

    private Player lumberjack;

    private Animator animator;
    private SpriteRenderer sprite;
    private Lumber lumberCount;
    private ForestHealth forestHealth;

    private float timeSinceLastGrowth;
    private float timeToGrow;
    private int stumpHealth;

    [SerializeField]
    private Vector2Int pos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        lumberCount = FindObjectOfType<Lumber>();
        sprite = GetComponent<SpriteRenderer>();
        forestHealth = FindObjectOfType<ForestHealth>();
        GameManager.instance.numTreesInPlay++;
        InitializeTree();
        UpdateTreeStats();
        PickAnimationStartFrame();
    }

    private void Start()
    {
        timeSinceLastGrowth = 0;
        timeToGrow = GetNewGrowthTime();
        waterCount = 0;
        pos.x = GameManager.instance.WorldCoordToArrayCoordX(transform.position.x);
        pos.y = GameManager.instance.WorldCoordToArrayCoordY(transform.position.y);
        GameManager.instance.map[pos.x, pos.y] = (int)MapGenerator.tileType.tree;
    }

    private void Update()
    {
        if(treeState != maturity.seed && treeState != maturity.large && treeState != maturity.stump)
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
            InitializeStump();
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

    /// <summary>
    /// randomly starts the swaying in the wind
    /// </summary>
    private void PickAnimationStartFrame()
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);//could replace 0 by any other animation layer index
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }

    /// <summary>
    /// create the tree in a random state, only used when the level is loaded
    /// </summary>
    private void InitializeTree()
    {
        treeState = (maturity)(Random.Range(1, 5));
        UpdateTreeStats();
    }

    /// <summary>
    /// assign tree states and animation states
    /// </summary>
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
                lumberYielded = 5;
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

    /// <summary>
    /// Deal damage to the tree when the lumberjack chops
    /// </summary>
    private void DealDamage()
    {
        health -= 1;

        if(health <= 0)
        {
            //need to find lumberjack in case when the tree is spawned a lumberjack isn't present
            if (lumberjack == null)
            {
                lumberjack = GameObject.FindGameObjectWithTag("Lumberjack").GetComponent<Player>();
            }

            animator.SetTrigger("Falling");

            //update the ui and count of lumber in the bank
            lumberCount.UpdateLumberCount(lumberYielded);

            GameManager.instance.numTreesInPlay--;

            forestHealth.UpdateForestHealth();

            InitializeStump();

            //stops the lumberjack chop animation
            lumberjack.ClearLumberjackAnimations();
        }
    }

    public void DigOutStump()
    {
        stumpHealth -= 1;

        if(stumpHealth <= 0)
        {
            //need to find lumberjack in case when the tree is spawned a lumberjack isn't present
            if (lumberjack == null)
            {
                lumberjack = GameObject.FindGameObjectWithTag("Lumberjack").GetComponent<Player>();
            }

            //update the ui and count of lumber in the bank
            lumberCount.UpdateLumberCount(lumberYielded);
            
            GameObject grass = Instantiate(grassTile, transform);
            grass.transform.parent = GameObject.FindGameObjectWithTag("Terrain").transform;

            //update the map at the position to now be grass
            GameManager.instance.map[pos.x, pos.y] = (int)MapGenerator.tileType.grass;

            //must set the colliding tile of the player to the grass so that the HandleActionPanel method doesn't think it's still the tree
            lumberjack.SetCollidingTile(grass);

            //stops the lumberjack chop animation
            lumberjack.ClearLumberjackAnimations();

            //destroy tree
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// resets health to zero, generates stump health and new lumber yielded amount, sets the treestate to stump
    /// </summary>
    private void InitializeStump()
    {
        health = 0;
        animator.SetBool("Swaying", false);

        switch (treeState)
        {
            case maturity.tiny:
                stumpHealth = 1;
                lumberYielded = 1;
                break;
            case maturity.small:
                stumpHealth = 2;
                lumberYielded = 3;
                break;
            case maturity.medium:
                stumpHealth = 3;
                lumberYielded = 5;
                break;
            case maturity.large:
                stumpHealth = 5;
                lumberYielded = 10;
                break;
        }

        treeState = maturity.stump;
    }

    /// <summary>
    /// Put the lumberjack sprite above the tree
    /// </summary>
    public void TreeHasFallen()
    {
        if (lumberjack != null)
        {
            lumberjack.ChangeSpriteOrder(1);
        }
    }

    /// <summary>
    /// grow the tree, change the sprite and stats
    /// </summary>
    public void GrowTree()
    {
        treeState += 1;
        UpdateTreeStats();

        //update the forest health UI on grow to Tiny tree
        if (treeState == maturity.tiny)
        {
            forestHealth.UpdateForestHealth();
        }
    }
}
