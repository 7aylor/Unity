using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPromoteButton : MonoBehaviour {

    private Player lumberjack;
    private Player planter;

    public enum PanelType { lumberjack, planter}
    public PanelType type;

    private void Awake()
    {
        if(type == PanelType.lumberjack)
        {
            lumberjack = GameObject.FindGameObjectWithTag("Lumberjack").GetComponent<Player>();
        }
        else
        {
            planter = GameObject.FindGameObjectWithTag("Planter").GetComponent<Player>();
        }
    }

    public void ResetLumberjackPromotionPoints()
    {
        if(lumberjack != null)
        {
            lumberjack.ResetPoints();
        }
    }

    public void ResetPlanterPromotionPoints()
    {
        if (planter != null)
        {
            planter.ResetPoints();
        }
    }
}
