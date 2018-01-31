using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Life_Counter : MonoBehaviour {

    private int health = 15;
    private Text healthText;

	// Use this for initialization
	void Start () {
        health = FindObjectOfType<Caveman_Health>().GetHealth();
        healthText = GetComponent<Text>();
        UpdateHealthUI(health);
	}

    public void UpdateHealthUI(int newHealth)
    {
        health = newHealth;
        healthText.text = health.ToString();
    }

}
