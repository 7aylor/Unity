using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour {

    private ActionPanel actionPanel;
    public PromotePanel lumberJackPromotePanel;
    public PromotePanel planterPromotePanel;

    private void Awake()
    {
        actionPanel = FindObjectOfType<ActionPanel>();
    }

    /// <summary>
    /// Called when Fire button is clicked. Fires selected player and updates UI
    /// </summary>
    public void FireSelectedPlayer(string player="")
    {
        //reset skills
        foreach (Skill s in GameManager.instance.skillLevels)
        {
            if (s.associatedPlayer == "Lumberjack" && (player == "Lumberjack" || 
                (GameManager.instance.selectedPlayer != null && 
                GameManager.instance.selectedPlayer.tag == "Lumberjack")))
            {
                s.level = 1;
                
            }
            if(s.associatedPlayer == "Planter" && (player == "Planter" ||
                (GameManager.instance.selectedPlayer != null && 
                GameManager.instance.selectedPlayer.tag == "Planter")))
            {
                s.level = 1;
            }
        }
        
        //reset lumberjack hired, and action panel stats
        if(player == "Lumberjack" || (GameManager.instance.selectedPlayer != null &&
            GameManager.instance.selectedPlayer.tag == "Lumberjack"))
        {
            GameManager.instance.lumberjackHired = false;
            lumberJackPromotePanel.ResetSkillLevels();
            lumberJackPromotePanel.gameObject.SetActive(false);
        }
        //reset planter hired, and action panel stats
        if (player == "Planter" || (GameManager.instance.selectedPlayer != null &&
            GameManager.instance.selectedPlayer.tag == "Planter"))
        {
            GameManager.instance.planterHired = false;
            planterPromotePanel.ResetSkillLevels();
            planterPromotePanel.gameObject.SetActive(false);
        }

        //no player is passed to the method, ie, when the player runs away from the bear, destroy the player
        if(player == "")
        {
            Destroy(GameManager.instance.selectedPlayer.gameObject);
        }

        //reset the rest and update ui
        GameManager.instance.playerSelected = false;
        actionPanel.selectedPlayer = ActionPanel.SelectedPlayer.none;
        actionPanel.ActionsButtonClick();
        gameObject.SetActive(false);
    }
}