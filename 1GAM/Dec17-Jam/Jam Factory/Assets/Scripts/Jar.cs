using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jar : MonoBehaviour {

    public Texture[] jams;

    private float jarSpeed;
    private Rigidbody2D rb;
    private RectTransform rt;
    private RawImage jamType;
    private bool jarStopped = false;

	// Use this for initialization
	void Start () {
        jarSpeed = GameManager.instance.GetSpeed();
        rb = GetComponent<Rigidbody2D>();
        rt = GetComponent<RectTransform>();
        jamType = transform.GetChild(0).GetComponent<RawImage>();
        SetJamType();
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
        rt.localPosition += Vector3.right * jarSpeed;
    }

    /// <summary>
    /// Stops the jar on the conveyor belt
    /// </summary>
    public void StopJar()
    {
        jarStopped = true;
    }

    public void StartJar()
    {
        jarStopped = false;
    }

    /// <summary>
    /// Pick a random type of jam when the jar is spawned
    /// </summary>
    private void SetJamType()
    {
        int randomNum = Random.Range(0, jams.Length);
        jamType.texture = jams[randomNum];
        Debug.Log(randomNum + " " + jams[randomNum]);
    }

}
