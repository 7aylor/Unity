using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

    public Text lifeLossNotificationText;

    private List<GameObject> lives = new List<GameObject>();
    private NotifiationManager notificationManager;
    private JamAudio jamAudio;


    private void Start()
    {
        notificationManager = FindObjectOfType<NotifiationManager>();
        foreach(Transform life in transform)
        {
            lives.Add(life.gameObject);
        }

        jamAudio = FindObjectOfType<JamAudio>();
    }

    /// <summary>
    /// Destroys a life and removes it from the list of lives
    /// </summary>
    public void LoseLife()
    {
        Destroy(lives[lives.Count - 1]);
        lives.RemoveAt(lives.Count - 1);
        GameManager.instance.DecreaseLives();

        if(GameManager.instance.GetNumLives() <= 0)
        {
            //TODO: Deal with losing the game, Maybe create some kind of fade or prompt to view the stats or try again?
            //Debug.Log("Loading end game");
            //LevelManager.instance.LoadScene("EndMenu");
            notificationManager.UpdateNotificationText("That's it! You're fired!");

        }
        if(GameManager.instance.GetNumLives() == 1)
        {
            Debug.Log("1 Life");
            jamAudio.LastLifeSong();
        }
    }

}