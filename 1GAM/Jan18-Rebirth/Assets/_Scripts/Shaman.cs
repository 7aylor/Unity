using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Shaman : MonoBehaviour {

    private Animator animator;
    private Exclamation exclamation;
    private ShamanDialogue dialogue;
    private Caveman_Throw cavemanThrow;
    private AudioSource audio;
    public static bool ChangeCursor { get; set; }
    public AudioClip talking;
    public AudioClip summoning;


    // Use this for initialization
    void Awake () {
        animator = GetComponent<Animator>();
        exclamation = GetComponentInChildren<Exclamation>();
        dialogue = GetComponent<ShamanDialogue>();
        cavemanThrow = FindObjectOfType<Caveman_Throw>();
        ChangeCursor = false;
        audio = GetComponent<AudioSource>();
	}

    private void Update()
    {
        if(animator.GetBool("Talking") == true &&  audio.isPlaying == false)
        {
            audio.clip = talking;
            audio.Play();
        }

        if (animator.GetBool("Summon") == true && audio.isPlaying == false)
        {
            audio.clip = summoning;
            audio.Play();
        }
    }

    public void Summon(bool enabled)
    {
        animator.SetBool("Summon", enabled);
    }

    public void Talk(bool enabled)
    {
        animator.SetBool("Talking", enabled);
    }

    private void OnMouseEnter()
    {
        if (ChangeCursor == true)
        {
            dialogue.SetDialogueCursor(true);
            cavemanThrow.CanThrow(false);
        }
    }

    private void OnMouseExit()
    {
        dialogue.SetDialogueCursor(false);
        cavemanThrow.CanThrow(true);
    }
}
