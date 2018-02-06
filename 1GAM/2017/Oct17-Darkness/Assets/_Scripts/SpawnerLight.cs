using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLight : MonoBehaviour {

    private Light light;

    private void Start()
    {
        light = GetComponent<Light>();
    }

    /// <summary>
    /// Allows outside access to set the color of the spawner light
    /// </summary>
    /// <param name="color"></param>
    public void SetLightColor(Color color)
    {
        light.color = color;
    }
}
