using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuAudioController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void InitFadeOut()
    {
        StartCoroutine("FadeOut");
    }

    private IEnumerator FadeOut()
    {
        AudioSource a = GetComponent<AudioSource>();

        for (int i = 0; i < 50; i++)
        {
            a.volume -= 0.02f;
            yield return new WaitForSeconds(0.02f);
        }

        LevelManager.instance.LoadLevel("MainMenu");
    }
}
