using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGame : MonoBehaviour {

    private PauseGame pause;

    private void Awake()
    {
        pause = GetComponent<PauseGame>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// When the LosePanel object is enabled, pause the game
    /// </summary>
    private void OnEnable()
    {
        pause.Pause(true);
    }
}
