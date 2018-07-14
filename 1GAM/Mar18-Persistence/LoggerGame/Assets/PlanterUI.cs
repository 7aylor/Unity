using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanterUI : MonoBehaviour {

    public delegate void planter_UI_Handler(GameManager.planter_UI_State state);
    public event planter_UI_Handler planter_UI_Observer;

    private GameManager.planter_UI_State planter_ui_state;

    public GameManager.planter_UI_State State
    {
        get
        {
            return planter_ui_state;
        }
        set
        {
            planter_ui_state = value;
            OnUpdatePlanterUI(planter_ui_state);
        }

    }

    private void OnUpdatePlanterUI(GameManager.planter_UI_State state)
    {
        if (planter_UI_Observer != null)
        {
            planter_UI_Observer(planter_ui_state);
        }
    }
}
