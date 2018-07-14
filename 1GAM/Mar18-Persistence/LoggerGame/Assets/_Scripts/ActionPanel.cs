using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanel : MonoBehaviour {

    public GameObject lumberjack_hire;
    public GameObject lumberjack_chop;
    public GameObject lumberjack_dig;
    public GameObject lumberjack_promote;

    public GameObject planter_hire;
    public GameObject planter_plant;
    public GameObject planter_water;
    public GameObject planter_promote;

    public GameObject fire;

    private UIActions uiActions;

    private void Start()
    {
        uiActions = GetComponent<UIActions>();
        uiActions.Player_State_Changed += UpdatePlayerStateUI;
        uiActions.PlanterState = GameManager.planter_UI_State.None;
        uiActions.LumberjackState = GameManager.lumberjack_UI_State.None;

        uiActions.Promote_State_Changed += UpdatePromoteStateUI;
        uiActions.PromoteState = GameManager.promote_UI_State.None;
    }

    private void UpdatePlayerStateUI(GameManager.lumberjack_UI_State lumberjackUI, GameManager.planter_UI_State planterUI)
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
                SetButtonState(lumberjack_dig, false);
                break;
            case GameManager.lumberjack_UI_State.Stump:
                SetButtonState(lumberjack_chop, false);
                SetButtonState(lumberjack_dig, true);
                break;
            case GameManager.lumberjack_UI_State.Other:
            case GameManager.lumberjack_UI_State.None:
                SetButtonState(lumberjack_chop, false);
                SetButtonState(lumberjack_dig, false);
                break;
        }

        switch (planterUI)
        {
            case GameManager.planter_UI_State.Grass:
                SetButtonState(planter_plant, true);
                SetButtonState(planter_water, false);
                break;
            case GameManager.planter_UI_State.Seed:
                SetButtonState(planter_plant, false);
                SetButtonState(planter_water, true);
                break;
            case GameManager.planter_UI_State.Other:
            case GameManager.planter_UI_State.None:
                SetButtonState(planter_plant, false);
                SetButtonState(planter_water, false);
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

    private void UpdatePromoteStateUI(GameManager.promote_UI_State state)
    {
        switch (state)
        {
            case GameManager.promote_UI_State.None:
                SetButtonState(lumberjack_promote, false);
                SetButtonState(planter_promote, false);
                break;
            case GameManager.promote_UI_State.Promote_lumberjack:
                SetButtonState(lumberjack_promote, true);
                SetButtonState(planter_promote, false);
                break;
            case GameManager.promote_UI_State.Promote_Planter:
                SetButtonState(lumberjack_promote, false);
                SetButtonState(planter_promote, true);
                break;
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
}