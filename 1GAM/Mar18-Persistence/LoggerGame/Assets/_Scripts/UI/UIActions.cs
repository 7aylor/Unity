using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActions : MonoBehaviour {

    #region player_state events, Observer pattern used to update the buttons related to the player's tile
    public delegate void Player_State_UI_Handler(GameManager.lumberjack_UI_State Lumberjack_state, 
                                               GameManager.planter_UI_State planter_state);

    public event Player_State_UI_Handler Player_State_Changed;

    private GameManager.lumberjack_UI_State lumberjack_ui_state;
    private GameManager.planter_UI_State planter_ui_state;

    public GameManager.lumberjack_UI_State LumberjackState
    {
        get
        {
            return lumberjack_ui_state;
        }
        set
        {
            lumberjack_ui_state = value;
            OnPlayerStateChanged();
        }
    }

    public GameManager.planter_UI_State PlanterState
    {
        get
        {
            return planter_ui_state;
        }
        set
        {
            planter_ui_state = value;
            OnPlayerStateChanged();
        }
    }

    private void OnPlayerStateChanged()
    {
        if (Player_State_Changed != null)
        {
            Player_State_Changed(lumberjack_ui_state, planter_ui_state);
        }
    }
    #endregion

    #region Promote buttons, Observer pattern used to update the promote buttons based off of player's progress
    public delegate void Promote_UI_Handler(GameManager.promote_UI_State state);
    public Promote_UI_Handler Promote_State_Changed;

    private GameManager.promote_UI_State promote_UI_State;

    public GameManager.promote_UI_State PromoteState
    {
        get
        {
            return promote_UI_State;
        }
        set
        {
            promote_UI_State = value;
            OnPromoteStateChanged();
        }
    }

    private void OnPromoteStateChanged()
    {
        if(Promote_State_Changed != null)
        {
            Promote_State_Changed(promote_UI_State);
        }
    }
    #endregion
}
