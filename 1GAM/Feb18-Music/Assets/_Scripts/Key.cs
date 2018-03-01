using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public float speed = 0.001f;
    public bool changePitchbyHeight;
    private AudioSource parentClip;
    private SpawnKeys parentSpawner;
    private InstrumentAlert[] instrumentAlerts;

    [HideInInspector]
    public InstrumentAlert drumsAlert; //these can be left blank intentionally in the inspector
    [HideInInspector]
    public InstrumentAlert pianoAlert;
    [HideInInspector]
    public InstrumentAlert bassAlert;

    private void Awake()
    {
        parentClip = transform.parent.GetComponent<AudioSource>();
        parentSpawner = transform.parent.GetComponent<SpawnKeys>();
        instrumentAlerts = FindObjectsOfType<InstrumentAlert>();

        foreach(InstrumentAlert alert in instrumentAlerts)
        {
            if (alert.gameObject.name.ToLower().Contains("drums"))
            {
                drumsAlert = alert;
            }
            if (alert.gameObject.name.ToLower().Contains("piano"))
            {
                pianoAlert = alert;
            }
            if (alert.gameObject.name.ToLower().Contains("bass"))
            {
                bassAlert = alert;
            }
        }

        if (changePitchbyHeight == true)
        {
            ChangePitch();
        }

        SetKeyColor();
    }

    // Update is called once per frame
    void Update () {
        MoveKey();
	}

    private void MoveKey()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            FindObjectOfType<AudioManager>().AddNoteToSong(parentClip.clip, parentClip.pitch, Time.timeSinceLevelLoad);
            if (parentClip.isPlaying == true)
            {
                Debug.Log("Clip is playing");
                if (parentClip.clip.name.ToLower().Contains("drums")){
                    drumsAlert.Interrupted();
                }
                else if(parentClip.clip.name.ToLower().Contains("bass"))
                {
                    bassAlert.Interrupted();
                }
                else if (parentClip.clip.name.ToLower().Contains("lead"))
                {
                    pianoAlert.Interrupted();
                }
            }

            parentSpawner.StartEndOfClipTimer();
            parentClip.Play();
        }
    }

    private void ChangePitch()
    {
        Debug.Log(Camera.main.WorldToScreenPoint(transform.position).y * 0.005f);

        parentClip.pitch = Camera.main.WorldToScreenPoint(transform.position).y * 0.005f;
    }

    private void SetKeyColor()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = transform.parent.GetComponent<SpawnKeys>().keyColor;
        Color c = sprite.color;
        c.a = 1;
        sprite.color = c;
    }
}