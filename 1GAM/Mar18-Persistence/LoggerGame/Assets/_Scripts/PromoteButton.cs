using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteButton : MonoBehaviour {

    public enum PromoteType { chop, lumberJump, plant, water, plantJump}
    public PromotionPoints points;

    public PromoteType type;

    private GameObject lumberjack;
    private GameObject planter;
    private Player lumberjackPlayer;
    private Player planterPlayer;
    private Animator lumberjackAnimator;
    private Animator planterAnimator;
   

    public void ClickPromote()
    {
        if (points.numPoints > 0)
        {
            points.UsePoints();
            if (transform.parent.parent.tag == "PlanterPromote")
            {
                planter = GameObject.FindGameObjectWithTag("Planter");
                planterPlayer = planter.GetComponent<Player>();
                planterAnimator = planter.GetComponent<Animator>();
                planterPlayer.currentRank++;
            }
            else if (transform.parent.parent.tag == "LumberjackPromote")
            {
                lumberjack = GameObject.FindGameObjectWithTag("Lumberjack");
                lumberjackPlayer = lumberjack.GetComponent<Player>();
                lumberjackAnimator = lumberjack.GetComponent<Animator>();
                lumberjackPlayer.currentRank++;
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
