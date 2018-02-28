using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRoundSong : MonoBehaviour {

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartFullSong()
    {
        audioSource.Play();
    }

}
