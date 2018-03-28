﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour, IPointerClickHandler {

    public AnimatorOverrideController up;
    public AnimatorOverrideController down;
    public AnimatorOverrideController side;
    public GameObject flag;
    public GameObject tree;

    private float jumpSpeed = 0.05f;
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

    private enum direction { up, down, left, right };
    private direction travelDirection;
    private int seedsPlanted;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        actionPanel = FindObjectOfType<ActionPanel>();
        chopButton = FindObjectOfType<ChopButton>();
        plantButton = FindObjectOfType<PlantButton>();
        waterButton = FindObjectOfType<WaterButton>();
        selectionIndicator = transform.GetChild(0).gameObject; //Gets the indicator child game object
    }

    void Start () {
        hasTarget = false;
        isSelected = false;
        seedsPlanted = 0;
	}

    public void HandleMovePlayer()
    {
        if (isSelected == true && hasTarget == false)
        {
            Debug.Log("Handling PlayerMovement");
            //get the distance between the mouse and the player
            Vector3 distanceToMouse = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hasTarget = true;
            targetXY = Input.mousePosition;

            //check if we haven't clicked the player again
            if (Mathf.Abs(distanceToMouse.x) >= 0.5f || Mathf.Abs(distanceToMouse.y) >= 0.5f)
            {
                Debug.Log("Not clicked self");
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

        for (int i = 0; i < 20; i++)
        {
            transform.Translate(changeVector);
            yield return new WaitForSeconds(0.025f);
        }
        hasTarget = false;
        Destroy(tempFlag);
        //CheckTileType();
    }

    private void CheckTileType()
    {
        if(collidingTile != null)
        {
            if (collidingTile.tag == "Tree")
            {
                animator.SetBool("Chop", true);
                sprite.sortingOrder = -1000;
            }
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
            }
            else if (tag == "Planter")
            {
                actionPanel.ActivateButtons(ActionPanel.SelectedPlayer.planter);
            }
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

                if(tag == "Lumberjack")
                {
                    actionPanel.ActivateButtons(ActionPanel.SelectedPlayer.lumberjack);
                }
                else if(tag == "Planter")
                {
                    actionPanel.ActivateButtons(ActionPanel.SelectedPlayer.planter);
                }

                GameManager.instance.playerSelected = true;
                SelectPlayer(true);
            }
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
    }

    public void PlayChopAnimation(bool playAnimation)
    {
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

    public void PlayPlantAnimation(bool playAnimation)
    {
        if(tag == "Planter")
        {
            animator.SetBool("Plant", playAnimation);
            animator.runtimeAnimatorController = down;
        }
    }

    public void PlayWaterAnimation(bool playAnimation)
    {
        if (tag == "Planter")
        {
            animator.SetBool("Water", playAnimation);
            animator.runtimeAnimatorController = down;
        }
    }

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

    public void ClearLumberjackAnimations()
    {
        PlayChopAnimation(false);
        chopButton.chopping = false;
    }

    public void SowSeeds()
    {
        if(seedsPlanted++ >= 5)
        {
            Destroy(collidingTile);
            collidingTile = Instantiate(tree, transform.position, Quaternion.identity);
            collidingTile.GetComponent<Tree>().StartAsSeed();
            seedsPlanted = 0;
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
