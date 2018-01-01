using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour {

    public AudioClip angry;
    public AudioClip happy;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    public void PlayAngrySounds()
    {
        audioSource.clip = angry;
        audioSource.Play();
    }

    public void PlayHappySounds()
    {
        audioSource.clip = happy;
        audioSource.Play();
    }
}
