using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActions : MonoBehaviour {

    #region player_state events
    public delegate void Player_State_UI_Handler(GameManager.lumberjack_UI_State Lumberjack_state, 
                                               GameManager.planter_UI_State planter_state);

    public event Player_State_UI_Handler Lumberjack_UI_Observer;

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
            OnUpdateLumberjackUI(lumberjack_ui_state, planter_ui_state);
        }
    }

    public GameManager.lumberjack_UI_State PlanterState
    {
        get
        {
            return lumberjack_ui_state;
        }
        set
        {
            lumberjack_ui_state = value;
            OnUpdatePlayerStateUI(lumberjack_ui_state, planter_ui_state);
        }
    }

    private void OnUpdatePlayerStateUI(GameManager.lumberjack_UI_State l, GameManager.planter_UI_State p)
    {
        if (Lumberjack_UI_Observer != null)
        {
            Lumberjack_UI_Observer(l, p);
        }
    }
    #endregion

    #region promote events
    public delegate void Player_Promote_UI_Handler();
    public Player_Promote_UI_Handler OnPromote;

    //TODO: Finish Promote event Handlers

    #endregion
}
