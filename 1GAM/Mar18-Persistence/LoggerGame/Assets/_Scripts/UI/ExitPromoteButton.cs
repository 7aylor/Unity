using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPromoteButton : MonoBehaviour {

    private Player lumberjack;
    private Player planter;

    public enum PanelType { lumberjack, planter}
    public PanelType type;

    /// <summary>
    /// Called when exit button on lumberjack is clicked. Resets the lumberjack's promotion points and updates UI
    /// </summary>
    public void ResetLumberjackPromotionPoints()
    {
        lumberjack = GameObject.FindGameObjectWithTag("Lumberjack").GetComponent<Player>();

        if (lumberjack != null)
        {
            lumberjack.ResetPoints();
        }
    }

    /// <summary>
    /// Called when exit button on lumberjack is clicked. Resets the planter's promotion points and updates UI
    /// </summary>
    public void ResetPlanterPromotionPoints()
    {
        planter = GameObject.FindGameObjectWithTag("Planter").GetComponent<Player>();

        if (planter != null)
        {
            planter.ResetPoints();
        }
    }
}
