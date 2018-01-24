using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caveman_Health : MonoBehaviour {

    private int health = 3;

    public void InflictDamage(int damage)
    {
        health -= damage;
    }
}
