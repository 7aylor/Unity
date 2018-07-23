using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour {

    public PromotePanel lumberJackPromotePanel;
    public PromotePanel planterPromotePanel;

    private UIActions uIActions;

    private void Awake()
    {
        uIActions = FindObjectOfType<UIActions>();
    }

    /// <summary>
    /// Called when Fire button is clicked. Fires selected player and updates UI
    /// </summary>
    public void FireSelectedPlayer(string playerType="")
    {
        //reset skills
        foreach (Skill s in GameManager.instance.skillLevels)
        {
            //if this skill is a lumberjack skill and we are firing the lumberjack, clear it
            if (s.associatedPlayer == "Lumberjack" && (playerType == "Lumberjack" || 
                (GameManager.instance.selectedPlayer != null && 
                GameManager.instance.selectedPlayer.tag == "Lumberjack")))
            {
                s.level = 1;
            }
            //if this skill is a planter skill and we are firing the planter, clear it
            if (s.associatedPlayer == "Planter" && (playerType == "Planter" ||
                (GameManager.instance.selectedPlayer != null && 
                GameManager.instance.selectedPlayer.tag == "Planter")))
            {
                s.level = 1;
            }
        }
        
        //reset lumberjack hired, and action panel stats
        if(playerType == "Lumberjack" || (GameManager.instance.selectedPlayer != null &&
            GameManager.instance.selectedPlayer.tag == "Lumberjack"))
        {
            GameManager.instance.lumberjackHired = false;
            lumberJackPromotePanel.ResetSkillLevels();
            lumberJackPromotePanel.gameObject.SetActive(false);
            uIActions.LumberjackState = GameManager.lumberjack_UI_State.None;
        }
        //reset planter hired, and action panel stats
        if (playerType == "Planter" || (GameManager.instance.selectedPlayer != null &&
            GameManager.instance.selectedPlayer.tag == "Planter"))
        {
            GameManager.instance.planterHired = false;
            planterPromotePanel.ResetSkillLevels();
            planterPromotePanel.gameObject.SetActive(false);
            uIActions.PlanterState = GameManager.planter_UI_State.None;
        }

        //no player is passed to the method, ie, when the player runs away from the bear, destroy the player
        if(playerType == "")
        {
            Destroy(GameManager.instance.selectedPlayer.gameObject);
        }

        //reset the rest and disable fire button
        GameManager.instance.playerSelected = false;
        gameObject.SetActive(false);
    }
}