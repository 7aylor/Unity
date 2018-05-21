using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ActionPanel : MonoBehaviour {

    private Animator animator;
    private GameObject actionsPanel;

    public enum SelectedPlayer { none, planter, lumberjack };
    public SelectedPlayer selectedPlayer;

    public Button[] hireButtons;
    public Button[] planterButtons;
    public Button[] lumberjackButtons;
    public Button fireButton;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        selectedPlayer = SelectedPlayer.none;
        ActionsButtonClick();
    }
	

    public void ActionsButtonClick()
    {
        if(selectedPlayer != SelectedPlayer.planter && selectedPlayer != SelectedPlayer.lumberjack)
        {
            ActivateButtons(SelectedPlayer.none);
        }
    }

    public void ActivateButtons(SelectedPlayer player)
    {
        selectedPlayer = player;

        if(selectedPlayer == SelectedPlayer.none)
        {
            //check if lumberjack is hired
            if (!GameManager.instance.lumberjackHired)
            {
                EnableDisableSingleButton(hireButtons[0].gameObject, true);
            }
            //check if planter is hired
            if (!GameManager.instance.planterHired)
            {
                EnableDisableSingleButton(hireButtons[1].gameObject, true);
            }

            EnableDisableButtons(planterButtons, false);
            EnableDisableButtons(lumberjackButtons, false);
            EnableDisableSingleButton(fireButton.gameObject, false);
        }
        else if (selectedPlayer == SelectedPlayer.lumberjack)
        {
            EnableDisableButtons(hireButtons, false);
            EnableDisableButtons(planterButtons, false);
            EnableDisableButtons(lumberjackButtons, true);
            EnableDisableSingleButton(fireButton.gameObject, true);
        }
        else if (selectedPlayer == SelectedPlayer.planter)
        {
            EnableDisableButtons(hireButtons, false);
            EnableDisableButtons(planterButtons, true);
            EnableDisableButtons(lumberjackButtons, false);
            EnableDisableSingleButton(fireButton.gameObject, true);
        }
    }

    /// <summary>
    /// enables or disables a set of buttons
    /// </summary>
    /// <param name="selectedButtons"></param>
    /// <param name="isActive"></param>
    private void EnableDisableButtons(Button[] selectedButtons, bool isActive)
    {
        foreach (Button button in selectedButtons)
        {
            button.gameObject.SetActive(isActive);
        }
    }

    /// <summary>
    /// enables or disables a single button in the panel
    /// </summary>
    /// <param name="buttonObj"></param>
    /// <param name="isActive"></param>
    public void EnableDisableSingleButton(GameObject buttonObj, bool isActive)
    {
        buttonObj.SetActive(isActive);
    }
}