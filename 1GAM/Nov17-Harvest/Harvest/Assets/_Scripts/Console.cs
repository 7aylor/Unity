using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour {

    private Text text;

    //singleton
    public static Console instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(instance);
    }

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
	}

    public void ClearConsole()
    {
        text.text = "";
    }

    public void WriteToConsole(string consoleText)
    {
        text.text = consoleText;
    }
}
