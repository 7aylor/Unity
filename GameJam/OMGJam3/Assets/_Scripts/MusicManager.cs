using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {


    public AudioClip[] clips;
    AudioSource music;

    public static MusicManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log(gameObject.name + " Destroyed on Load");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        music = GetComponent<AudioSource>();
    }

    public void EndScene()
    {

    }

    private IEnumerator FadeOutMusic()
    {
        for(int i = 0; i < 10; i++)
        {
            music.volume -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FadeInMusic()
    {
        for (int i = 0; i < 10; i++)
        {
            music.volume += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
