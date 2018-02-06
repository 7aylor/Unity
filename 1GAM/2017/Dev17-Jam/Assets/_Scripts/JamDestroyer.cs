using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamDestroyer : MonoBehaviour {

    private SpawnPipeJam spawnPipeJam;
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        spawnPipeJam = FindObjectOfType<SpawnPipeJam>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Lever.leverDown || collision.GetComponent<Jam>().JamIndex != SpawnJam.jamIndex)
        {
            spawnPipeJam.DecreaseJamCount();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Lever.leverDown || collision.GetComponent<Jam>().JamIndex != SpawnJam.jamIndex)
        {
            spawnPipeJam.DecreaseJamCount();
            Destroy(collision.gameObject);
        }
    }
}
