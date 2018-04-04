using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteButton : MonoBehaviour {

    public enum PromoteType { chop, lumberJump, plant, water, plantJump}

    public PromoteType type;

    private GameObject lumberjack;
    private GameObject planter;
    private Player lumberjackPlayer;
    private Player planterPlayer;
    private Animator lumberjackAnimator;
    private Animator planterAnimator;
    private LumberjackPromotionPoints lumberjackPoints;

    private void Awake()
    {
        lumberjackPoints = FindObjectOfType<LumberjackPromotionPoints>();
    }


    public void ClickPromote()
    {
        if (lumberjackPoints.numPoints > 0)
        {
            lumberjackPoints.UsePoints();
            if (transform.parent.parent.tag == "PlanterPromote")
            {
                planter = GameObject.FindGameObjectWithTag("Planter");
                planterPlayer = planter.GetComponent<Player>();
                planterAnimator = planter.GetComponent<Animator>();
            }
            else if (transform.parent.parent.tag == "LumberjackPromote")
            {
                lumberjack = GameObject.FindGameObjectWithTag("Lumberjack");
                lumberjackPlayer = lumberjack.GetComponent<Player>();
                lumberjackAnimator = lumberjack.GetComponent<Animator>();
            }

            switch (type)
            {
                case PromoteType.chop:
                    lumberjackPlayer.animatorChopSpeed += 1;
                    lumberjackAnimator.SetFloat("ChopSpeed", lumberjackPlayer.animatorChopSpeed);
                    break;
                case PromoteType.lumberJump:
                    lumberjackPlayer.animatorLumberjackJumpSpeed += 0.5f;
                    lumberjackAnimator.SetFloat("JumpSpeed", lumberjackPlayer.animatorLumberjackJumpSpeed);
                    lumberjackPlayer.jumpSpeed += 0.05f;
                    //more here
                    break;
                case PromoteType.plant:
                    Debug.Log("Planting Speed Upgraded");
                    planterPlayer.animatorPlantSpeed += 1;
                    planterAnimator.SetFloat("PlantSpeed", planterPlayer.animatorPlantSpeed);
                    break;
                case PromoteType.plantJump:
                    planterPlayer.animatorPlanterJumpSpeed += 0.5f;
                    planterAnimator.SetFloat("JumpSpeed", planterPlayer.animatorPlanterJumpSpeed);
                    planterPlayer.jumpSpeed += 0.05f;
                    //more here
                    break;
                case PromoteType.water:
                    planterPlayer.animatorWaterSpeed += 1;
                    planterAnimator.SetFloat("WaterSpeed", planterPlayer.animatorWaterSpeed);
                    break;
            }
        }
    }
}
