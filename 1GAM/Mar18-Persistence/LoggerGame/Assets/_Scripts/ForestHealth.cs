using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForestHealth : MonoBehaviour {

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Start () {
        text.text = GameManager.instance.forestHealth.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
