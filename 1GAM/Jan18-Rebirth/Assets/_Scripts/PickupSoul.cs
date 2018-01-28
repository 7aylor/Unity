using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSoul : MonoBehaviour {

    private SoulCounter soulCounter;

    private void Start()
    {
        soulCounter = FindObjectOfType<SoulCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            soulCounter.IncreaseSoulCounter();
            Destroy(gameObject);
            //animation
        }
    }

}
