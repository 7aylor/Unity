using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private bool isRecording;
    public SO.Song currentSong;
    private Song song;
    private AudioSource[] audioSources;

    //public static AudioManager instance = null;

    private void Awake()
    {
        //if(instance == null)
        //{
        //    instance = this;
        //}
        //else if(instance != this)
        //{
        //    Destroy(gameObject);
        //}

        //DontDestroyOnLoad(gameObject);

        audioSources = GetComponents<AudioSource>();

    }

    // Use this for initialization
    void Start () {
        isRecording = false;
        song = new Song();
	}


    private void SaveSong()
    {
        
    }

    public void AddNoteToSong(AudioClip clip, float pitch, float time)
    {
        song.AddNoteToSong(clip, pitch, time);
    }

    public void PlaySong()
    {
        Debug.Log("Playing Song");
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        float waitTime = 0;
        for (int i = 0; i < song.notes.Count; i++)
        {
            if(i > 0)
            {
                waitTime = song.notes[i].timeStamp - song.notes[i - 1].timeStamp;
            }
           
            yield return new WaitForSecondsRealtime(waitTime);
            if(audioSources[0].isPlaying == false)
            {
                audioSources[0].clip = song.notes[i].sound;
                audioSources[0].pitch = song.notes[i].pitch;
                audioSources[0].Play();
            }
            else if(audioSources[1].isPlaying == false)
            {
                audioSources[1].clip = song.notes[i].sound;
                audioSources[1].pitch = song.notes[i].pitch;
                audioSources[1].Play();
            }
            else
            {
                AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
                newAudioSource.clip = song.notes[i].sound;
                newAudioSource.pitch = song.notes[i].pitch;
                newAudioSource.Play();
                StartCoroutine(DeleteUnneededAudioSource(newAudioSource));
            }   
        }
    }

    private IEnumerator DeleteUnneededAudioSource(AudioSource a)
    {
        while (a.isPlaying)
        {
            yield return null;
        }
        Destroy(a);
    }

    /*Notes from Krass*/
    //public List<int> notes = new List<int>(); //in song manager
    //public Dictionary<int, AudioClip> notesToClips = new Dictionary<int, AudioClip>(); //in song manager
    //public List<int> song = new List<int>(); //in song manager
    //public List<float> timeStamps = new List<float>(); //in song manager

    //private int notesSaved = 0; //in song manager

    ////more songmanager stuff
    //void PlaySong()
    //{
    //    song.AddRange(notes); //drop all saved notes into song list

    //    StartCoroutine(Play());
    //}

    //IEnumerator Play()
    //{
    //    for (int i = 0; i < song.Count; i++)
    //    {
    //        yield return new WaitForSecondsRealTime(timeStamps[i] - Time.Time);
    //        audiomanager.PlaySound(notesToClips[i]);
    //    }
    //}

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (notTouched)
    //    {
    //        notTouched = false;
    //        songManager.notes.Add(songManager.notesSaved, notesToClips[numberedNote]); //example C3 = 37 or whatever the numbering is
    //        timeStamps.Add(Time.time); //cache current time for note
    //        songManager.notesSaved++;
    //    }
    //}

    ////you also would need either an enum of all the notes th
}

[System.Serializable]
public class Song
{
    public List<Note> notes { get; set; }

    public Song()
    {
        notes = new List<Note>();
    }

    public void AddNoteToSong(AudioClip clip, float pitch, float time)
    {
        notes.Add(new Note(clip, pitch, time, notes.Count));
    }
}

public class Note
{
    public AudioClip sound;
    public float pitch;
    public float timeStamp;
    public int noteIndex;

    public Note(AudioClip audioClip, float thisPitch, float timePlayed, int index)
    {
        sound = audioClip;
        pitch = thisPitch;
        timeStamp = timePlayed;
        noteIndex = index;

        //Debug.Log("Successfully Added clip to song: Clip-" + sound.name + 
        //          " Pitch-" + pitch + " Time Stamp-" + timeStamp + " Index-" + index);

    }
}