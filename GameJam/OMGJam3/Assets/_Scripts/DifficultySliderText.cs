using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySliderText : MonoBehaviour {

    Slider diffSlider;
    Text difficultyText;

	// Use this for initialization
	void Start () {
        diffSlider = GetComponent<Slider>();
        difficultyText = GetComponentInChildren<Text>();
        //Adds a listener to the main slider and invokes a method when the value changes.
        diffSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        string diff = "";
        if (diffSlider.value == 0)
        {
            diff = "Easy";
        }
        else if (diffSlider.value == 1)
        {
            diff = "Medium";
        }
        else
        {
            diff = "Hard";
        }

        difficultyText.text = diff;
    }
}
