using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageCan : MonoBehaviour {

    private LifeManager lifeManager;
    private NotifiationManager notificationManager;
    private Animator animator;

    private void Start()
    {
        lifeManager = GameObject.FindObjectOfType<LifeManager>();
        notificationManager = GameManager.FindObjectOfType<NotifiationManager>();
        animator = FindObjectOfType<BossAnimator>().GetComponent<Animator>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (transform.up * 50));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Jam") || collision.CompareTag("Dispensed"))
        {
            //Destroy(collision.gameObject);
            GameManager.instance.IncreaseJamWasted();

            collision.gameObject.transform.SetParent(transform);

            if(GameManager.instance.GetJamWasted() == 100)
            {
                lifeManager.LoseLife();
                animator.SetTrigger("Angry");
                notificationManager.UpdateNotificationText("-1 Life! Stop wasting Jam!");

                foreach(Transform t in collision.transform.parent.transform)
                {
                    if (t.CompareTag("Jam") || t.CompareTag("Dispensed"))
                    {
                        Destroy(t.gameObject);
                    } 
                }
                //prompt the user telling them what they did wrong
            }
        }
    }

}
