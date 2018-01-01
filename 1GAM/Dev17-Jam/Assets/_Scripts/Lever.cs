using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    public SpawnJam jamSpawner;

    public float topY;
    public float botY;
    private float leverPressure = 0;
    public static bool leverDown = false;
    private float spawnInterval = 0.3f;
    private float lastSpawned = 0;
    private JamDispenser jamDispenser;

    private void Start()
    {
        jamDispenser = FindObjectOfType<JamDispenser>();
    }


    private void Update()
    {
        if(leverDown == true)
        {
            if(Time.fixedTime - lastSpawned > spawnInterval)
            {
                lastSpawned = Time.fixedTime;
                jamSpawner.DispenseJam();
            }
        }
    }

    void OnMouseDrag()
    {
        leverDown = true;
        jamDispenser.PlayDispenseAnimation(true);

        float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        y = Mathf.Clamp(y, botY, topY);
        transform.position = new Vector3(transform.position.x, y, 0);

        leverPressure = Mathf.Abs(transform.position.y - topY);
        spawnInterval = DetermineFrameDelay();
    }

    /// <summary>
    /// Resets all of the lever variables to defaults
    /// </summary>
    private void OnMouseUp()
    {
        leverDown = false;
        jamDispenser.PlayDispenseAnimation(false);
        leverPressure = 0;
        transform.position = new Vector3(transform.position.x, topY, 0);
    }

    private float DetermineFrameDelay()
    {
        if(leverPressure <= 0.5)
        {
            jamDispenser.SetPlaySpeed(0.5f);
            return 0.15f;
        }
        else if (leverPressure > 0.5 && leverPressure < 1)
        {
            jamDispenser.SetPlaySpeed(0.65f);
            return 0.1f;
        }
        else if (leverPressure > 1 && leverPressure < 1.25)
        {
            jamDispenser.SetPlaySpeed(0.85f);
            return 0.05f;
        }
        else
        {
            jamDispenser.SetPlaySpeed(1f);
            return 0.01f;
        }
    }

    private IEnumerator SpawnJam()
    {
        for(int i = 0; i < spawnInterval; i++)
        {
            if(i + 1 == spawnInterval)
            {
                jamSpawner.DispenseJam();
            }
            yield return new WaitForEndOfFrame();
        }
    }

}