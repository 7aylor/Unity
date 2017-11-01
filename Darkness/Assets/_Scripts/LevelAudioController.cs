using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudioController : MonoBehaviour {

 //   public static LevelAudioController instance;

	//// Use this for initialization
	//void Start () {
	//	if(instance == null)
 //       {
 //           instance = this;
 //       }
 //       else
 //       {
 //           Debug.Log("Destroyed Audio Controller");
 //           Destroy(gameObject);
 //       }

 //       DontDestroyOnLoad(gameObject);
	//}

    private void Update()
    {
        if (!LevelManager.instance.GetCurrentSceneName().Contains("Level_"))
        {
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        AudioSource a = GetComponent<AudioSource>();

        for(int i = 0; i < 50; i++)
        {
            a.volume -= 0.01f;
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(gameObject);
    }
}
