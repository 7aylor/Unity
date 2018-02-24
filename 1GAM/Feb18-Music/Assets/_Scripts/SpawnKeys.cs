using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKeys : MonoBehaviour {

    private int currentClip;
    private AudioSource audioSource;
    public GameObject key;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        currentClip = 0;
    }

    public void Spawn()
    {
        GameObject newKey = Instantiate(key, transform, false);
    }

    public void UpdateAudioClip()
    {
        switch (gameObject.tag)
        {
            case "Drums":

                break;
            case "Bass":

                break;
            case "Lead":

                break;
        }
    }
}