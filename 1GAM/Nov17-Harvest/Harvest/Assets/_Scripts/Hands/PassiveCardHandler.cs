using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveCardHandler : MonoBehaviour {

    /// <summary>
    /// Handles the passive hand card functionality
    /// </summary>
    /// <param name="card"></param>
    public void HandleCard(string cardName)
    {

        switch (cardName)
        {
            case "Bank":
                HandleBank();
                break;
            case "Family":
                HandleFamily();
                break;
            case "Fertilizer":
                HandleFertilizer();
                break;
            case "Irrigation":
                HandleIrrigation();
                break;
            default:
                break;
        }
    }

    private void HandleBank()
    {
        GoldManager.instance.SetGoldAmount(GoldManager.instance.GetGoldAmount() + 3);
    }

    private void HandleFamily()
    {
        if(ActionPointManager.instance.GetStartActionPoints() < 4)
        {
            ActionPointManager.instance.IncreaseStartActionPoints();
        }
        
        GoldManager.instance.SetGoldAmount(GoldManager.instance.GetGoldAmount() - 2);
    }

    private void HandleFertilizer()
    {

    }

    private void HandleIrrigation()
    {

    }
}
