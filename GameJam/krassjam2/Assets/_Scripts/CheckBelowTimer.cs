using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBelowTimer : MonoBehaviour {

    List<GameObject> collidingObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
           
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int DetectFriendsBelowTimer()
    {
        return collidingObjects.Count;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collidingObjects);
        if (!collidingObjects.Contains(collision.gameObject))
        {
            collidingObjects.Add(collision.gameObject);
            Debug.Log(collidingObjects.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collidingObjects);
        if (collidingObjects.Contains(collision.gameObject))
        {
            //remove is not working FIX THIS
            collidingObjects.Remove(collision.gameObject);
            Debug.Log(collidingObjects.Count);
        }
    }
}