using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageCan : MonoBehaviour {

    private LifeManager lifeManager;

    private void Start()
    {
        lifeManager = GameObject.FindObjectOfType<LifeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Jam")
        {
            //Destroy(collision.gameObject);
            GameManager.instance.IncreaseJamWasted();
            if(GameManager.instance.GetJamWasted() == 100)
            {
                lifeManager.LoseLife("-1 Life! Stop wasting Jam!");

                foreach(Transform t in collision.transform.parent.transform)
                {
                    Destroy(t.gameObject);
                }
                //prompt the user telling them what they did wrong
            }
        }
    }

}
