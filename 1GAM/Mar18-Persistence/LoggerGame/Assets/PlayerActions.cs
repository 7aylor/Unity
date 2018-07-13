using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour {

    public enum PlayerActionType { Lumberjack_Hire, Lumberjack_Chop, Lumberjack_Dig, Lumberjack_Promote,
                                   Planter_Hire, Planter_Plant, Planter_Water, Planter_Promote, Player_Fire}

    [Tooltip("Order: " +
        "\n1) Lumberjack_Hire, " +
        "\n2) Lumberjack_Chop, " +
        "\n3) Lumberjack_Dig, " +
        "\n4) Lumberjack_Promote, " +
        "\n5) Planter_Hire, " +
        "\n6) Planter_Plant, " +
        "\n7) Planter_Water, " +
        "\n8) Planter_Promote, " +
        "\n9) Player_Fire")]
    public GameObject[] actions;
    public delegate void OnUpdatePlayerActions(PlayerActionType action, bool isEnabled);
    public event OnUpdatePlayerActions playerActionsObserver;

	// Use this for initialization
	void Start () {
        playerActionsObserver += UpdatePlayerActions;
        playerActionsObserver(PlayerActionType.Lumberjack_Hire, true);
        playerActionsObserver(PlayerActionType.Planter_Hire, true);
        playerActionsObserver(PlayerActionType.Player_Fire, false);
        playerActionsObserver(PlayerActionType.Lumberjack_Chop, false);
        playerActionsObserver(PlayerActionType.Lumberjack_Dig, false);
        playerActionsObserver(PlayerActionType.Lumberjack_Promote, false);
        playerActionsObserver(PlayerActionType.Planter_Plant, false);
        playerActionsObserver(PlayerActionType.Planter_Water, false);
        playerActionsObserver(PlayerActionType.Planter_Promote, false);
    }

    public void UpdatePlayerActions(PlayerActionType action, bool isEnabled)
    {
        switch (action)
        {
            case PlayerActionType.Lumberjack_Hire:
                actions[0].SetActive(isEnabled);
                break;
            case PlayerActionType.Lumberjack_Chop:
                actions[1].SetActive(isEnabled);
                break;
            case PlayerActionType.Lumberjack_Dig:
                actions[2].SetActive(isEnabled);
                break;
            case PlayerActionType.Lumberjack_Promote:
                actions[3].SetActive(isEnabled);
                break;
            case PlayerActionType.Planter_Hire:
                actions[4].SetActive(isEnabled);
                break;
            case PlayerActionType.Planter_Plant:
                actions[5].SetActive(isEnabled);
                break;
            case PlayerActionType.Planter_Water:
                actions[6].SetActive(isEnabled);
                break;
            case PlayerActionType.Planter_Promote:
                actions[7].SetActive(isEnabled);
                break;
            case PlayerActionType.Player_Fire:
                actions[8].SetActive(isEnabled);
                break;
        }
    }
}
