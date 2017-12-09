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
            //Debug.Log("Jar had " + collision.transform.childCount + " children");

            collision.GetComponent<Jar>().CalculateJamProportion();

            //check how full the jar is and if most of the jam is the correct type
            if (collision.transform.childCount < (jarFull * 0.75f))
            {
                lifeManager.LoseLife("-1 Life! We can't ship a jar with no jam in it!");
                //prompt the user telling them what they did wrong
            }
            else if(collision.GetComponent<Jar>().CalculateJamProportion() < 0.75f)
            {
                lifeManager.LoseLife("-1 Life! It's not hard to fill a jar with the right kind of jam!");
                //prompt the user telling them what they did wrong
            }
        }
    }
}
