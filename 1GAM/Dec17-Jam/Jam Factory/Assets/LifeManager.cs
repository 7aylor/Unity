using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

    private List<GameObject> lives = new List<GameObject>();

    private void Start()
    {
        foreach(Transform life in transform)
        {
            lives.Add(life.gameObject);
        }
    }

    /// <summary>
    /// Destroys a life and removes it from the list of lives
    /// </summary>
    public void LoseLife()
    {
        if(GameManager.instance.GetNumLives() > 0)
        {
            Destroy(lives[lives.Count - 1]);
            lives.RemoveAt(lives.Count - 1);
        }
        else
        {
            //TODO: Deal with losing the game
        }
          
    }

}
