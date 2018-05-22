using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromoteButton : MonoBehaviour {

    public enum PromoteType { chop, dig, lumberJump, plant, water, plantJump, stamina}
    public PromotionPoints points;

    public PromoteType type;

    private GameObject lumberjack;
    private GameObject planter;
    private Player lumberjackPlayer;
    private Player planterPlayer;
    private Animator lumberjackAnimator;
    private Animator planterAnimator;
    private TMP_Text planterLevelText;
    private TMP_Text lumberjackLevelText;

    public void ClickPromote()
    {
        if (points.numPoints > 0)
        {
            if (transform.parent.parent.tag == "PlanterPromote")
            {
                planter = GameObject.FindGameObjectWithTag("Planter");
                planterPlayer = planter.GetComponent<Player>();
                planterAnimator = planter.GetComponent<Animator>();
                planterLevelText = GameObject.FindGameObjectWithTag("PlanterLevel").GetComponent<TMP_Text>();
                planterPlayer.currentRank++;
                planterLevelText.text = (planterPlayer.currentRank + 1).ToString();
            }
            else if (transform.parent.parent.tag == "LumberjackPromote")
            {
                lumberjack = GameObject.FindGameObjectWithTag("Lumberjack");
                lumberjackPlayer = lumberjack.GetComponent<Player>();
                lumberjackAnimator = lumberjack.GetComponent<Animator>();
                lumberjackLevelText = GameObject.FindGameObjectWithTag("LumberjackLevel").GetComponent<TMP_Text>();
                lumberjackPlayer.currentRank++;
                lumberjackLevelText.text = (lumberjackPlayer.currentRank + 1).ToString();
            }

            switch (type)
            {
                case PromoteType.chop:
                    lumberjackPlayer.animatorChopSpeed += 0.25f;
                    lumberjackAnimator.SetFloat("ChopSpeed", lumberjackPlayer.animatorChopSpeed);
                    break;
                case PromoteType.dig:
                    lumberjackPlayer.animatorDigSpeed += 0.25f;
                    lumberjackAnimator.SetFloat("DigSpeed", lumberjackPlayer.animatorDigSpeed);
                    break;
                case PromoteType.lumberJump:
                    lumberjackPlayer.animatorLumberjackJumpSpeed += 0.25f;
                    lumberjackAnimator.SetFloat("JumpSpeed", lumberjackPlayer.animatorLumberjackJumpSpeed);
                    lumberjackPlayer.jumpSpeed += 0.05f;
                    //more here
                    break;
                case PromoteType.plant:
                    Debug.Log("Planting Speed Upgraded");
                    planterPlayer.animatorPlantSpeed += 0.25f;
                    planterAnimator.SetFloat("PlantSpeed", planterPlayer.animatorPlantSpeed);
                    break;
                case PromoteType.plantJump:
                    planterPlayer.animatorPlanterJumpSpeed += 0.25f;
                    planterAnimator.SetFloat("JumpSpeed", planterPlayer.animatorPlanterJumpSpeed);
                    planterPlayer.jumpSpeed += 0.05f;
                    //more here
                    break;
                case PromoteType.water:
                    planterPlayer.animatorWaterSpeed += 0.25f;
                    planterAnimator.SetFloat("WaterSpeed", planterPlayer.animatorWaterSpeed);
                    break;
                case PromoteType.stamina:
                    if(planterPlayer != null)
                    {
                        planterPlayer.fatigueIncrement -= 0.02f;
                        planterPlayer.recoverFatigueRate += 0.0005f;
                    }
                    else if(lumberjackPlayer != null)
                    {
                        lumberjackPlayer.fatigueIncrement -= 0.02f;
                        lumberjackPlayer.recoverFatigueRate += 0.0005f;
                    }
                    
                    break;
            }
        }
    }
}
