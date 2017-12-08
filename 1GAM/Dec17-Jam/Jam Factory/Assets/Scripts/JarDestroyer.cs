using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarDestroyer : MonoBehaviour {

    private LifeManager lifeManager;

    private int jarFull = 80;

    private void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        //if we are destroying a jar, check if we lose a life
        if (collision.GetComponent<Jar>())
        {
            Debug.Log("Jar had " + collision.transform.childCount + " children");

            //check how full the jar is and if most of the jam is the correct type
            if(collision.transform.childCount < (jarFull * 0.75))
            {
                lifeManager.LoseLife();
            }

        }
    }

}
