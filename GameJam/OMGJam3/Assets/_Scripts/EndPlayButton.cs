using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlayButton : MonoBehaviour {

    public Difficulty difficulty;

    public void NoTutorial()
    {
        difficulty.playTutorial = false;
    }

}
