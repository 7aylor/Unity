using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour, IPointerClickHandler
{
    public AnimatorOverrideController up;
    public AnimatorOverrideController down;
    public AnimatorOverrideController side;

    public AnimatorOverrideController riverUp;
    public AnimatorOverrideController riverDown;
    public AnimatorOverrideController riverSide;

    public GameObject flag;
    public GameObject tree;
    public AudioClip chopSfx;
    public AudioClip waterSfx;
    public float jumpSpeed;
    public bool doneJumping;

    public float animatorLumberjackJumpSpeed;
    public float animatorPlanterJumpSpeed;
    public float animatorPlantSpeed;
    public float animatorWaterSpeed;
    public float animatorChopSpeed;
    public float animatorDigSpeed;
    public int currentRank;

    public float fatigueIncrement;
    public float recoverFatigueRate;

    private ActionPanel actionPanel;

    private bool isSelected;
    private bool canSelect;
    private bool rejuvinating;
    private bool hasTarget; //used to determine if the lumberjack is walking toward a tree
    private Vector3 targetXY; //the target location of the next tree;
    private Animator animator;
    private SpriteRenderer sprite;
    private GameObject selectionIndicator;
    private GameObject tempFlag;

    [SerializeField]
    private GameObject collidingTile;
    private Animator collidingTreeAnimator;

    private ChopButton chopButton;
    private DigButton digButton;
    private PlantButton plantButton;
    private WaterButton waterButton;
    private FireButton fireButton;
    private UIActions uIActions;

    private AudioSource audioSource;
    private Slider fatigueSlider;
    private Transform terrain;

    private enum direction { up, down, left, right, none };
    private direction travelDirection;

    [SerializeField]
    private direction lastDirection;
    private int seedsPlanted;

    [SerializeField]
    private int pointTowardsNextRank;
    [SerializeField]
    private bool canMove = true;
    private bool isFatigued = false;

    [SerializeField]
    private bool nextTileRiver;

    private List<direction> moves;

    [SerializeField]
    private Vector2Int mapPos;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        actionPanel = FindObjectOfType<ActionPanel>();
        terrain = GameObject.FindGameObjectWithTag("Terrain").transform;

        uIActions = FindObjectOfType<UIActions>();

        chopButton = actionPanel.lumberjack_chop.GetComponent<ChopButton>();
        digButton = actionPanel.lumberjack_dig.GetComponent<DigButton>();
        plantButton = actionPanel.planter_plant.GetComponent<PlantButton>();
        waterButton = actionPanel.planter_water.GetComponent<WaterButton>();
        fireButton = actionPanel.gameObject.transform.GetChild(8).GetComponent<FireButton>();

        selectionIndicator = transform.GetChild(0).gameObject; //Gets the indicator child game object
        fatigueSlider = transform.SearchForChild("FatigueSlider").GetComponent<Slider>();

        moves = new List<direction>();
    }

    void Start()
    {
        hasTarget = false;
        isSelected = false;
        canSelect = true;
        rejuvinating = false;
        seedsPlanted = 0;
        currentRank = 0;
        pointTowardsNextRank = 0;
        animatorLumberjackJumpSpeed = 1;
        animatorPlanterJumpSpeed = 1;
        animatorPlantSpeed = 1;
        animatorWaterSpeed = 1;
        animatorChopSpeed = 1;
        animatorDigSpeed = 1;
        jumpSpeed = 0.05f;
        doneJumping = true;
        nextTileRiver = false;
        selectionIndicator.SetActive(false);
        fatigueSlider.gameObject.SetActive(false);
        lastDirection = direction.none;

        if (tag == "Planter")
        {
            GameManager.instance.planterHired = true;
        }
        else if (tag == "Lumberjack")
        {
            GameManager.instance.lumberjackHired = true;
        }

        GetMapPos();
    }

    /// <summary>
    /// converts player's worldspace position to map, and sets the player's map position
    /// </summary>
    private void GetMapPos()
    {
        mapPos.x = GameManager.instance.WorldCoordToArrayCoordX(transform.position.x);
        mapPos.y = GameManager.instance.WorldCoordToArrayCoordY(transform.position.y);
    }

    public void PlayerRunsFromBear()
    {
        //if the player is fatigued, give them a rush of energy to escape
        if (isFatigued == true || fatigueSlider.value > 0.75)
        {
            fatigueSlider.value = 0.5f;
            isFatigued = false;
            animator.SetBool("Fatigued", false);
        }
        SelectPlayer(false);
        StartCoroutine(ChainJumps());
    }

    private void OnBecameInvisible()
    {
        DestroyPlayer();
    }

    private void DestroyPlayer()
    {
        //Trigger Fire/Quit Player notification
        if (GameManager.instance.selectedPlayer == this)
        {
            SelectPlayer(false);
        }

        fireButton.FireSelectedPlayer(tag);

        if (tempFlag != null)
        {
            Destroy(tempFlag);
        }
    }

    private IEnumerator ChainJumps()
    {
        for (int i = 0; i < 10; i++)
        {
            if (isFatigued)
            {
                break;
            }

            switch (lastDirection)
            {
                case direction.down:
                    yield return StartCoroutine(TravelToTarget(direction.up));
                    break;
                case direction.up:
                    yield return StartCoroutine(TravelToTarget(direction.down));
                    break;
                case direction.left:
                    yield return StartCoroutine(TravelToTarget(direction.right));
                    break;
                case direction.right:
                    yield return StartCoroutine(TravelToTarget(direction.left));
                    break;
            }
        }
    }

    /// <summary>
    /// Called from EnvironmentClick.cs, Builds the move stack and starts the player movement
    /// </summary>
    /// <param name="tileX">World Position X of the tile that is clicked</param>
    /// <param name="tileY">World Position Y of the tile that is clicked</param>
    public void HandleMovePlayer(float tileX, float tileY)
    {
        Debug.Log("HandleMovePlayer called");
        if (isSelected == true && hasTarget == false && canMove == true && isFatigued == false)
        {
            if (GameManager.instance.IsRiverTile(tileX, tileY) == false)
            {
                SetMoves(tileX, tileY);
                if (moves.Count > 0)
                {
                    SpawnFlag(tileX, tileY);
                    OnPointerClick(null);
                    canSelect = false;
                    canMove = false;
                    StartCoroutine(TravelToTarget(moves[0]));
                }
            }
        }
    }

    private void SetMoves(float targetX, float targetY)
    {
        int newTargetX = GameManager.instance.WorldCoordToArrayCoordX(targetX);
        int newTargetY = GameManager.instance.WorldCoordToArrayCoordY(targetY);
        int playerX = GameManager.instance.WorldCoordToArrayCoordX(transform.position.x);
        int playerY = GameManager.instance.WorldCoordToArrayCoordY(transform.position.y);

        int maxMoves = (Mathf.Abs(playerX - newTargetX)) + (Mathf.Abs(playerY - newTargetY));

        Debug.Log("Before While in SetMoves");
        Debug.Log(newTargetX);
        Debug.Log(newTargetY);
        Debug.Log(playerX);
        Debug.Log(playerY);

        int count = 0;

        while ((playerX != newTargetX || playerY != newTargetY) && count < 1000)
        {
            Debug.Log("While in SetMoves Executed");
            count++;
            //right
            if (playerX < newTargetX && lastDirection != direction.left)
            {
                //if right tile is not a river, add move right
                if (GameManager.instance.IsRiverTile(playerX + 1, playerY) == false)
                {
                    UpdateMovesAndPlayerPos(direction.right, ref lastDirection, ref playerX, 1);
                    continue;
                }
                else
                {
                    //if next tile is river, and next next tile is not, move right, add 2
                    if (GameManager.instance.IsRiverTile(playerX + 2, playerY) == false)
                    {
                        UpdateMovesAndPlayerPos(direction.right, ref lastDirection, ref playerX, 2);
                        continue;
                    }
                }
            }
            //left
            if (playerX > newTargetX && lastDirection != direction.right)
            {
                if (GameManager.instance.IsRiverTile(playerX - 1, playerY) == false)
                {
                    UpdateMovesAndPlayerPos(direction.left, ref lastDirection, ref playerX, -1);
                    continue;
                }
                else
                {
                    if (GameManager.instance.IsRiverTile(playerX - 2, playerY) == false)
                    {
                        UpdateMovesAndPlayerPos(direction.left, ref lastDirection, ref playerX, -2);
                        continue;
                    }
                }
            }
            //up
            if (playerY < newTargetY && lastDirection != direction.down)
            {
                //if next tile up is not a river, add move up
                if (GameManager.instance.IsRiverTile(playerX, playerY + 1) == false)
                {
                    UpdateMovesAndPlayerPos(direction.up, ref lastDirection, ref playerY, 1);
                    continue;
                }
                else
                {
                    //if next tile is river but two tiles up is not a river, add move up
                    if (GameManager.instance.IsRiverTile(playerX, playerY + 2) == false)
                    {
                        UpdateMovesAndPlayerPos(direction.up, ref lastDirection, ref playerY, 2);
                        continue;
                    }
                }
            }
            //down
            if (playerY > newTargetY && lastDirection != direction.up)
                if (GameManager.instance.IsRiverTile(playerX, playerY - 1) == false)
                {
                    UpdateMovesAndPlayerPos(direction.down, ref lastDirection, ref playerY, -1);
                    continue;
                }
                else
                {
                    if (GameManager.instance.IsRiverTile(playerX, playerY - 2) == false)
                    {
                        UpdateMovesAndPlayerPos(direction.down, ref lastDirection, ref playerY, -2);
                        continue;
                    }
                }
        }
    }

    /// <summary>
    /// Adds a direction to the moves List, adjusts the players x or y position in SetMoves, and sets the last direction
    /// </summary>
    /// <param name="newDir"></param>
    /// <param name="lastDir"></param>
    /// <param name="playerXY"></param>
    /// <param name="changeAmount"></param>
    private void UpdateMovesAndPlayerPos(direction newDir, ref direction lastDir, ref int playerXY, int changeAmount)
    {
        moves.Add(newDir);
        playerXY += changeAmount;
        lastDir = newDir;
    }

    /// <summary>
    /// raycasts to determine if the next tile is a river space
    /// </summary>
    /// <param name="direction"></param>
    private void IsNextTileRiver(direction direction, int offset = 0)
    {
        switch (direction)
        {
            case direction.down:
                nextTileRiver = GameManager.instance.IsRiverTile(transform.position.x, transform.position.y - 1);
                break;
            case direction.up:
                nextTileRiver = GameManager.instance.IsRiverTile(transform.position.x, transform.position.y + 1);
                break;
            case direction.left:
                nextTileRiver = GameManager.instance.IsRiverTile(transform.position.x - 1, transform.position.y);
                break;
            case direction.right:
            default:
                nextTileRiver = GameManager.instance.IsRiverTile(transform.position.x + 1, transform.position.y);
                break;
        }
    }

    /// <summary>
    /// Used to make a player travel in the direction they are jumping
    /// </summary>
    /// <param name="dir">direction the player is moving in</param>
    /// <returns></returns>
    private IEnumerator TravelToTarget(direction dir)
    {
        if (fatigueSlider.value < 1 && !isFatigued)
        {
            Vector3 changeVector;

            //determine the direction vector
            if (dir == direction.down)
            {
                HandleRiverJumpAnimations(dir);
                changeVector = new Vector3(0, -jumpSpeed, 0);
            }
            else if (dir == direction.up)
            {
                HandleRiverJumpAnimations(direction.up);
                changeVector = new Vector3(0, jumpSpeed, 0);
            }
            else if (dir == direction.left)
            {
                HandleRiverJumpAnimations(direction.left);
                sprite.flipX = true;
                changeVector = new Vector3(-jumpSpeed, 0, 0);
            }
            else
            {
                HandleRiverJumpAnimations(direction.right);
                sprite.flipX = false;
                changeVector = new Vector3(jumpSpeed, 0, 0);
            }

            doneJumping = false;
            if (tag == "Planter")
            {
                animator.SetFloat("JumpSpeed", animatorPlanterJumpSpeed);
            }
            else if (tag == "Lumberjack")
            {
                animator.SetFloat("JumpSpeed", animatorLumberjackJumpSpeed);
            }
            animator.SetTrigger("Jump");

            float fatigueVal = (1 - fatigueSlider.value);

            if (nextTileRiver == true)
            {

                float numLoops = (1 / (jumpSpeed / 2)); //divide by two because you jump over a river tile

                //numLoops = 

                //TODO: make these frame independent

                animator.speed = 0.35f;

                for (int i = 0; i < numLoops; i++)
                {
                    transform.Translate(changeVector);
                    yield return new WaitForSeconds(0.025f);
                }

                if (transform.position.x >= 9 || transform.position.x <= -9 ||
                   transform.position.y >= 5.6 || transform.position.y <= -4)
                {
                    fireButton.FireSelectedPlayer(tag);
                }

                animator.speed = 1;
                nextTileRiver = false;
            }
            else
            {
                float numLoops = (1 / jumpSpeed);

                for (int i = 0; i < numLoops; i++)
                {
                    transform.Translate(changeVector);
                    yield return new WaitForSeconds(0.025f);
                }
            }

            if (moves != null && moves.Count > 0)
            {
                moves.RemoveAt(0);
            }
            EndTravelToTarget();
        }
        else
        {
            moves.Clear();
        }

        if (moves.Count > 0)
        {
            RoundPlayerPosition();
            StartCoroutine(TravelToTarget(moves[0]));
        }
        else
        {
            //destroy the marker flag that shows the tile they are landing on
            moves.Clear();
            Destroy(tempFlag);
            GetMapPos();
            RoundPlayerPosition();
            canMove = true;
            canSelect = true;
            if (GameManager.instance.selectedPlayer == null)
            {
                OnPointerClick(null);
            }
        }
    }

    /// <summary>
    /// Ensure that the player ends exactly on a tile instead of being off by a tiny fraction
    /// </summary>
    private void RoundPlayerPosition()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x * 10) / 10,
                                         Mathf.Round(transform.position.y * 10) / 10, transform.position.z);
    }

    /// <summary>
    /// Called from TravelToTarget and handles which animators to use for each jump type
    /// </summary>
    /// <param name="d">direction</param>
    private void HandleRiverJumpAnimations(direction d)
    {
        IsNextTileRiver(d);

        if (nextTileRiver)
        {
            switch (d)
            {
                case direction.left:
                case direction.right:
                    animator.runtimeAnimatorController = riverSide;
                    break;
                case direction.down:
                    animator.runtimeAnimatorController = riverDown;
                    break;
                case direction.up:
                    animator.runtimeAnimatorController = riverUp;
                    break;
            }
        }
        else
        {
            switch (d)
            {
                case direction.left:
                case direction.right:
                    animator.runtimeAnimatorController = side;
                    break;
                case direction.down:
                    animator.runtimeAnimatorController = down;
                    break;
                case direction.up:
                    animator.runtimeAnimatorController = up;
                    break;
            }
        }
    }

    private void EndTravelToTarget()
    {
        hasTarget = false;

        //update the buttons based on the new tile the player is on
        HandleActionPanelButtons();

        //indicate we are done jumping
        doneJumping = true;
    }

    private void SpawnFlag(float x, float y)
    {
        if (isSelected)
        {
            tempFlag = Instantiate(flag, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canSelect == true)
        {
            //if there is no currently selected player
            if (GameManager.instance.playerSelected == false)
            {
                GameManager.instance.playerSelected = true;
                SelectPlayer(true);
                HandleActionPanelButtons();
            }
            //the player is selected when clicked
            else
            {
                //if this player is selected already, deactivate
                if (isSelected == true)
                {
                    uIActions.PlanterState = GameManager.planter_UI_State.Other;
                    uIActions.LumberjackState = GameManager.lumberjack_UI_State.Other;
                    uIActions.PromoteState = GameManager.promote_UI_State.None;
                    GameManager.instance.playerSelected = false;
                    hasTarget = false;
                    SelectPlayer(false);
                }
                //otherwise we are selecting the other player
                else
                {
                    //find the other player
                    foreach (Player p in FindObjectsOfType<Player>())
                    {
                        if (p.gameObject != this)
                        {
                            p.SelectPlayer(false);
                        }
                    }

                    if (tag == "Lumberjack")
                    {
                        uIActions.LumberjackState = GameManager.lumberjack_UI_State.Other;
                    }
                    else if (tag == "Planter")
                    {
                        uIActions.PlanterState = GameManager.planter_UI_State.Other;
                    }

                    GameManager.instance.playerSelected = true;
                    SelectPlayer(true);
                }

                HandleActionPanelButtons();
            }
        }
    }

    public void SelectPlayer(bool isActive)
    {
        isSelected = isActive;
        selectionIndicator.SetActive(isActive);
        fatigueSlider.gameObject.SetActive(isActive);
        if (isActive == true)
        {
            //a player is selected, so activate the fire button
            fireButton.gameObject.SetActive(true);
            GameManager.instance.selectedPlayer = this;
        }
        else
        {
            //a player is selected, so activate the fire button
            fireButton.gameObject.SetActive(false);
            GameManager.instance.selectedPlayer = null;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetCollidingTile(collision.gameObject);
        HandleActionPanelButtons();

        if (collision.tag == "Boundary")
        {
            DestroyPlayer();
        }
    }

    /// <summary>
    /// based off of tile type, enable/disable buttons
    /// </summary>
    public void HandleActionPanelButtons()
    {
        if (GameManager.instance.selectedPlayer == this)
        {
            if (tag == "Lumberjack")
            {
                //if the planter is hired, turn off it's buttons by changing its UI state
                if (uIActions.PlanterState != GameManager.planter_UI_State.None)
                {
                    uIActions.PlanterState = GameManager.planter_UI_State.Other;
                }

                //check rank to enable promotion button
                if (pointTowardsNextRank < GameManager.instance.rank[currentRank])
                {
                    uIActions.PromoteState = GameManager.promote_UI_State.None;
                }
                else
                {
                    uIActions.PromoteState = GameManager.promote_UI_State.Promote_lumberjack;
                }

                //make sure player is not fatigued before setting interactable buttons
                if (isFatigued == false)
                {
                    //check colliding tile is a tree and enable chop button/dig button
                    if (collidingTile.tag == "Tree")
                    {
                        Tree t = collidingTile.GetComponent<Tree>();
                        if (t.treeState != Tree.maturity.stump)
                        {
                            uIActions.LumberjackState = GameManager.lumberjack_UI_State.Tree;
                        }
                        else
                        {
                            uIActions.LumberjackState = GameManager.lumberjack_UI_State.Stump;
                        }
                    }
                    //grass, disable chop and dig buttons
                    else
                    {
                        uIActions.LumberjackState = GameManager.lumberjack_UI_State.Other;
                    }
                }
            }
            else if (tag == "Planter")
            {
                //if the lumberjack is hired, turn off it's buttons by changing its UI state
                if (uIActions.LumberjackState != GameManager.lumberjack_UI_State.None)
                {
                    uIActions.LumberjackState = GameManager.lumberjack_UI_State.Other;
                }

                //check rank to enable promotion button
                if (pointTowardsNextRank < GameManager.instance.rank[currentRank])
                {
                    uIActions.PromoteState = GameManager.promote_UI_State.None;
                }
                else
                {
                    uIActions.PromoteState = GameManager.promote_UI_State.Promote_Planter;
                }

                //make sure player is not fatigued before setting interactable buttons
                if (isFatigued == false)
                {
                    //check tiles we collide with
                    if (collidingTile.tag == "Tree" || collidingTile.tag == "Obstacle" || collidingTile.tag == "River")
                    {
                        //check if tree is a seed to activate water button
                        Tree tree = collidingTile.GetComponent<Tree>();
                        if (tree != null && tree.treeState == Tree.maturity.seed)
                        {
                            uIActions.PlanterState = GameManager.planter_UI_State.Seed;
                        }
                        else
                        {
                            uIActions.PlanterState = GameManager.planter_UI_State.Other;
                        }
                    }
                    //if not a tree, or obstacle, or river
                    else
                    {
                        uIActions.PlanterState = GameManager.planter_UI_State.Grass;
                    }
                }
            }
        }
    }

    /// <summary>
    /// called from chop button
    /// </summary>
    /// <param name="playAnimation"></param>
    public void PlayLumberjackAnimation(bool playAnimation, string animation)
    {
        canMove = !playAnimation;
        Tree tree = collidingTile.GetComponent<Tree>();

        if (tag == "Lumberjack")
        {
            if (playAnimation == true)
            {
                ChangeSpriteOrder(-10);

                //allows the player to be clicked on when behind the tree
                if (tree != null)
                {
                    tree.EnableCollider(false);
                }
            }
            else
            {
                ChangeSpriteOrder(1);
                if (tree != null)
                {
                    tree.EnableCollider(true);
                }
            }
            animator.SetBool(animation, playAnimation);
            animator.runtimeAnimatorController = down;
        }
    }

    /// <summary>
    /// change the sprite sorting order of the player
    /// </summary>
    /// <param name="newOrder"></param>
    public void ChangeSpriteOrder(int newOrder)
    {
        sprite.sortingOrder = newOrder;
    }

    /// <summary>
    /// called from Plant Button
    /// </summary>
    /// <param name="playAnimation"></param>
    public void PlayPlantAnimation(bool playAnimation)
    {
        canMove = false;
        if (tag == "Planter")
        {
            animator.SetBool("Plant", playAnimation);
            animator.runtimeAnimatorController = down;
        }
    }

    /// <summary>
    /// called from Water button
    /// </summary>
    /// <param name="playAnimation"></param>
    public void PlayWaterAnimation(bool playAnimation)
    {
        canMove = false;
        if (tag == "Planter")
        {
            animator.SetBool("Water", playAnimation);
            animator.runtimeAnimatorController = down;
        }
    }

    /// <summary>
    /// called from the animator
    /// </summary>
    public void LumberjackChopAnimation()
    {
        if (collidingTreeAnimator != null)
        {
            collidingTreeAnimator.SetTrigger("Chop");
            if (sprite.flipX == true)
            {
                collidingTile.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                collidingTile.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    /// <summary>
    /// called from the animator
    /// </summary>
    public void LumberjackDigAnimation()
    {
        if (collidingTile.tag == "Tree")
        {
            collidingTreeAnimator.GetComponent<Tree>().DigOutStump();
        }
    }

    /// <summary>
    /// called on tree script when tree falls
    /// </summary>
    public void ClearLumberjackAnimations()
    {
        PlayLumberjackAnimation(false, "Chop");
        chopButton.chopping = false;
        PlayLumberjackAnimation(false, "Dig");
        digButton.digging = false;
        pointTowardsNextRank++;
        HandleActionPanelButtons();
    }

    /// <summary>
    /// called from animator at the end of the planting animation
    /// </summary>
    public void SowSeeds()
    {
        if (tag == "Planter" && seedsPlanted++ >= 2)
        {
            canMove = true;

            pointTowardsNextRank++;

            ////destroy the grass tile
            Destroy(collidingTile);

            //instantiate the tree and start as a seed
            collidingTile = Instantiate(tree, transform.position, Quaternion.identity);
            collidingTile.transform.parent = terrain;
            collidingTile.GetComponent<Tree>().StartAsSeed();

            //Update buttons in action panel
            HandleActionPanelButtons();

            //resets the seeds planted count
            seedsPlanted = 0;

            //sets the seed icon active and 
            transform.GetChild(1).gameObject.SetActive(true);
            animator.SetBool("Plant", false);
            plantButton.planting = false;
        }
    }

    /// <summary>
    /// called at the end of the watering animation
    /// </summary>
    public void HandleWatering()
    {
        //get the colliding tree
        Tree collidingTree = collidingTile.GetComponent<Tree>();

        //check if the tree has been watered five times, then adjust stats, handle animations, and update the buttons
        if (collidingTree.waterCount++ >= 5)
        {
            canMove = true;
            pointTowardsNextRank++;
            collidingTree.GrowTree();
            HandleActionPanelButtons();
            animator.SetBool("Water", false);
            waterButton.watering = false;
        }
    }

    #region play sound effects
    public void PlayChopSound()
    {
        audioSource.clip = chopSfx;
        audioSource.Play();
    }

    public void PlayWaterSound()
    {
        audioSource.clip = waterSfx;
        audioSource.Play();
    }
    #endregion

    public void ResetPoints()
    {
        pointTowardsNextRank = 0;
        HandleActionPanelButtons();
    }

    /// <summary>
    /// called at the ends of each action animation
    /// </summary>
    public void AddFatigue()
    {
        fatigueSlider.value += fatigueIncrement;
        if (rejuvinating == false)
        {
            StartCoroutine(Rejuvinate());
        }

        if (fatigueSlider.value == 1)
        {
            isFatigued = true;

            //set UI to reflect being fully fatigued
            if (tag == "Planter")
            {
                uIActions.PlanterState = GameManager.planter_UI_State.Other;
            }
            else if (tag == "Lumberjack")
            {
                uIActions.LumberjackState = GameManager.lumberjack_UI_State.Other;
            }

            StartCoroutine(HandleFatigued());
        }
    }

    /// <summary>
    /// if player is at all fatigued, rejuvinate until full
    /// </summary>
    /// <returns></returns>
    private IEnumerator Rejuvinate()
    {
        rejuvinating = true;

        Debug.Log("Before While in Rejuvinate");
        while (fatigueSlider.value > 0)
        {
            fatigueSlider.value -= recoverFatigueRate;
            yield return new WaitForEndOfFrame();
        }

        rejuvinating = false;
    }

    /// <summary>
    /// if player has become fully fatigued, stop player from moving and wait until fully recovered
    /// </summary>
    /// <returns></returns>
    private IEnumerator HandleFatigued()
    {
        animator.SetBool("Fatigued", true);

        Debug.Log("Before While in HandleFatigued");
        while (fatigueSlider.value > 0)
        {
            fatigueSlider.value -= recoverFatigueRate;
            yield return new WaitForEndOfFrame();

        }

        isFatigued = false;
        animator.SetBool("Fatigued", false);
        HandleActionPanelButtons();
    }

    /// <summary>
    /// Used to set the tile and the animation that the Player is colliding with
    /// </summary>
    /// <param name="obj"></param>
    public void SetCollidingTile(GameObject obj)
    {
        collidingTile = obj;
        if (collidingTile.tag == "Tree")
        {
            collidingTreeAnimator = collidingTile.GetComponent<Animator>();
        }
    }
}