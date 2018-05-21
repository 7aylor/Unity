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
    public void FireSelectedPlayer()
    {
        if(GameManager.instance.playerSelected == true)
        {
            GameManager.instance.playerSelected = false;
            
            if(GameManager.instance.selectedPlayer.tag == "Lumberjack")
            {
                GameManager.instance.lumberjackHired = false;
                lumberJackPromotePanel.ResetSkillLevels();
            }
            if (GameManager.instance.selectedPlayer.tag == "Planter")
            {
                GameManager.instance.planterHired = false;
                planterPromotePanel.ResetSkillLevels();
            }

            Destroy(GameManager.instance.selectedPlayer.gameObject);
            actionPanel.selectedPlayer = ActionPanel.SelectedPlayer.none;
            actionPanel.ActionsButtonClick();
            gameObject.SetActive(false);
        }
    }
}