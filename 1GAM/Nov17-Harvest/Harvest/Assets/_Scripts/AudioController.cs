using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    private float defaultVolume;
    private AudioSource audioSource;
    private int songIndex;

    public AudioClip[] songs;
    public static AudioController instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultVolume = instance.audioSource.volume;
        songIndex = 0;
    }

    public void transitionSong()
    {
        Coroutine c = StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        while(audioSource.volume > 0)
        {
            audioSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }

        PickNextSong();

        yield return StartCoroutine("FadeIn");

    }


    IEnumerator FadeIn()
    {
        while (audioSource.volume < defaultVolume)
        {
            audioSource.volume += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void PickNextSong()
    {
        songIndex++;

        if (songIndex >= songs.Length)
        {
            songIndex = 0;
        }

        audioSource.clip = songs[songIndex];
        audioSource.Play();
    }
}
