﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caveman_Health : MonoBehaviour {

    private Vector2 cavemanStartPos;
    private EnabledCaveman enabledCaveman;
    private Rebirth rebirth;
    private ActivateRunes runes;
    private SpriteRenderer sprite;
    private DialogueWindow dw;
    private int startHealth = 10;
    private int health;
    private Life_Counter life;
    private AudioSource audio;
    public AudioClip hitCaveman;

    private void Start()
    {
        cavemanStartPos = transform.position;
        enabledCaveman = GetComponent<EnabledCaveman>();
        rebirth = FindObjectOfType<Rebirth>();
        health = startHealth;
        runes = FindObjectOfType<ActivateRunes>();
        sprite = GetComponent<SpriteRenderer>();
        dw = FindObjectOfType<DialogueWindow>();
        life = FindObjectOfType<Life_Counter>();
        audio = GetComponent<AudioSource>();
    }

    public void InflictDamage(int damage)
    {
        health -= damage;
        life.UpdateHealthUI(health);
        audio.clip = hitCaveman;
        audio.Play();
        if(health <= 0)
        {
            Debug.Log("Killed played");
            KillPlayer();
        }
        else
        {
            StartCoroutine("Blink");
        }
    }

    private void KillPlayer()
    {
        if(runes.GetActiveRuneCount() > 0)
        {
            transform.position = cavemanStartPos;
            StopCoroutine("Blink");
            enabledCaveman.EnableScripts(false);
            health = startHealth;
            life.UpdateHealthUI(health);
            dw.SetWordTracker(11);
            dw.EnablePanel(true);
        }
        else
        {
            LevelManager.instance.LoadScene("End_Lose");
        }
    }

    private IEnumerator Blink()
    {
        for(int i = 0; i < 6; i++)
        {
            if(i % 2 == 0)
            {
                sprite.enabled = false;
            }
            else
            {
                sprite.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
