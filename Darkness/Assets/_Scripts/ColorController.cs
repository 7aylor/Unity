using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour {

    public Material[] colors;
    public Material startColor;

	// Use this for initialization
	void Awake () {
        SetRandomColor();
	}

    private void SetRandomColor()
    {
        int index = Random.Range(0, colors.Length);
        startColor = colors[index];
    }

}
