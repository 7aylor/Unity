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

        //check if the level has been beaten and disable/change to red if it hasn't been beaten
        if(gameObject.name != "Level_01" && PlayerPrefs.GetString(gameObject.name + "_Enabled") == "False")
        {
            button.gameObject.GetComponent<Image>().color = Color.red;
            button.enabled = false;
        }
    }
	
}
