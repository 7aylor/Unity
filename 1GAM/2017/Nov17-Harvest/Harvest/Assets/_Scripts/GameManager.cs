using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour{

    public static GameManager instance = null;
    public AudioClip clickSound;

    public enum difficulty { easy, medium, hard}
    public difficulty Diff { get; set; }
    public int GoldGoal { get; set; }
    public int DiffIndex { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        Diff = difficulty.easy;
        DiffIndex = 1;
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioSource.PlayClipAtPoint(clickSound, Vector3.zero);
        }
    }
}
