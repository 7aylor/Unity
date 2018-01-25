using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSoul : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SoulCounter.IncreaseSoulCounter();
            Destroy(gameObject);
            //animation
        }
    }

}
