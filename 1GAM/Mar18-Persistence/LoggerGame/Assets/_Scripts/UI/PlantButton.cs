using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantButton : MonoBehaviour {

    public bool planting;

    private void Start()
    {
        planting = false;
    }

    public void Plant()
    {
        planting = !planting;
        GameManager.instance.selectedPlayer.PlayPlantAnimation(planting);
    }
}