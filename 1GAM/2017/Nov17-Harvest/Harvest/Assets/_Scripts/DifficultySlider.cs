using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour {

    public Text difficultyText;
    private Slider difficultySlider;

	// Use this for initialization
	void Start () {
        difficultySlider = GetComponent<Slider>();
    }

    /// <summary>
    /// Changes the diffulty text when the slider changes
    /// </summary>
    public void ChangeDifficulty()
    {
        if(difficultySlider.value == 0)
        {
            difficultyText.text = "Easy";
            GameManager.instance.Diff = GameManager.difficulty.easy;
        }
        else if (difficultySlider.value == 1)
        {
            difficultyText.text = "Medium";
            GameManager.instance.Diff = GameManager.difficulty.medium;
        }
        else
        {
            difficultyText.text = "Hard";
            GameManager.instance.Diff = GameManager.difficulty.hard;
        }
    }

}
