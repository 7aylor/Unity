using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnKeys : MonoBehaviour {

    public GameObject key;
    public Slider pianoSlider;
    public Slider bassSlider;
    public Slider drumsSlider;
    public int sliderDistance { get; set; }
    public bool CanSpawnKeys { get; set; }
    public InstrumentAlert drumsAlert;
    public InstrumentAlert pianoAlert;
    public InstrumentAlert bassAlert;
    public Color keyColor { get; set; }
    public Color leadColor;
    public Color bassColor;
    public Color drumsColor;

    private float clipTimePerFrame = 0;
    private float timeClipHasPlayed = 0;
    private int currentClip;
    private AudioSource audioSource;
    private SO.Song currentSong;

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
            Debug.Log("Spawner " + gameObject.name + " " + currentClip);

            if (currentClip >= currentSong.instruments[instrumentNum].Count)
            {
                CanSpawnKeys = false;
                audioSource.volume = 0;
                transform.parent.GetComponent<KeySpawnManager>().activeSpawners--;
            }
            else
            {
                audioSource.clip = currentSong.instruments[instrumentNum][currentClip];
            }
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

        if(currentClip < 4)
        {
            UpdateAudioClip();
            SuccessAlert();
        }
    }

    public void SuccessAlert()
    {
        switch (gameObject.tag)
        {
            case "Drums":
                drumsSlider.value = sliderDistance;
                drumsAlert.Success();
                break;
            case "Bass":
                bassSlider.value = sliderDistance;
                bassAlert.Success();
                break;
            case "Lead":
                pianoSlider.value = sliderDistance;
                pianoAlert.Success();
                break;
        }
    }

    public void AssignKeyColor()
    {
        switch (gameObject.tag)
        {
            case "Drums":
                keyColor = drumsColor;
                break;
            case "Bass":
                keyColor = bassColor;
                break;
            case "Lead":
                keyColor = leadColor;
                break;
        }
    }
}