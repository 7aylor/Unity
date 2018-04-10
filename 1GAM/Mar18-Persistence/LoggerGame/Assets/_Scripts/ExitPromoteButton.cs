using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPromoteButton : MonoBehaviour {

    private Player lumberjack;
    private Player planter;

    private void Awake()
    {
        lumberjack = GameObject.FindGameObjectWithTag("Lumberjack").GetComponent<Player>();
        planter = GameObject.FindGameObjectWithTag("Planter").GetComponent<Player>();
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
