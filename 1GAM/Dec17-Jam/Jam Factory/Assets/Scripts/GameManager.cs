﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Texture2D cursor0;
    public Texture2D cursor1;
    

    private bool clicked = false;
    private float speed = 1f;
    private float spawnSpeed = 3;

    public static GameManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log(gameObject.name + " Destroyed on Load");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start () {
        Cursor.SetCursor(cursor0, new Vector2(0,0), CursorMode.Auto); //0.325f, 0.99f
        Cursor.visible = true;
	}

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (clicked == false)
            {
                Cursor.SetCursor(cursor1, new Vector2(0, 0), CursorMode.Auto);
                clicked = true;
            }
        }
        else
        {
            Cursor.SetCursor(cursor0, new Vector2(0, 0), CursorMode.Auto);
            clicked = false;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetSpawnSpeed()
    {
        return spawnSpeed;
    }

}
