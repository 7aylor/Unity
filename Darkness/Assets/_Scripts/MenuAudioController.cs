using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioController : MonoBehaviour {

    public static MenuAudioController instance;

	// Use this for initialization
	void Start () {
		if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
	}

    private void Update()
    {
        //if we are in a level instead of menu, fade the music
        if (LevelManager.instance.GetCurrentSceneName().Contains("Level_"))
        {
            StartCoroutine("FadeOut");
        }
    }

    /// <summary>
    /// coroutine to fade the menu music out
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        AudioSource a = GetComponent<AudioSource>();

        while(a.volume > 0)
        {
            //if we are back in a menu, reinstantiate a menu audio controller and break from the fade
            if (!LevelManager.instance.GetCurrentSceneName().Contains("Level_"))
            {
                Instantiate(this);
                break;
            }
            a.volume -= 0.01f;
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(gameObject);
    }
}
