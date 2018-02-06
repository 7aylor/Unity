using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonSymbol : MonoBehaviour {

    public GameObject[] symbols;
    private int index;
    private Image symbolImage;

	// Use this for initialization
	void Start () {
        index = 0;
        symbolImage = GetComponent<Image>();
	}

    public void UpdateSeasonSymbol()
    {
        index++;

        if(index >= symbols.Length)
        {
            index = 0;
            
        }
        Debug.Log("Changing symbol");
        symbolImage.sprite = symbols[index].GetComponent<Image>().sprite;
    }

}
