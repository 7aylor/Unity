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
        Debug.Log("Lid collision");
        if(collision.gameObject.tag == "Dispensed")
        {
            Debug.Log("Lid collision with Jam");
            //collision.gameObject.transform.position = jamCatcherSpawner.transform.position;
            //collision.transform.SetParent(garbageCan.transform);

            collision.gameObject.transform.position = jamCatcherSpawner.transform.position;
            //collision.transform.SetParent(null);
        }
    }

}
