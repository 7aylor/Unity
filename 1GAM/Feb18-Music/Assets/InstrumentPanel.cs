using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentPanel : MonoBehaviour {

    public SO.Song song;
    public GameObject keyImage;
    private Transform instrumentImagePanel;
    private AudioSource[] audioSources;
    private int numKeys;
    private List<GameObject> keys;
    private Text instrumentName;
    private KeySpawnManager keySpawner;

    private void Awake()
    {
        SetTime(0);
        song.instrumentIndex = 0;
        instrumentName = transform.GetComponentInChildren<Text>();
        audioSources = GetComponents<AudioSource>();
        instrumentImagePanel = transform.GetChild(0);
        keys = new List<GameObject>();
        keySpawner = FindObjectOfType<KeySpawnManager>();
    }

    // Use this for initialization
    void Start ()
    {

        instrumentName.text = song.songName;

        AddKeysToPanel();
        ConfigureKeysAndSpawners();
        StartPlayClips();
    }

    private void AddKeysToPanel()
    {
        for (int i = 0; i < song.fullInstrumentClips.Count; i++)
        {
            keys.Add(Instantiate(keyImage, instrumentImagePanel));
        }
    }

    private void ConfigureKeysAndSpawners()
    {
        int numSpawners = keySpawner.transform.childCount;
        int countOfUsedClips = 0;
        for (int i = 0; i < numSpawners; i++)
        {
            GameObject child = keySpawner.transform.GetChild(i).gameObject;

            //number of spawners / number of clips for this instrument
            if (i % (Mathf.Ceil((float)numSpawners / song.instruments.Count)) != 0)
            {
                child.SetActive(false);
            }
            else
            {
                child.GetComponent<AudioSource>().clip = song.instruments[countOfUsedClips][0];

                switch (countOfUsedClips)
                {
                    case 0:
                        child.tag = "Drums";
                        break;
                    case 1:
                        child.tag = "Bass";
                        break;
                    case 2:
                        child.tag = "Lead";
                        break;
                }

                countOfUsedClips++;
                Debug.Log("Spawner " + i + " left active");
            }
        }
    }

    public void StartPlayClips()
    {
        StartCoroutine(PlayClips());
    }

    private IEnumerator PlayClips()
    {
        int clipIndex = 0;
        for(int i = 0; i < song.fullInstrumentClips.Count; i++)
        {
            Debug.Log("Playing clip " + (clipIndex + 1));
            AudioClip clip = song.fullInstrumentClips[i];

            Image keyImage = keys[clipIndex].GetComponent<Image>();
            keyImage.color = Color.red;

            if (audioSources[0].isPlaying == false)
            {
                audioSources[0].clip = clip;
                audioSources[0].Play();
                
            }
            else
            {
                audioSources[1].clip = clip;
                audioSources[1].Play();
            }

            yield return new WaitForSecondsRealtime(clip.length);
            keyImage.color = Color.white;
            clipIndex++;
        }

        //song.instrumentIndex++;
    }

    public void SetTime(float newTime)
    {
        Time.timeScale = newTime;
    }

}
