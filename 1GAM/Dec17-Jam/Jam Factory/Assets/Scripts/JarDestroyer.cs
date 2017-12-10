using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarDestroyer : MonoBehaviour {

    private LifeManager lifeManager;
    private PointManager pointManager;
    private NotifiationManager notificationManager;

    private int jarFull = 80;

    private void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
        pointManager = FindObjectOfType<PointManager>();
        notificationManager = FindObjectOfType<NotifiationManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        //if we are destroying a jar, check if we lose a life
        if (collision.GetComponent<Jar>())
        {
            //Debug.Log("Jar had " + collision.transform.childCount + " children");

            collision.GetComponent<Jar>().CalculateJamProportion();

            //check how full the jar is
            if (collision.transform.childCount < (jarFull * 0.75f))
            {
                lifeManager.LoseLife();
                notificationManager.UpdateNotificationText("-1 Life! We can't sell a jar with no jam in it!");
            }
            //if most of the jam is the correct type
            else if (collision.gameObject.GetComponent<Jar>().CalculateJamProportion() < 0.75f)
            {
                lifeManager.LoseLife();
                notificationManager.UpdateNotificationText("-1 Life! It's not hard to fill a jar with the right kind of jam!");
            }
            else
            {
                pointManager.IncreasePoints();
            }
        }
    }
}
