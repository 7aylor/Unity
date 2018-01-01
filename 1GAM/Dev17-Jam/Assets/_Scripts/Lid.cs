using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lid : MonoBehaviour {

    public GameObject jamCatcherSpawner;
    private GarbageCan garbageCan;

    private void Start()
    {
        garbageCan = FindObjectOfType<GarbageCan>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Dispensed")
        {
            collision.gameObject.transform.position = jamCatcherSpawner.transform.position;
        }
    }

}
