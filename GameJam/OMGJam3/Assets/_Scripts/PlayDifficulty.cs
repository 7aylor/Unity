using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayDifficulty : MonoBehaviour {

    public Slider diffSlider;
    public Difficulty difficulty;
    
    public void GetDifficultyFromSlider()
    {
        if(diffSlider.value == 0)
        {
            difficulty.SetDifficulty(Difficulty.DifficultyOptions.Easy);
        }
        else if(diffSlider.value == 1)
        {
            difficulty.SetDifficulty(Difficulty.DifficultyOptions.Medium);
        }
        else
        {
            difficulty.SetDifficulty(Difficulty.DifficultyOptions.Hard);
        }
    }
}
