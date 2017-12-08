﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnJam : MonoBehaviour {

    public Color[] jamColors;
    public GameObject jam;
    public int jamIndex;
    public Sprite[] images;
    public GameObject jamIndicator;

    private void Update()
    {
        if (Input.GetButton("Strawberry"))
        {
            ChangeJamType(0);
        }
        if (Input.GetButton("Raspberry"))
        {
            ChangeJamType(1);
        }
        if (Input.GetButton("Grapes"))
        {
            ChangeJamType(2);
        }
        //if (Input.GetButton("Blackberry"))
        //{
        //    ChangeJamType(3);
        //}
    }

    public void DispenseJam()
    {
        GameObject j = Instantiate(jam, transform);
        j.GetComponent<SpriteRenderer>().color = jamColors[jamIndex];
    }

    public void ChangeJamType(int index)
    {
        jamIndex = index;
        jamIndicator.GetComponent<Image>().sprite = images[index];
    }
}
