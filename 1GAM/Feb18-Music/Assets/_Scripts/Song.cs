using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Song", menuName = "Song")]
    public class Song : ScriptableObject
    {
        public int instrumentIndex { get; set; }
        public List<List<AudioClip>> instruments = new List<List<AudioClip>>();
        public List<AudioClip> bassClips;
        public List<AudioClip> drumClips;
        public List<AudioClip> leadClips;
        public string[] instrumentNames = { "Bass", "Drums", "Lead" };

        public Song()
        {
            instrumentIndex = 0;
            bassClips = new List<AudioClip>();
            drumClips = new List<AudioClip>();
            leadClips = new List<AudioClip>();

            instruments.Add(bassClips);
            instruments.Add(drumClips);
            instruments.Add(leadClips);
        }
    }
}

