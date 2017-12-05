using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour {

    private float jarSpeed;
    private Rigidbody2D rb;
    private RectTransform rt;

	// Use this for initialization
	void Start () {
        jarSpeed = GameManager.instance.GetSpeed();
        rb = GetComponent<Rigidbody2D>();
        rt = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveJar();
	}

    /// <summary>
    /// Moves the jar on the conveyor belt by the speed
    /// </summary>
    private void MoveJar()
    {
        rt.localPosition += Vector3.right * jarSpeed;
    }

    private void StopJar()
    {

    }
}
