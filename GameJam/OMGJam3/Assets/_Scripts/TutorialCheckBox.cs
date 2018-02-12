using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialCheckBox : MonoBehaviour {

    public Difficulty difficulty;
    Toggle tutorialCheckBox;

    private void Awake()
    {
        tutorialCheckBox = GetComponent<Toggle>();
    }

    public void DoTutorial()
    {
        difficulty.playTutorial = tutorialCheckBox.isOn;
    }

}
