using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            gameObject.SendMessageUpwards("UnTargetPlayer");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //layer 9 is Enemy
        if (collision.gameObject.layer == 8)
        {
            gameObject.SendMessageUpwards("TargetPlayer", collision.transform);
        }
    }
}