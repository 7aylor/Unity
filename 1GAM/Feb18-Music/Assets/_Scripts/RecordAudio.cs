using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAudio : MonoBehaviour {


    public List<int> notes = new List<int>();
    public Dictionary<int, AudioClip> notesToClips = new Dictionary<int, AudioClip>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
