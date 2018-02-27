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
    public bool CanSpawnKeys { get; set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentSong = FindObjectOfType<AudioManager>().currentSong;
    }

    private void Start()
    {
        sliderDistance = 0;
        currentClip = 0;
        CanSpawnKeys = true;
    }

    private void FixedUpdate()
    {
        if (audioSource.isPlaying)
        {
            timeClipHasPlayed += Time.fixedDeltaTime / audioSource.clip.length;
            switch (gameObject.tag)
            {
                case "Drums":
                    drumsSlider.value += Time.fixedDeltaTime / 2;
                    break;
                case "Bass":
                    bassSlider.value += Time.fixedDeltaTime / 2;
                    break;
                case "Lead":
                    pianoSlider.value += Time.fixedDeltaTime / 2;
                    break;
            }
        }
        //    if(timeClipHasPlayed >= audioSource.clip.length)
        //    {
        //        Debug.Log("Finished playing clip");
        //        timeClipHasPlayed = 0;
        //        sliderDistance++;
        //    }
        //}
        //else
        //{
        //    ResetSlidersToLastCheckPoint();
        //}
    }

    public void ResetSlidersToLastCheckPoint()
    {
        timeClipHasPlayed = 0;
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
            currentClip++;
            audioSource.clip = currentSong.instruments[instrumentNum][currentClip];
        }
        else
        {
            CanSpawnKeys = false;
            audioSource.volume = 0;
            transform.parent.GetComponent<KeySpawnManager>().activeSpawners--;
        }
    }

    public void StartEndOfClipTimer()
    {
        StopAllCoroutines();
        ResetSlidersToLastCheckPoint();
        StartCoroutine(WaitForEndOfClip());
    }

    private IEnumerator WaitForEndOfClip()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Debug.Log("End of Clip");
        timeClipHasPlayed = 0;
        sliderDistance++;
        //drumsSlider.value = sliderDistance;
        UpdateAudioClip();
    }
}