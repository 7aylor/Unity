using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenseZone : MonoBehaviour {

    private float timer = 0;
    private SpawnJars spawner;

	// Use this for initialization
	void Start () {
        spawner = FindObjectOfType<SpawnJars>();
	}

    /// <summary>
    /// When a jar passes through the collider, find all jars that aren't dispensed
    /// and stop them from moving
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //reset the timer
        timer = 0;
        //stop the spawner from spawning
        spawner.CanSpawnJars = false;

        Debug.Log("Triggered");

        foreach(Jar jar in FindObjectsOfType<Jar>())
        {
            if(jar.tag != "Dispensed")
            {
                jar.StopJar();
            }
        }

        //start the timer
        StartCoroutine("StartTimer");
    }

    /// <summary>
    /// Waits for the conveyor belt pause, then starts the jars back moving
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartTimer()
    {
        while(timer <= GameManager.instance.GetTimeToPause())
        {
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        foreach (Jar jar in FindObjectsOfType<Jar>())
        {
            jar.StartJar();
        }

        spawner.CanSpawnJars = true;
    }

    //set the dispensed trigger to allow the jar to continue moving down the conveyor belt
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.tag = "Dispensed";
    }


}
