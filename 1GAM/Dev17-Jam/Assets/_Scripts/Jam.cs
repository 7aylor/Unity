using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jam : MonoBehaviour {

    private AudioSource splat;
    private bool hasPlayed = false;

    public int JamIndex { get; set; }

    private void Start()
    {
        splat = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasPlayed == false && collision.gameObject.tag != "NoSplat" && collision.gameObject.tag != "Jam")
        {
            splat.Play();
            hasPlayed = true;
        }
    }


}
