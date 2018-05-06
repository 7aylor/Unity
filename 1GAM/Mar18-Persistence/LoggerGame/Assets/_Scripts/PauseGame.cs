using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    /// <summary>
    /// When in the menu panel, pause the game
    /// </summary>
    /// <param name="isPaused"></param>
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
