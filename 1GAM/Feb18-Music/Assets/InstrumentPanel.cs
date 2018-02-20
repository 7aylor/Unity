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

    private void Awake()
    {
        Time.timeScale = 0;
        song.instrumentIndex = 0;
        instrumentName = transform.GetComponentInChildren<Text>();
        audioSources = GetComponents<AudioSource>();
        instrumentImagePanel = transform.GetChild(0);
        keys = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {

        instrumentName.text = song.instrumentNames[song.instrumentIndex];

        for(int i = 0; i < song.instruments[0].Count; i++)
        {
            keys.Add(Instantiate(keyImage, instrumentImagePanel));
        }

        StartCoroutine(PlayClips());
	}

    private IEnumerator PlayClips()
    {
        int clipIndex = 0;
        for(int i = 0; i < song.instruments[song.instrumentIndex].Count; i++)
        {
            Debug.Log("Playing clip " + (clipIndex + 1));
            AudioClip clip = song.instruments[song.instrumentIndex][clipIndex];

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

        song.instrumentIndex++;
    }


}
