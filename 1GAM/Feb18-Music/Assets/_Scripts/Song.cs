using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Song", menuName = "Song")]
    public class Song : ScriptableObject
    {
        //public int instrumentIndex { get; set; }
        //public List<List<AudioClip>> instruments = new List<List<AudioClip>>();
        //public List<AudioClip> bassClips;
        //public List<AudioClip> drumClips;
        //public List<AudioClip> leadClips;
        //public string[] instrumentNames = { "Bass", "Drums", "Lead" };

        //public Song()
        //{
        //    instrumentIndex = 0;
        //    bassClips = new List<AudioClip>();
        //    drumClips = new List<AudioClip>();
        //    leadClips = new List<AudioClip>();

        //    instruments.Add(bassClips);
        //    instruments.Add(drumClips);
        //    instruments.Add(leadClips);
        //}

        public int phraseIndex { get; set; }
        public List<List<AudioClip>> phrases = new List<List<AudioClip>>();
        public List<AudioClip> phrase1;
        public List<AudioClip> phrase2;
        public List<AudioClip> phrase3;
        public List<AudioClip> phrase4;
        public string[] phraseNames = { "Phrase1", "Phrase2", "Phrase3", "Phrase4" };

        public Song()
        {
            phraseIndex = 0;
            phrase1 = new List<AudioClip>();
            phrase2 = new List<AudioClip>();
            phrase3 = new List<AudioClip>();
            phrase4 = new List<AudioClip>();

            phrases.Add(phrase1);
            phrases.Add(phrase2);
            phrases.Add(phrase3);
            phrases.Add(phrase4);
        }
    }
}

