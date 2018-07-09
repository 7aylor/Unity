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
    [SerializeField]
    private bool canMove = true;
    private bool isFatigued = false;

    [SerializeField]
    private bool nextTileRiver;

    private Stack<direction> moves;

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

        moves = new Stack<direction>();
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
        selectionIndicator.SetActive(false);
        fatigueSlider.gameObject.SetActive(false);

        if (tag == "Planter")
        {
            GameManager.instance.planterHired = true;
        }
        else if(tag == "Lumberjack")
        {
            GameManager.instance.lumberjackHired = true;
        }
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
            canMove = false;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePos.x = Mathf.Round(mousePos.x);
            mousePos.y = Mathf.Round(mousePos.y);

            if(GameManager.instance.IsRiverTile(mousePos.x, mousePos.y) == false)
            {
                SetMoves(mousePos.x, mousePos.y);
                SpawnFlag(mousePos.x, mousePos.y);
                StartCoroutine(TravelToTarget(moves.Pop()));
            }

            ////get the distance between the mouse and the player
            //Vector3 distanceToMouse = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //hasTarget = true;
            //targetXY = Input.mousePosition;

            ////check if we haven't clicked the player again
            //if (Mathf.Abs(distanceToMouse.x) >= 0.5f || Mathf.Abs(distanceToMouse.y) >= 0.5f)
            //{
            //    //travel on X
            //    if (Mathf.Abs(distanceToMouse.x) > Mathf.Abs(distanceToMouse.y))
            //    {
            //        if (distanceToMouse.x > 0)
            //        {
            //            StartCoroutine(TravelToTarget(lastDirection = direction.left));
            //        }
            //        else
            //        {
            //            StartCoroutine(TravelToTarget(lastDirection = direction.right));
            //        }
            //    }
            //    //travel on Y
            //    else
            //    {
            //        if (distanceToMouse.y > 0)
            //        {
            //            StartCoroutine(TravelToTarget(lastDirection = direction.down));
            //        }
            //        else
            //        {
            //            StartCoroutine(TravelToTarget(lastDirection = direction.up));
            //        }
            //    }
            //}
        }
    }

    private void SetMoves(float targetX, float targetY)
    {
        float playerX = transform.position.x;
        float playerY = transform.position.y;

        //x
        while(Mathf.Abs(playerX - targetX) > 0.5f)
        {
            //right
           if(playerX < targetX)
            {
                playerX++;
                //if(GameManager.instance.IsRiverTile(playerX, playerY) == false)
                //{
                //    moves.Push(direction.right);
                //}
                moves.Push(direction.right);
            }
            //left
            else if(playerX > targetX)
            {
                playerX--;
                //if (GameManager.instance.IsRiverTile(playerX, playerY) == false)
                //{
                //    moves.Push(direction.left);
                //}
                moves.Push(direction.left);
            }
        }

        //y
        while (Mathf.Abs(playerY - targetY) > 0.5f)
        {
            //up
            if (playerY < targetY)
            {
                playerY++;
                //if (GameManager.instance.IsRiverTile(playerX, playerY) == false)
                //{
                //    moves.Push(direction.up);
                //}
                moves.Push(direction.up);
            }
            //down
            else if (playerY > targetY)
            {
                playerY--;
                //if (GameManager.instance.IsRiverTile(playerX, playerY) == false)
                //{
                //    moves.Push(direction.down);
                //}
                moves.Push(direction.down);
            }
        }

        foreach(direction d in moves)
        {
            print(d);
        }
        Debug.Log("targetX: " + targetX);
        Debug.Log("targetY: " + targetY);
    }

    /// <summary>
    /// raycasts to determine if the next tile is a river space
    /// </summary>
    /// <param name="direction"></param>
    private void IsNextTileRiver(direction direction, int offset=0)
    {
        RaycastHit2D hit;

        switch (direction)
        {
            case direction.down:
                hit = Physics2D.Raycast(transform.position, Vector2.down + Vector2.down * offset, 1, LayerMask.GetMask("River"));
                break;
            case direction.up:
                hit = Physics2D.Raycast(transform.position, Vector2.up + Vector2.up * offset, 1, LayerMask.GetMask("River"));
                break;
            case direction.left:
                hit = Physics2D.Raycast(transform.position, Vector2.left + Vector2.left * offset, 1, LayerMask.GetMask("River"));
                break;
            case direction.right:
            default:
                hit = Physics2D.Raycast(transform.position, Vector2.right + Vector2.right * offset, 1, LayerMask.GetMask("River"));
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
        if(fatigueSlider.value < 1 && !isFatigued)
        {
            Vector3 changeVector;

            //determine the direction vector
            if (dir == direction.down)
            {
                HandleNextTileRiverJumps(dir);
                changeVector = new Vector3(0, -jumpSpeed, 0);
            }
            else if (dir == direction.up)
            {
                HandleNextTileRiverJumps(direction.up);
                changeVector = new Vector3(0, jumpSpeed, 0);
            }
            else if (dir == direction.left)
            {
                HandleNextTileRiverJumps(direction.left);
                sprite.flipX = true;
                changeVector = new Vector3(-jumpSpeed, 0, 0);
            }
            else
            {
                HandleNextTileRiverJumps(direction.right);
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

            EndTravelToTarget();
        }
        else
        {
            moves.Clear();
            canMove = true;
        }

        if (moves.Count > 0)
        {
            StartCoroutine(TravelToTarget(moves.Pop()));
        }
        else
        {
            //destroy the marker flag that shows the tile they are landing on
            Destroy(tempFlag);
            canMove = true;
        }
    }

    /// <summary>
    /// Called from TravelToTarget and handles which animators to use for each jump type
    /// </summary>
    /// <param name="d">direction</param>
    private void HandleNextTileRiverJumps(direction d)
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
            moves.Pop();
            //IsNextTileRiver(direction.down, 1);
            //if (nextTileRiver)
            //{
            //    EndTravelToTarget();
            //    StopAllCoroutines();
            //}

            //SpawnFlag(transform.position.x, transform.position.y - 2);
            
        }
        else
        {
            //SpawnFlag(transform.position.x, transform.position.y - 1);
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
                        //Debug.Log("Deselecting other player");
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
        SetCollidingTile(collision.gameObject);
        //HandleActionPanelButtons();

        //if we jump out of the play space, destroy the player and reset values
        if(collision.tag == "Boundary")
        {
            //Trigger Fire/Quit Player notification
            SelectPlayer(false);

            if (tag == "Lumberjack")
            {
                fireButton.FireSelectedPlayer("Lumberjack");
            }
            else if (tag == "Planter")
            {
                fireButton.FireSelectedPlayer("Planter");
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
        ////Shows the method that this is called from
        //System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();
        //int caller = 1;

        //System.Diagnostics.StackFrame frame = trace.GetFrame(caller);

        //string callerName = frame.GetMethod().Name;

        //UnityEngine.Debug.Log(frame);

        if (GameManager.instance.selectedPlayer == this)
        {
            if (tag == "Lumberjack")
            {
                //check rank to enable promotion button
                if (pointTowardsNextRank < GameManager.instance.rank[currentRank])
                {
                    actionPanel.EnableDisableSingleButton(promoteLumberJackButton.gameObject, false);
                }
                else
                {
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
                        //Debug.Log("Can watter");
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
        if (collidingTile.tag == "Tree")
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

    /// <summary>
    /// Used to set the tile and the animation that the Player is colliding with
    /// </summary>
    /// <param name="obj"></param>
    public void SetCollidingTile(GameObject obj)
    {
        collidingTile = obj;
        collidingTileAnimator = collidingTile.GetComponent<Animator>();
    }
}