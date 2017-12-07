using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJam : MonoBehaviour {

    public Color[] jamColors;
    public GameObject jam;
    public int jamIndex;

    public void DispenseJam()
    {
        GameObject j = Instantiate(jam, transform);
        j.GetComponent<SpriteRenderer>().color = jamColors[jamIndex];
    }

    public void ChangeJamType(int index)
    {
        jamIndex = index;
    }
}
