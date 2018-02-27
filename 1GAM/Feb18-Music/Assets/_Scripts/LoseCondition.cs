using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour {

    public GameObject loseMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        loseMenu.SetActive(true);
        Debug.Log("You have lost");
    }

}
