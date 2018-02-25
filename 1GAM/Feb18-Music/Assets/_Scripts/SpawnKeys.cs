using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnKeys : MonoBehaviour {

    [SerializeField]
    private int currentClip;
    private AudioSource audioSource;
    public GameObject key;
    private SO.Song currentSong;
    public Slider pianoSlider;
    public Slider bassSlider;
    public Slider drumsSlider;
    public int sliderDistance { get; set; }
    private float clipTimePerFrame = 0;
    private float timeClipHasPlayed = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentSong = FindObjectOfType<AudioManager>().currentSong;
    }

    private void Start()
    {
        sliderDistance = 0;
        currentClip = 0;
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            timeClipHasPlayed += Time.deltaTime;
            switch (gameObject.tag)
            {
                case "Drums":
                    drumsSlider.value += Time.deltaTime / audioSource.clip.length / 4;
                    break;
                case "Bass":
                    bassSlider.value += Time.deltaTime / audioSource.clip.length;
                    break;
                case "Lead":
                    pianoSlider.value += Time.deltaTime / audioSource.clip.length;
                    break;
            }

            if(timeClipHasPlayed >= audioSource.clip.length)
            {
                timeClipHasPlayed = 0;
                sliderDistance++;   
            }
        }
        else
        {
            ResetSlidersToLastCheckPoint();
        }
    }

    public void ResetSlidersToLastCheckPoint()
    {
        switch (gameObject.tag)
        {
            case "Drums":
                drumsSlider.value = sliderDistance;
                break;
            case "Bass":
                bassSlider.value = sliderDistance;
                break;
            case "Lead":
                pianoSlider.value = sliderDistance;
                break;
        }
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
                UpdateInstrumentClip(0);
                break;
            case "Bass":
                UpdateInstrumentClip(1);
                break;
            case "Lead":
                UpdateInstrumentClip(2);
                break;
        }
    }

    private void UpdateInstrumentClip(int instrumentNum)
    {
        if (currentClip < currentSong.instruments[instrumentNum].Count)
        {
            audioSource.clip = currentSong.instruments[instrumentNum][currentClip];
            currentClip++;
        }
        //else
        //{
        //    gameObject.SetActive(false);
        //}
    }
}