using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamCatcher : MonoBehaviour {

    public GameObject jamCatcherSpawner;
    private GarbageCan garbageCan;

    private void Start()
    {
        garbageCan = FindObjectOfType<GarbageCan>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.transform.SetParent(garbageCan.transform);
        collision.transform.position = jamCatcherSpawner.transform.position;
    }

}
