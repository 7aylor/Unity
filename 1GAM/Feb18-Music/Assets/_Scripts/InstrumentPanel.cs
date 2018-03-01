using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentPanel : MonoBehaviour {

    public GameObject keyImage;
    public Color leadColor;
    public Color bassColor;
    public Color drumColor;
    private SO.Song currentSong;
    private Transform instrumentImagePanel;
    private AudioSource[] audioSources;
    private int numKeys;
    private List<GameObject> keys;
    private Text songName;
    private Text songNameShadow;
    private KeySpawnManager keySpawner;
    private InstrumentImages instrumentImages;

    private void Awake()
    {
        SetTime(0);
        currentSong = FindObjectOfType<AudioManager>().currentSong;
        currentSong.instrumentIndex = 0;
        songNameShadow = transform.GetChild(1).GetComponent<Text>();
        songName = transform.GetChild(2).GetComponent<Text>();
        audioSources = GetComponents<AudioSource>();
        instrumentImagePanel = transform.GetChild(0);
        keys = new List<GameObject>();
        keySpawner = FindObjectOfType<KeySpawnManager>();
        instrumentImages = GetComponentInChildren<InstrumentImages>();
    }

    // Use this for initialization
    void Start ()
    {
        songNameShadow.text = currentSong.songName;
        songName.text = currentSong.songName;

        AddKeysToPanel();
        ConfigureKeysAndSpawners();
        StartPlayClips();
    }

    private void AddKeysToPanel()
    {
        for (int i = 0; i < currentSong.fullInstrumentClips.Count; i++)
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
            if (i % (Mathf.Ceil((float)numSpawners / currentSong.instruments.Count)) != 0)
            {
                child.SetActive(false);
            }
            else
            {
                child.GetComponent<AudioSource>().clip = currentSong.instruments[countOfUsedClips][0];

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
        StopAllCoroutines();
        Debug.Log("Stopped all Coroutines");
        StartCoroutine(PlayClips());
    }

    private IEnumerator PlayClips()
    {
        int clipIndex = 0;
        audioSources[0].Stop();

        for (int i = 0; i < currentSong.fullInstrumentClips.Count; i++)
        {
            Image keyImage = keys[i].GetComponent<Image>();
            keyImage.color = Color.white;
        }

        for (int i = 0; i < currentSong.fullInstrumentClips.Count; i++)
        {
            instrumentImages.UseNextInstrumentHighlightedImage();
            Debug.Log("Playing clip " + (clipIndex + 1));
            AudioClip clip = currentSong.fullInstrumentClips[i];

            Image keyImage = keys[clipIndex].GetComponent<Image>();

            if(i == 0)
            {
                keyImage.color = leadColor;
            }
            else if (i == 1)
            {
                keyImage.color = bassColor;
            }
            else
            {
                keyImage.color = drumColor;
            }

            Color newColor = keyImage.color;
            newColor.a = 1;
            keyImage.color = newColor;

            audioSources[0].clip = clip;
            audioSources[0].Play();

            yield return new WaitForSecondsRealtime(clip.length);
            keyImage.color = Color.white;
            clipIndex++;
        }

        instrumentImages.UseNextInstrumentHighlightedImage();
        //song.instrumentIndex++;
    }

    public void SetTime(float newTime)
    {
        Time.timeScale = newTime;
    }

}
