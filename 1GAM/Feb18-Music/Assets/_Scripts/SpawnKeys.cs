using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKeys : MonoBehaviour {

    public GameObject key;

    public void Spawn()
    {
        Instantiate(key, transform.position, Quaternion.identity);
    }

}
