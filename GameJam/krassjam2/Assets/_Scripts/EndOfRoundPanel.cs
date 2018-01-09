using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class EndOfRoundPanel : MonoBehaviour {

    private Text titleText;
    private AudioSource audioSource;
    public AudioClip[] clips;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        titleText = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
	}

    public void SetTitleText(bool gameOver, int roundNum)
    {
        if(gameOver == true)
        {
            audioSource.clip = clips[0];
            audioSource.Play();
            titleText.text = "You have failed at ROUND " + roundNum;
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            audioSource.clip = clips[1];
            audioSource.Play();
            titleText.text = "Congratulations on completing ROUND " + roundNum;
        }
    }
}
