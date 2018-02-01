using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSoul : MonoBehaviour {

    private SoulCounter soulCounter;
    public AudioClip soulSound;

    private void Start()
    {
        soulCounter = FindObjectOfType<SoulCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            soulCounter.IncreaseSoulCounter();
            GameObject obj = new GameObject();
            obj.AddComponent<DestroyOnFinishAudio>();
            AudioSource audio = obj.AddComponent<AudioSource>();
            audio.clip = soulSound;
            audio.Play();

            Destroy(gameObject);
            //animation
        }
    }
}
