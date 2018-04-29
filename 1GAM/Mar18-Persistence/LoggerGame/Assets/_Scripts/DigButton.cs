using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigButton : MonoBehaviour {

    public bool digging;

    private void Start()
    {
        digging = false;
    }

    public void Dig()
    {
        digging = !digging;
        GameManager.instance.selectedPlayer.PlayLumberjackAnimation(digging, "Dig");
    }
}
