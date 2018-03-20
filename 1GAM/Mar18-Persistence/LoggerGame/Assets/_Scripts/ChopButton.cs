using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopButton : MonoBehaviour {

    public bool chopping;

    private void Start()
    {
        chopping = false;
    }

    public void Chop()
    {
        chopping = !chopping;
        GameManager.instance.selectedPlayer.PlayChopAnimation(chopping);
    }
}
