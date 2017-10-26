using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSelect : MonoBehaviour {

    private Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();

        if(gameObject.name != "Level_01" && PlayerPrefs.GetString(gameObject.name + "_Enabled") == "False")
        {
            button.gameObject.GetComponent<Image>().color = Color.red;
            button.enabled = false;
        }

        Debug.Log(PlayerPrefs.HasKey("Level_01_HighScore"));
        Debug.Log(PlayerPrefs.GetInt("Level_01_HighScore"));


        //PlayerPrefs.SetInt("Level_01_HighScore", 100);


    }
	
}
