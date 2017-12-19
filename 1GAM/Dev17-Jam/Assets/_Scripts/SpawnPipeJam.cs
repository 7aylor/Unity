using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPipeJam : SpawnJam {

    bool canSpawnJam = true;
    private int jamCount = 0;
    private int maxJam = 80;
    private JamDestroyer jamDestroyer;



	// Use this for initialization
	void Start () {
        jamDestroyer = FindObjectOfType<JamDestroyer>();
    }
	
	// Update is called once per frame
	void Update () {

        if (jamCount < maxJam)
        {
            jamCount++;
            base.DispenseJam();



        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        canSpawnJam = false;
    }

    public void DecreaseJamCount()
    {
        if(jamCount >= 0)
        {
            jamCount--;
        }
    }

    //private bool CheckChildrenForOldJamType()
    //{
    //    foreach (Transform t in transform)
    //    {

    //    }
    //}

}
