﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jar : MonoBehaviour {

    private float jarSpeed;
    private Rigidbody2D rb;
    private bool jarStopped = false;

	// Use this for initialization
	void Start () {
        jarSpeed = GameManager.instance.GetSpeed();
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (jarStopped == false)
        {
            MoveJar();
        }

	}

    /// <summary>
    /// Moves the jar on the conveyor belt by the speed
    /// </summary>
    private void MoveJar()
    {
        transform.Translate(Vector3.right * jarSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Stops the jar on the conveyor belt
    /// </summary>
    public void StopJar()
    {
        jarStopped = true;
    }

    /// <summary>
    /// allows the jar to continue moving
    /// </summary>
    public void StartJar()
    {
        jarStopped = false;
    }

    /// <summary>
    /// When jam goes in the jar, parent the jam to the jar
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jam")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
}
