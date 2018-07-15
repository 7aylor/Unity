using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterButton : MonoBehaviour {

    public bool watering;

    private void Start()
    {
        watering = false;
    }

    public void Water()
    {
        watering = !watering;
        GameManager.instance.selectedPlayer.PlayWaterAnimation(watering);
    }
}
