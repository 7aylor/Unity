using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCardHandler : MonoBehaviour {

    public GameObject grass;

	// Use this for initialization
	void Start () {
		
	}
	
    /// <summary>
    /// Handles the special cases for active cards
    /// </summary>
    /// <param name="cardName"></param>
    public void HandleCard(string cardName)
    {
        switch (cardName)
        {
            case "House":
                HandleHouse();
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// Find the house in the playspace and remove it, then return.
    /// </summary>
    private void HandleHouse()
    {
        GameObject playspace = GameObject.FindGameObjectWithTag("Playspace");

        if(playspace != null)
        {
            foreach(Transform obj in playspace.transform)
            {
                Image img = obj.GetComponent<Image>();
                if (img.sprite.name == "House")
                {
                    img.sprite = grass.GetComponent<Image>().sprite;
                    return;
                }
            }
        }
    }

}
