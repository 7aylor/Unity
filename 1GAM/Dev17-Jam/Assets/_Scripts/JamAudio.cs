using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamAudio : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip lastLifeSong;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
    public void LastLifeSong()
    {
        //StartCoroutine("FadeOut");
        audioSource.clip = lastLifeSong;
        audioSource.Play();
    }

    private IEnumerator FadeOut()
    {
        for(int i = 0; i < 20; i++)
        {
            audioSource.volume -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine("FadeIn");
    }

    private IEnumerator FadeIn()
    {
        for (int i = 0; i < 20; i++)
        {
            audioSource.volume += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        audioSource.Play();
    }
}
