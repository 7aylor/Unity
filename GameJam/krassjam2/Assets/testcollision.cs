using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcollision : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered by " + collision.gameObject.name);
    }
}
