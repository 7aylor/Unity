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
        //reset
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
            
        if(player == "Lumberjack" || (GameManager.instance.selectedPlayer != null &&
            GameManager.instance.selectedPlayer.tag == "Lumberjack"))
        {
            GameManager.instance.lumberjackHired = false;
            lumberJackPromotePanel.ResetSkillLevels();
        }
        if (player == "Planter" || (GameManager.instance.selectedPlayer != null &&
            GameManager.instance.selectedPlayer.tag == "Planter"))
        {
            GameManager.instance.planterHired = false;
            planterPromotePanel.ResetSkillLevels();
        }

        if(player == "")
        {
            Destroy(GameManager.instance.selectedPlayer.gameObject);
        }

        GameManager.instance.playerSelected = false;
        actionPanel.selectedPlayer = ActionPanel.SelectedPlayer.none;
        actionPanel.ActionsButtonClick();
        gameObject.SetActive(false);
    }
}