using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private Slider healthBar;
    private int maxHealth;
	// Use this for initialization
	void Start () {
        healthBar = GetComponent<Slider>();
        maxHealth = transform.parent.parent.GetComponent<SpiderGuy>().GetMaxHealth();
        healthBar.minValue = 1;
        healthBar.maxValue = maxHealth;
        UpdateHealthBar(maxHealth);
	}
	
    public void UpdateHealthBar(int newHealth)
    {
        healthBar.value = newHealth;
    }
}
