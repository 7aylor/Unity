using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public float speed = 0.001f;
    public bool changePitchbyHeight;
    private AudioSource parentClip;

    private void Awake()
    {
        parentClip = transform.parent.GetComponent<AudioSource>();

        if(changePitchbyHeight == true)
        {
            ChangePitch();
        }
    }

    // Update is called once per frame
    void Update () {
        MoveKey();
	}

    private void MoveKey()
    {
        transform.Translate(Vector2.left * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            AudioManager.instance.AddNoteToSong(parentClip.clip, parentClip.pitch, Time.timeSinceLevelLoad);
            if (parentClip.isPlaying == false)
            {
                transform.parent.GetComponent<SpawnKeys>().UpdateAudioClip();
            }
            else
            {
                transform.parent.GetComponent<SpawnKeys>().ResetSlidersToLastCheckPoint();
            }

            parentClip.Play();
        }
    }

    private void ChangePitch()
    {
        Debug.Log(Camera.main.WorldToScreenPoint(transform.position).y * 0.005f);

        parentClip.pitch = Camera.main.WorldToScreenPoint(transform.position).y * 0.005f;
    }
}