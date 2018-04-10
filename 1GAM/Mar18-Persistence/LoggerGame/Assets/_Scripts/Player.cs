using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour, IPointerClickHandler {

    public AnimatorOverrideController up;
    public AnimatorOverrideController down;
    public AnimatorOverrideController side;
    public GameObject flag;
    public GameObject tree;
    public AudioClip chopSfx;
    public AudioClip waterSfx;
    public float jumpSpeed;

    public float animatorLumberjackJumpSpeed;
    public float animatorPlanterJumpSpeed;
    public float animatorPlantSpeed;
    public float animatorWaterSpeed;
    public float animatorChopSpeed;
    public int currentRank;


    private bool isSelected;
    private bool hasTarget; //used to determine if the lumberjack is walking toward a tree
    private Vector3 targetXY; //the target location of the next tree;
    private Animator animator;
    private SpriteRenderer sprite;
    private GameObject selectionIndicator;
    private GameObject tempFlag;
    private GameObject collidingTile;
    private Animator collidingTileAnimator;
    private ActionPanel actionPanel;
    private ChopButton chopButton;
    private PlantButton plantButton;
    private WaterButton waterButton;
    private PromoteLumberJackButton promoteLumberJackButton;
    private PromotePlanterButton promotePlanterButton;
    private AudioSource audioSource;

    private enum direction { up, down, left, right };
    private direction travelDirection;
    private int seedsPlanted;

    private int pointTowardsNextRank;
    private bool canMove = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        actionPanel = FindObjectOfType<ActionPanel>();
        chopButton = FindObjectOfType<ChopButton>();
        plantButton = FindObjectOfType<PlantButton>();
        waterButton = FindObjectOfType<WaterButton>();
        promoteLumberJackButton = FindObjectOfType<PromoteLumberJackButton>();
        promotePlanterButton = FindObjectOfType<PromotePlanterButton>();
        selectionIndicator = transform.GetChild(0).gameObject; //Gets the indicator child game object
    }

    void Start () {
        hasTarget = false;
        isSelected = false;
        seedsPlanted = 0;
        currentRank = 1;
        pointTowardsNextRank = 0;
        animatorLumberjackJumpSpeed = 1;
        animatorPlanterJumpSpeed = 1;
        animatorPlantSpeed = 1;
        animatorWaterSpeed = 1;
        animatorChopSpeed = 1;
        jumpSpeed = 0.05f;

        if(tag == "Planter")
        {
            GameManager.instance.planterHired = true;
        }
        else if(tag == "Lumberjack")
        {
            GameManager.instance.lumberjackHired = true;
        }

    }

    public void HandleMovePlayer()
    {
        if (isSelected == true && hasTarget == false && canMove == true)
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
                    animator.runtimeAnimatorController = side;

                    if (distanceToMouse.x > 0)
                    {
                        tempFlag = Instantiate(flag, new Vector3(transform.position.x - 1, transform.position.y, 0), Quaternion.identity);
                        sprite.flipX = true;
                        StartCoroutine(TravelToTarget(direction.left));
                    }
                    else
                    {
                        tempFlag = Instantiate(flag, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity);
                        sprite.flipX = false;
                        StartCoroutine(TravelToTarget(direction.right));
                    }
                }
                //travel on Y
                else
                {
                    if (distanceToMouse.y > 0)
                    {
                        tempFlag = Instantiate(flag, new Vector3(transform.position.x, transform.position.y - 1, 0), Quaternion.identity);
                        animator.runtimeAnimatorController = down;
                        StartCoroutine(TravelToTarget(direction.down));
                    }
                    else
                    {
                        tempFlag = Instantiate(flag, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
                        animator.runtimeAnimatorController = up;
                        StartCoroutine(TravelToTarget(direction.up));
                    }
                }

                if (tag == "Planter")
                {
                    animator.SetFloat("JumpSpeed", animatorPlanterJumpSpeed);
                }
                else if (tag == "Lumberjack")
                {
                    animator.SetFloat("JumpSpeed", animatorLumberjackJumpSpeed);
                }
                animator.SetTrigger("Jump");
            }
        }
    }

    private IEnumerator TravelToTarget(direction dir)
    {
        Vector3 changeVector;
        if(dir == direction.down)
        {
            changeVector = new Vector3(0, -jumpSpeed, 0);
        }
        else if(dir == direction.up)
        {
            changeVector = new Vector3(0, jumpSpeed, 0);
        }
        else if (dir == direction.left)
        {
            changeVector = new Vector3(-jumpSpeed, 0, 0);
        }
        else
        {
            changeVector = new Vector3(jumpSpeed, 0, 0);
        }

        float numLoops = (1 / jumpSpeed);

        for (int i = 0; i < numLoops; i++)
        {
            transform.Translate(changeVector);
            yield return new WaitForSeconds(0.025f);
        }

        hasTarget = false;
        Destroy(tempFlag);
        HandleActionPanelButtons();
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
            }
            else if (tag == "Planter")
            {
                actionPanel.ActivateButtons(ActionPanel.SelectedPlayer.planter);
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
        HandleActionPanelButtons();
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
                //check rank to enable promotion button
                if (pointTowardsNextRank < GameManager.instance.rank[currentRank])
                {
                    actionPanel.EnableDisableSingleButton(promoteLumberJackButton.gameObject, false);
                }
                else
                {
                    actionPanel.EnableDisableSingleButton(promoteLumberJackButton.gameObject, true);
                }

                //check colliding tile is a tree and enable chop button
                if (collidingTile.tag == "Tree")
                {
                    actionPanel.EnableDisableSingleButton(chopButton.gameObject, true);
                }
                //disable chop button
                else
                {
                    actionPanel.EnableDisableSingleButton(chopButton.gameObject, false);
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

    //called from chop button
    public void PlayChopAnimation(bool playAnimation)
    {
        canMove = false;

        if (tag == "Lumberjack")
        {
            if(playAnimation == true)
            {
                sprite.sortingOrder = -1000;
            }
            else
            {
                sprite.sortingOrder = 1;
            }
            animator.SetBool("Chop", playAnimation);
            animator.runtimeAnimatorController = down;
        }
    }

    //called from Plant Button
    public void PlayPlantAnimation(bool playAnimation)
    {
        canMove = false;
        if(tag == "Planter")
        {
            animator.SetBool("Plant", playAnimation);
            animator.runtimeAnimatorController = down;
        }
    }

    //called from Water button
    public void PlayWaterAnimation(bool playAnimation)
    {
        canMove = false;
        if (tag == "Planter")
        {
            animator.SetBool("Water", playAnimation);
            animator.runtimeAnimatorController = down;
        }
    }

    //called from the animator
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

    //called on tree script with tree falls
    public void ClearLumberjackAnimations()
    {
        PlayChopAnimation(false);
        chopButton.chopping = false;
        HandleActionPanelButtons();
        pointTowardsNextRank++;
        canMove = true;
    }

    public void SowSeeds()
    {
        if(tag == "Planter" && seedsPlanted++ >= 5)
        {
            canMove = true;

            //destroy the grass tile
            Destroy(collidingTile);

            //instantiate the tree and start as a seed
            collidingTile = Instantiate(tree, transform.position, Quaternion.identity);
            collidingTile.GetComponent<Tree>().StartAsSeed();

            //resets the seeds planted count
            seedsPlanted = 0;

            //sets the seed icon active and 
            transform.GetChild(1).gameObject.SetActive(true);
            animator.SetBool("Plant", false);
            plantButton.planting = false;
        }
    }

    public void HandleWatering()
    {
        Tree collidingTree = collidingTile.GetComponent<Tree>();

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

    public void ResetPoints()
    {
        pointTowardsNextRank = 0;
        HandleActionPanelButtons();
    }
}