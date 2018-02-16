using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public float speed = 0.001f;
    private AudioSource note;

    private void Awake()
    {
        note = GetComponent<AudioSource>();
        ChangePitch();
    }

    // Update is called once per frame
    void Update () {
        MoveKey();
	}

    private void MoveKey()
    {
        transform.Translate(Vector2.left * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Play Sound");
        note.Play();
    }

    private void ChangePitch()
    {
        Debug.Log(Camera.main.WorldToScreenPoint(transform.position).y * 0.005f);

        note.pitch = Camera.main.WorldToScreenPoint(transform.position).y * 0.005f;
    }
}