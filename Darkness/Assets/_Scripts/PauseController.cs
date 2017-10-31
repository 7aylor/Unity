using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

    public GameObject pauseMenu;
    private AudioSource audioSource;
    private bool isPaused = false;
    private Player player;
    private VignetteController vignette;
    private float vChangeRate = 0;

	// Use this for initialization
	void Start () {
        pauseMenu.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindObjectOfType<Player>();
        vignette = GameObject.FindObjectOfType<VignetteController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused == false)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
	}

    /// <summary>
    /// NEED TO PAUSE EVERYTHING IN THE GAME, Rigidbody's etc
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        audioSource.Play();
        vignette.PauseVignetteIntensity();
        player.canTurn = false;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        audioSource.Play();
        vignette.ResumeVignetteIntensity();
        player.canTurn = true;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
