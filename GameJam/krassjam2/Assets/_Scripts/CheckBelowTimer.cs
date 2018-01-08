using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBelowTimer : MonoBehaviour {

    List<GameObject> collidingObjects = new List<GameObject>();

    public int FriendsBelowTimer()
    {
        return collidingObjects.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collidingObjects.Contains(collision.gameObject))
        {
            collidingObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collidingObjects.Contains(collision.gameObject))
        {
            collidingObjects.Remove(collision.gameObject);
        }
    }
}