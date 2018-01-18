using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SendMessageUpwards("TargetPlayer", collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Player Out of Sight");
    }

}
