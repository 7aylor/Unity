using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictDamage : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpiderGuy enemy = collision.gameObject.GetComponent<SpiderGuy>();

        if(enemy != null)
        {
            enemy.InflictDamage(1);
            Destroy(gameObject);
        }
    }
}
