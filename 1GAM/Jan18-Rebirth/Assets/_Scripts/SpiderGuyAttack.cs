using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderGuyAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Debug.Log("Attack");

            gameObject.GetComponent<SpiderGuy>().SpiderGuyState = SpiderGuy.state.attack;
        }
    }
}
