using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harvest : MonoBehaviour {

    private int index;
    private List<GameObject> passiveCards = new List<GameObject>();
    private List<GameObject> neighbors = new List<GameObject>();
    private bool onTop;
    private bool onBot;
    private bool onLeft;
    private bool onRight;
    private string spriteName;

    private void Start()
    {
        index = gameObject.transform.GetSiblingIndex();
        onTop = index % 5 == 0;
        onBot = index % 5 == 4;
        onLeft = index < 5;
        onRight = index > 54;
        spriteName = GetComponent<Image>().sprite.name;

        foreach (Transform obj in GameObject.FindGameObjectWithTag("Hand_Passive").transform)
        {
            passiveCards.Add(obj.gameObject);
        }
    }

    public int CalculateHarvest()
    {
        int goldYeilded = 0;
        string season = GameObject.FindObjectOfType<SeasonText>().GetCurrentSeason();

        if (HandType.Crops.ContainsKey(spriteName))
        {
            string currSeason = "";
            //get the yield from the crop, or just make a static value for all crops?
            goldYeilded = 1;

            //is the tile in season?
            if (HandType.Crops.TryGetValue(spriteName, out currSeason) && season == HandType.Crops[spriteName])
            {
                goldYeilded *= 5;
            }

            //is irrigation in play?
            if (PassiveCardInPlay("Irrigation") == true)
            {
                goldYeilded *= 2;
            }

            //is Fertilizer in play? this needs to be inside the check for water source
            if (PassiveCardInPlay("Fertilizer") == true)
            {
                goldYeilded *= 2;
            }

            //is the tile next to water?


            //is the tile next to the house?


            //is the tile next to both?

        }

        return goldYeilded;
    }

    /// <summary>
    /// searches through the passive cards and checks if a particular card is in play
    /// </summary>
    /// <param name="card">Pass in the name of the card you want to check for</param>
    /// <returns></returns>
    private bool PassiveCardInPlay(string card)
    {
        foreach(GameObject obj in passiveCards)
        {
            if(obj.GetComponent<Image>().sprite.name == card)
            {
                return true;
            }
        }

        return false;
    }

    private void GetNeighbors()
    {
        //index is middle of grid
        if(!onTop && !onBot && !onLeft && !onRight)
        {
            //left top
            neighbors.Add(transform.parent.GetChild(index - 5 - 1).gameObject);
            //left
            neighbors.Add(transform.parent.GetChild(index - 5).gameObject);
            //left bot
            neighbors.Add(transform.parent.GetChild(index - 5 + 1).gameObject);

            //top
            neighbors.Add(transform.parent.GetChild(index - 1).gameObject);

            //bot
            neighbors.Add(transform.parent.GetChild(index + 1).gameObject);

            //right top
            neighbors.Add(transform.parent.GetChild(index + 5 - 1).gameObject);
            //right
            neighbors.Add(transform.parent.GetChild(index + 5).gameObject);
            //right bot
            neighbors.Add(transform.parent.GetChild(index + 5 + 1).gameObject);
        }


        //index is top of the grid
        if (onTop && !onBot && !onLeft && !onRight)
        {
            //left
            neighbors.Add(transform.parent.GetChild(index - 5).gameObject);
            //left bot
            neighbors.Add(transform.parent.GetChild(index - 5 + 1).gameObject);

            //bot
            neighbors.Add(transform.parent.GetChild(index + 1).gameObject);

            //right
            neighbors.Add(transform.parent.GetChild(index + 5).gameObject);
            //right bot
            neighbors.Add(transform.parent.GetChild(index + 5 + 1).gameObject);
        }

        //index if bot of grid
        if(onBot && !onTop && !onLeft && !onRight)
        {
            //left top
            neighbors.Add(transform.parent.GetChild(index - 5 - 1).gameObject);
            //left
            neighbors.Add(transform.parent.GetChild(index - 5).gameObject);

            //top
            neighbors.Add(transform.parent.GetChild(index - 1).gameObject);

            //right top
            neighbors.Add(transform.parent.GetChild(index + 5 - 1).gameObject);
            //right
            neighbors.Add(transform.parent.GetChild(index + 5).gameObject);
        }


        //left
        //left top
        //left bot

        //right
        //right top
        //right bot


    }

}
