using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour, IPointerClickHandler {

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
    private bool hasTarget; //used to determine if the lumberjack is walking toward a tree
    private Vector3 targetXY; //the target location of the next tree;
    private Animator animator;
    private SpriteRenderer sprite;
    private GameObject selectionIndicator;
    private GameObject tempFlag;
    private GameObject collidingTile;
    private Animator collidingTileAnimator;
    
    private ChopButton chopButton;
    private DigButton digButton;
    private PlantButton plantButton;
    private WaterButton waterButton;
    private PromoteLumberJackButton promoteLumberJackButton;
    private PromotePlanterButton promotePlanterButton;
    private HirePlayer lumberjackHireButton;
    private HirePlayer planterHireButton;
    private FireButton fireButton;

    private AudioSource audioSource;
    private Slider fatigueSlider;
    private Transform terrain;

    private enum direction { up, down, left, right };
    private direction travelDirection;

    [SerializeField]
    private direction lastDirection;
    private int seedsPlanted;

    [SerializeField]
    private int pointTowardsNextRank;
    private bool canMove = true;
    private bool isFatigued = false;

    [SerializeField]
    private bool nextTileRiver;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        actionPanel = FindObjectOfType<ActionPanel>();
        terrain = GameObject.FindGameObjectWithTag("Terrain").transform;

        //lumberjack buttons
        chopButton = actionPanel.lumberjackButtons[0].GetComponent<ChopButton>();
        digButton = actionPanel.lumberjackButtons[1].GetComponent<DigButton>();
        promoteLumberJackButton = actionPanel.lumberjackButtons[2].GetComponent<PromoteLumberJackButton>();

        //planter buttons
        plantButton = actionPanel.planterButtons[0].GetComponent<PlantButton>();
        waterButton = actionPanel.planterButtons[1].GetComponent<WaterButton>();
        promotePlanterButton = actionPanel.planterButtons[2].GetComponent<PromotePlanterButton>();

        //hire buttons
        lumberjackHireButton = actionPanel.hireButtons[0].GetComponent<HirePlayer>();
        planterHireButton = actionPanel.hireButtons[1].GetComponent<HirePlayer>();
        fireButton = actionPanel.gameObject.transform.GetChild(8).GetComponent<FireButton>();

        selectionIndicator = transform.GetChild(0).gameObject; //Gets the indicator child game object
        fatigueSlider = transform.SearchForChild("FatigueSlider").GetComponent<Slider>();
    }

    void Start () {
        hasTarget = false;
        isSelected = false;
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

        if(tag == "Planter")
        {
            GameManager.instance.planterHired = true;
        }
        else if(tag == "Lumberjack")
        {
            GameManager.instance.lumberjackHired = true;
        }
        SelectPlayer(false);
    }

    private void Update()
    {
        if(fatigueSlider.value == 1)
        {
            isFatigued = true;
            animator.SetBool("Fatigued", true);
        }

        if(isFatigued == true)
        {
            if (tag == "Planter")
            {
                actionPanel.EnableDisableSingleButton(plantButton.gameObject, false);
                actionPanel.EnableDisableSingleButton(waterButton.gameObject, false);
            }
            else if (tag == "Lumberjack")
            {
                actionPanel.EnableDisableSingleButton(chopButton.gameObject, false);
                actionPanel.EnableDisableSingleButton(digButton.gameObject, false);
            }
        }

        if(fatigueSlider.value > 0)
        {
            fatigueSlider.value -= recoverFatigueRate;
            if (fatigueSlider.value == 0)
            {
                isFatigued = false;
                animator.SetBool("Fatigued", false);

                HandleActionPanelButtons();
            }
        }
    }

    public void PlayerRunsFromBear()
    {
        SelectPlayer(false);
        StartCoroutine(ChainJumps());
    }

    private IEnumerator ChainJumps()
    {
        for(int i = 0; i < 10; i++)
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
            
            //yield return new WaitForSeconds(0.5f);
        }
    }

    public void HandleMovePlayer()
    {
        if (isSelected == true && hasTarget == false && canMove == true && isFatigued == false)
        {
            //get the distance between the mouse and the player
            Vector3 distanceToMouse = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hasTarget = true;
            targetXY = Input.mousePosition;

            //check if we haven't clicked the player again
            if (Mathf.Abs(distanceToMouse.x) >= 0.5f || Mathf.Abs(distanceToMouse.y) >= 0.5f)
            {
                //travel on X
                if (Mathf.Abs(distanceToMouse.x) > Mathf.Abs(distanceToMouse.y))
                {
                    if (distanceToMouse.x > 0)
                    {
                        StartCoroutine(TravelToTarget(lastDirection = direction.left));
                    }
                    else
                    {
                        StartCoroutine(TravelToTarget(lastDirection = direction.right));
                    }
                }
                //travel on Y
                else
                {
                    if (distanceToMouse.y > 0)
                    {
                        StartCoroutine(TravelToTarget(lastDirection = direction.down));
                    }
                    else
                    {
                        StartCoroutine(TravelToTarget(lastDirection = direction.up));
                    }
                }
            }
        }
    }

    /// <summary>
    /// raycasts to determine if the next tile is a river space
    /// </summary>
    /// <param name="direction"></param>
    private void IsNextTileRiver(direction direction)
    {
        RaycastHit2D hit;

        switch (direction)
        {
            case direction.down:
                hit = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("River"));
                break;
            case direction.up:
                hit = Physics2D.Raycast(transform.position, Vector2.up, 1, LayerMask.GetMask("River"));
                break;
            case direction.left:
                hit = Physics2D.Raycast(transform.position, Vector2.left, 1, LayerMask.GetMask("River"));
                break;
            case direction.right:
            default:
                hit = Physics2D.Raycast(transform.position, Vector2.right, 1, LayerMask.GetMask("River"));
                break;
        }

        if(hit != false)
        {
            nextTileRiver = hit.collider.gameObject.tag == "River" ? true : false;
        }
        
    }

    /// <summary>
    /// Used to make a player travel in the direction they are jumping
    /// </summary>
    /// <param name="dir">direction the player is moving in</param>
    /// <returns></returns>
    private IEnumerator TravelToTarget(direction dir)
    {
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

        Vector3 changeVector;

        float fatigueVal = (1 - fatigueSlider.value);

        //determine the direction vector
        if (dir == direction.down)
        {
            IsNextTileRiver(direction.down);

            if (nextTileRiver)
            {
                SpawnFlag(transform.position.x, transform.position.y - 2);
                animator.runtimeAnimatorController = riverDown;
            }
            else
            {
                SpawnFlag(transform.position.x, transform.position.y - 1);
                animator.runtimeAnimatorController = down;
            }

            
            changeVector = new Vector3(0, -jumpSpeed, 0);
        }
        else if(dir == direction.up)
        {
            IsNextTileRiver(direction.up);

            if (nextTileRiver)
            {
                SpawnFlag(transform.position.x, transform.position.y + 2);
                animator.runtimeAnimatorController = riverUp;
            }
            else
            {
                SpawnFlag(transform.position.x, transform.position.y + 1);
                animator.runtimeAnimatorController = up;
            }
            
            changeVector = new Vector3(0, jumpSpeed, 0);
        }
        else if (dir == direction.left)
        {
            IsNextTileRiver(direction.left);

            if (nextTileRiver)
            {
                SpawnFlag(transform.position.x - 2, transform.position.y);
                animator.runtimeAnimatorController = riverSide;
            }
            else
            {
                SpawnFlag(transform.position.x - 1, transform.position.y);
                animator.runtimeAnimatorController = side;
            }

            sprite.flipX = true;
            changeVector = new Vector3(-jumpSpeed, 0, 0);
        }
        else
        {
            IsNextTileRiver(direction.right);

            if (nextTileRiver)
            {
                SpawnFlag(transform.position.x + 2, transform.position.y);
                animator.runtimeAnimatorController = riverSide;
            }
            else
            {
                SpawnFlag(transform.position.x + 1, transform.position.y);
                animator.runtimeAnimatorController = side;
            }

            sprite.flipX = false;
            changeVector = new Vector3(jumpSpeed, 0, 0);
        }


        if(nextTileRiver == true)
        {

            float numLoops = (1 / (jumpSpeed / 2));

            animator.speed = 0.35f;

            for (int i = 0; i < numLoops; i++)
            {
                transform.Translate(changeVector);
                yield return new WaitForSeconds(0.025f);
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
        

        hasTarget = false;

        //destroy the marker flag that shows they tile they are landing on
        Destroy(tempFlag);

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
        //if there is no currently selected player
        if(GameManager.instance.playerSelected == false)
        {
            GameManager.instance.playerSelected = true;
            SelectPlayer(true);

            //toggle which buttons are active on the actions panel
            if (tag == "Lumberjack")
            {
                actionPanel.ActivateButtons(ActionPanel.SelectedPlayer.lumberjack);
                if(GameManager.instance.planterHired == false)
                {
                    actionPanel.EnableDisableSingleButton(planterHireButton.gameObject, true);
                }
            }
            else if (tag == "Planter")
            {
                actionPanel.ActivateButtons(ActionPanel.SelectedPlayer.planter);
                if (GameManager.instance.lumberjackHired == false)
                {
                    actionPanel.EnableDisableSingleButton(lumberjackHireButton.gameObject, true);
                }
            }
            HandleActionPanelButtons();
        }
        //the player is selected when clicked
        else
        {
            //if this player is selected already, deactivate
            if (isSelected == true)
            {

               actionPanel.ActivateButtons(ActionPanel.SelectedPlayer.none);

                if (tag == "Lumberjack" && chopButton.chopping == true)
                {
                    animator.SetBool("Chop", false);
                    animator.runtimeAnimatorController = down;
                    chopButton.chopping = false;
                }
                else if (tag == "Planter" && plantButton.planting == true)
                {
                    animator.SetBool("Plant", false);
                    plantButton.planting = false;
                    animator.runtimeAnimatorController = down;
                }
                else if (tag == "Planter" && waterButton.watering == true)
                {
                    animator.SetBool("Water", false);
                    waterButton.watering = false;
                    animator.runtimeAnimatorController = down;
                }

                GameManager.instance.playerSelected = false;
                hasTarget = false;
                SelectPlayer(false);
            }
            //otherwise we are selecting the other player
            else
            {
                //find the other player
                foreach(Player p in FindObjectsOfType<Player>())
                {
                    if(p.gameObject != this)
                    {
                        p.SelectPlayer(false);
                        Debug.Log("Deselecting other player");
                    }
                }

                if (tag == "Lumberjack")
                {
                    actionPanel.ActivateButtons(ActionPanel.SelectedPlayer.lumberjack);
                }
                else if (tag == "Planter")
                {
                    actionPanel.ActivateButtons(ActionPanel.SelectedPlayer.planter);
                }

                

                GameManager.instance.playerSelected = true;
                SelectPlayer(true);
            }

            HandleActionPanelButtons();
        }
    }

    public void SelectPlayer(bool isActive)
    {
        isSelected = isActive;
        selectionIndicator.SetActive(isActive);
        fatigueSlider.gameObject.SetActive(isActive);
        if(isActive == true)
        {
            GameManager.instance.selectedPlayer = this;
        }
        else
        {
            GameManager.instance.selectedPlayer = null;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidingTile = collision.gameObject;
        collidingTileAnimator = collidingTile.GetComponent<Animator>();
        //HandleActionPanelButtons();

        //if we jump out of the play space, destroy the player and reset values
        if(collision.tag == "Boundary")
        {
            //Trigger Fire/Quit Player notification
            SelectPlayer(false);

            if (tag == "Lumberjack")
            {
                actionPanel.EnableDisableSingleButton(lumberjackHireButton.gameObject, true);
                GameManager.instance.lumberjackHired = false;
            }
            else if(tag == "Planter")
            {
                actionPanel.EnableDisableSingleButton(planterHireButton.gameObject, true);
                GameManager.instance.planterHired = false;
            }

            Destroy(tempFlag);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// based off of tile type, enable/disable buttons
    /// </summary>
    private void HandleActionPanelButtons()
    {
        if(GameManager.instance.selectedPlayer == this)
        {
            if (tag == "Lumberjack")
            {
                Debug.Log("current rank " + currentRank);
                Debug.Log("point towards next rank " + pointTowardsNextRank);

                //check rank to enable promotion button
                if (pointTowardsNextRank < GameManager.instance.rank[currentRank])
                {
                    Debug.Log("Enabled promotion button from Handle Action Panel Buttons");
                    actionPanel.EnableDisableSingleButton(promoteLumberJackButton.gameObject, false);
                }
                else
                {
                    Debug.Log("Disabled promotion button from Handle Action Panel Buttons");
                    actionPanel.EnableDisableSingleButton(promoteLumberJackButton.gameObject, true);
                }

                //check colliding tile is a tree and enable chop button/dig button
                if (collidingTile.tag == "Tree")
                {
                    Tree t = collidingTile.GetComponent<Tree>();
                    if (t.treeState != Tree.maturity.stump)
                    {
                        actionPanel.EnableDisableSingleButton(chopButton.gameObject, true);
                        actionPanel.EnableDisableSingleButton(digButton.gameObject, false);
                    }
                    else
                    {
                        actionPanel.EnableDisableSingleButton(chopButton.gameObject, false);
                        actionPanel.EnableDisableSingleButton(digButton.gameObject, true);
                    }
                    
                }
                //grass, disable chop and dig buttons
                else
                {
                    actionPanel.EnableDisableSingleButton(chopButton.gameObject, false);
                    actionPanel.EnableDisableSingleButton(digButton.gameObject, false);
                }
            }
            else if (tag == "Planter")
            {
                //check rank to enable promotion button
                if (pointTowardsNextRank < GameManager.instance.rank[currentRank])
                {
                    actionPanel.EnableDisableSingleButton(promotePlanterButton.gameObject, false);
                }
                else
                {
                    actionPanel.EnableDisableSingleButton(promotePlanterButton.gameObject, true);
                }

                //check tiles we collide with
                if (collidingTile.tag == "Tree" || collidingTile.tag == "Obstacle" || collidingTile.tag == "River")
                {

                    //check if tree is a seed to activate water button
                    Tree tree = collidingTile.GetComponent<Tree>();
                    if (tree != null && tree.treeState == Tree.maturity.seed)
                    {
                        Debug.Log("Can watter");
                        actionPanel.EnableDisableSingleButton(waterButton.gameObject, true);
                    }
                    else
                    {
                        actionPanel.EnableDisableSingleButton(waterButton.gameObject, false);
                    }

                    //disable plant button
                    actionPanel.EnableDisableSingleButton(plantButton.gameObject, false);
                }
                //if not a tree, or obstacle, or river
                else
                {
                    //enable plant button, disable water button
                    actionPanel.EnableDisableSingleButton(plantButton.gameObject, true);
                    actionPanel.EnableDisableSingleButton(waterButton.gameObject, false);
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

        if (tag == "Lumberjack")
        {
            if(playAnimation == true)
            {
                ChangeSpriteOrder(-1000);
            }
            else
            {
                ChangeSpriteOrder(1);
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
        if(tag == "Planter")
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
        if (collidingTileAnimator != null)
        {
            collidingTileAnimator.SetTrigger("Chop");
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
        if (collidingTileAnimator != null)
        {
            collidingTileAnimator.GetComponent<Tree>().DigOutStump();
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
        if(tag == "Planter" && seedsPlanted++ >= 2)
        {
            canMove = true;

            pointTowardsNextRank++;
            HandleActionPanelButtons();

            ////destroy the grass tile
            Destroy(collidingTile);

            //instantiate the tree and start as a seed
            collidingTile = Instantiate(tree, transform.position, Quaternion.identity);
            collidingTile.transform.parent = terrain;
            collidingTile.GetComponent<Tree>().StartAsSeed();

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
        if(collidingTree.waterCount++ >= 5)
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
    }
}