using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

    private const int bufferFrames = 1000;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
    private Rigidbody rigidBody;
    private GameManager manager;
    private int lastFrameRecorded = 0;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        manager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(manager.recording == true)
        {
            Record();
        }
        else
        {
            PlayBack();
        }
    }

    public void PlayBack()
    {
        rigidBody.isKinematic = true;
        int frame = Time.frameCount % lastFrameRecorded;
        print("Reading frame " + frame);
        transform.position = keyFrames[frame].position;
        transform.rotation = keyFrames[frame].rotation;
    }

    public void Record()
    {
        lastFrameRecorded++;
        rigidBody.isKinematic = false;
        int frame = Time.frameCount % bufferFrames;
        float time = Time.time;
        print("Writing frame " + frame);
        keyFrames[frame] = new MyKeyFrame(time, transform.position, transform.rotation);
    }
}

/// <summary>
/// A structure for storing, Time, Position, and Rotation
/// </summary>
public struct MyKeyFrame
{
    public float frameTime;
    public Vector3 position;
    public Quaternion rotation;

    public MyKeyFrame(float time, Vector3 position, Quaternion rotation)
    {
        this.frameTime = time;
        this.position = position;
        this.rotation = rotation;
    }
}
