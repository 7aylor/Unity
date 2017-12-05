using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarDestroyer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Destroyer Triggered");
        Destroy(collision.gameObject);
    }

}
