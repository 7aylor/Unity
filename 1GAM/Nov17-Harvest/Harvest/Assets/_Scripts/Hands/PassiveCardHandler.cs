using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveCardHandler : MonoBehaviour {

    /// <summary>
    /// Handles the passive hand card functionality
    /// </summary>
    /// <param name="card"></param>
    public void HandleCard(GameObject card)
    {
        string cardName = card.GetComponent<Image>().sprite.name;

        switch (cardName)
        {
            case "Bank":

                break;
            case "Bulldozer":

                break;
            case "Family":
                HandleFamily();
                break;
            case "Fertilizer":

                break;
            case "Irrigation":

                break;
        }
    }

    private void HandleFamily()
    {
        ActionPointManager.instance.IncreaseStartActionPoints();
    }

}
