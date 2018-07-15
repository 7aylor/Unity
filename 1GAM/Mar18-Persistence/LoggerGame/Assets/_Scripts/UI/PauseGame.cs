using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    /// <summary>
    /// Pauses the Game
    /// </summary>
    /// <param name="isPaused">true to pause, false to unpause</param>
    public void Pause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
