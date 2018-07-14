using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour {

    private GameObject lumberjack_hire;
    private GameObject lumberjack_chop;
    private GameObject lumberjack_dig;
    private GameObject lumberjack_promote;

    private GameObject planter_hire;
    private GameObject planter_plant;
    private GameObject planter_water;
    private GameObject planter_promote;

    private GameObject fire;

    public void UpdateUI(GameManager.lumberjack_UI_State lumberjackUI, GameManager.planter_UI_State planterUI)
    {
        //TODO: Deal with Promotion buttons and try to fit them into the pattern
        if(lumberjackUI == GameManager.lumberjack_UI_State.None &&
           planterUI == GameManager.planter_UI_State.None)
        {
            SetButtonState(lumberjack_hire, true);
            SetButtonState(lumberjack_chop, false);
            SetButtonState(lumberjack_dig, false);
            SetButtonState(lumberjack_promote, false);

            SetButtonState(planter_hire, true);
            SetButtonState(planter_plant, false);
            SetButtonState(planter_water, false);
            SetButtonState(planter_promote, false);

            SetButtonState(fire, false);
            return;
        }

        switch (lumberjackUI)
        {
            case GameManager.lumberjack_UI_State.Tree:
                SetButtonState(lumberjack_chop, true);
                SetButtonState(lumberjack_dig, true);
                break;
            case GameManager.lumberjack_UI_State.Stump:
                break;
            case GameManager.lumberjack_UI_State.Other:
                break;
        }

        if (planterUI == GameManager.planter_UI_State.None)
        {
            SetButtonState(planter_hire, true);
        }

        if (lumberjackUI == GameManager.lumberjack_UI_State.None)
        {
            SetButtonState(lumberjack_hire, true);
        }
    }

    private void SetButtonGroupState(GameObject[] buttons, bool isEnabled)
    {
        foreach(GameObject g in buttons)
        {
            g.SetActive(isEnabled);
        }
    }

    private void SetButtonState(GameObject button, bool isEnabled)
    {
        button.SetActive(isEnabled);
    }


    #region old
    //public Button[] hireButtons;
    //private GameObject actionsPanel;

    //public enum SelectedPlayer { none, planter, lumberjack };
    //public SelectedPlayer selectedPlayer;

    //// Use this for initialization
    //void Start () {
    //    selectedPlayer = SelectedPlayer.none;
    //    ActionsButtonClick();
    //}

    //public void ActionsButtonClick()
    //{
    //    if(selectedPlayer != SelectedPlayer.planter && selectedPlayer != SelectedPlayer.lumberjack)
    //    {
    //        ActivateButtons(SelectedPlayer.none);
    //    }
    //}

    //public void ActivateButtons(SelectedPlayer player)
    //{
    //    selectedPlayer = player;

    //    if(selectedPlayer == SelectedPlayer.none)
    //    {
    //        //check if lumberjack is hired
    //        if (!GameManager.instance.lumberjackHired)
    //        {
    //            EnableDisableSingleButton(hireButtons[0].gameObject, true);
    //        }
    //        //check if planter is hired
    //        if (!GameManager.instance.planterHired)
    //        {
    //            EnableDisableSingleButton(hireButtons[1].gameObject, true);
    //        }

    //        EnableDisableButtons(planterButtons, false);
    //        EnableDisableButtons(lumberjackButtons, false);
    //        EnableDisableSingleButton(fireButton.gameObject, false);
    //    }
    //    else if (selectedPlayer == SelectedPlayer.lumberjack)
    //    {
    //        EnableDisableButtons(hireButtons, false);
    //        EnableDisableButtons(planterButtons, false);
    //        EnableDisableButtons(lumberjackButtons, true);
    //        EnableDisableSingleButton(fireButton.gameObject, true);
    //    }
    //    else if (selectedPlayer == SelectedPlayer.planter)
    //    {
    //        EnableDisableButtons(hireButtons, false);
    //        EnableDisableButtons(planterButtons, true);
    //        EnableDisableButtons(lumberjackButtons, false);
    //        EnableDisableSingleButton(fireButton.gameObject, true);
    //    }
    //}

    ///// <summary>
    ///// enables or disables a set of buttons
    ///// </summary>
    ///// <param name="selectedButtons"></param>
    ///// <param name="isActive"></param>
    //private void EnableDisableButtons(Button[] selectedButtons, bool isActive)
    //{
    //    foreach (Button button in selectedButtons)
    //    {
    //        button.gameObject.SetActive(isActive);
    //    }
    //}

    ///// <summary>
    ///// enables or disables a single button in the panel
    ///// </summary>
    ///// <param name="buttonObj"></param>
    ///// <param name="isActive"></param>
    //public void EnableDisableSingleButton(GameObject buttonObj, bool isActive)
    //{
    //    buttonObj.SetActive(isActive);
    //}
    #endregion
}