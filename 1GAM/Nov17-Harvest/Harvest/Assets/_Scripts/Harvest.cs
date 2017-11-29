using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harvest : MonoBehaviour {

    private int index;
    private List<GameObject> passiveCards = new List<GameObject>();
    private List<string> neighbors = new List<string>();
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

        foreach (Transform obj in GameObject.FindGameObjectWithTag("Hand_Passive").transform)
        {
            passiveCards.Add(obj.gameObject);
        }

    }

    /// <summary>
    /// Calculates the amount earned from a given tile
    /// </summary>
    /// <returns></returns>
    public int CalculateHarvest()
    {
        int goldYeilded = 0;
        string season = GameObject.FindObjectOfType<SeasonText>().GetCurrentSeason();
        spriteName = GetComponent<Image>().sprite.name;
        GetNeighbors();

        if (HandType.Crops.ContainsKey(spriteName))
        {
            string currSeason = "";
            string logger = "";
            //get the yield from the crop, or just make a static value for all crops?
            goldYeilded = 1;

            //is the tile in season?
            if (HandType.Crops.TryGetValue(spriteName, out currSeason) && season == HandType.Crops[spriteName])
            {
                goldYeilded *= 5;
                logger += " in season, ";
            }

            //is Fertilizer in play? this needs to be inside the check for water source
            if (PassiveCardInPlay("Fertilizer") == true)
            {
                goldYeilded *= 2;
                logger += " fertilzer, ";
            }

            //is the tile next to water?
            if(neighbors.Contains("Well") || neighbors.Contains("Lake"))
            {
                goldYeilded *= 3;
                logger += " near water, ";

                //is irrigation in play?
                if (PassiveCardInPlay("Irrigation") == true)
                {
                    goldYeilded *= 2;
                    logger += " irrigated, ";
                }
            }

            //is the tile next to the house?
            if (neighbors.Contains("House"))
            {
                goldYeilded *= 2;
                logger += " near house, ";
            }

            //is the tile next tile a crop of the same type
            if (neighbors.Contains(spriteName))
            {
                goldYeilded *= 2;
                logger += " near " + spriteName;
            }
            Debug.Log(spriteName + ": " + logger + ", " + goldYeilded.ToString());
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

    /// <summary>
    /// Gets the neighbor cells of the tile we are on
    /// </summary>
    private void GetNeighbors()
    {
        //clear neighbors
        neighbors.Clear();

        //index is middle of grid
        if(!onTop && !onBot && !onLeft && !onRight)
        {
            //left top
            neighbors.Add(transform.parent.GetChild(index - 5 - 1).gameObject.GetComponent<Image>().sprite.name);
            //left
            neighbors.Add(transform.parent.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name);
            //left bot
            neighbors.Add(transform.parent.GetChild(index - 5 + 1).gameObject.GetComponent<Image>().sprite.name);

            //top
            neighbors.Add(transform.parent.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name);

            //bot
            neighbors.Add(transform.parent.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name);

            //right top
            neighbors.Add(transform.parent.GetChild(index + 5 - 1).gameObject.GetComponent<Image>().sprite.name);
            //right
            neighbors.Add(transform.parent.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name);
            //right bot
            neighbors.Add(transform.parent.GetChild(index + 5 + 1).gameObject.GetComponent<Image>().sprite.name);
        }


        //index is top of the grid
        if (onTop && !onBot && !onLeft && !onRight)
        {
            //left
            neighbors.Add(transform.parent.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name);
            //left bot
            neighbors.Add(transform.parent.GetChild(index - 5 + 1).gameObject.GetComponent<Image>().sprite.name);

            //bot
            neighbors.Add(transform.parent.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name);

            //right
            neighbors.Add(transform.parent.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name);
            //right bot
            neighbors.Add(transform.parent.GetChild(index + 5 + 1).gameObject.GetComponent<Image>().sprite.name);
        }

        //index if bot of grid
        if(onBot && !onTop && !onLeft && !onRight)
        {
            //left top
            neighbors.Add(transform.parent.GetChild(index - 5 - 1).gameObject.GetComponent<Image>().sprite.name);
            //left
            neighbors.Add(transform.parent.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name);

            //top
            neighbors.Add(transform.parent.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name);

            //right top
            neighbors.Add(transform.parent.GetChild(index + 5 - 1).gameObject.GetComponent<Image>().sprite.name);
            //right
            neighbors.Add(transform.parent.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name);
        }


        //left
        if(onLeft && !onTop && !onBot && !onRight)
        {
            //top
            neighbors.Add(transform.parent.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name);

            //bot
            neighbors.Add(transform.parent.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name);

            //right top
            neighbors.Add(transform.parent.GetChild(index + 5 - 1).gameObject.GetComponent<Image>().sprite.name);
            //right
            neighbors.Add(transform.parent.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name);
            //right bot
            neighbors.Add(transform.parent.GetChild(index + 5 + 1).gameObject.GetComponent<Image>().sprite.name);
        }

        //left top
        if(onLeft && onTop && !onBot && !onRight)
        {
            //bot
            neighbors.Add(transform.parent.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name);

            //right
            neighbors.Add(transform.parent.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name);
            //right bot
            neighbors.Add(transform.parent.GetChild(index + 5 + 1).gameObject.GetComponent<Image>().sprite.name);
        }

        //left bot
        if(onLeft && onBot && !onTop && !onRight)
        {
            //top
            neighbors.Add(transform.parent.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name);

            //right top
            neighbors.Add(transform.parent.GetChild(index + 5 - 1).gameObject.GetComponent<Image>().sprite.name);
            //right
            neighbors.Add(transform.parent.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name);
        }

        //right
        if(onRight && !onTop && !onBot && !onLeft)
        {
            //top
            neighbors.Add(transform.parent.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name);

            //bot
            neighbors.Add(transform.parent.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name);

            //left top
            neighbors.Add(transform.parent.GetChild(index - 5 - 1).gameObject.GetComponent<Image>().sprite.name);
            //left
            neighbors.Add(transform.parent.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name);
            //left bot
            neighbors.Add(transform.parent.GetChild(index - 5 + 1).gameObject.GetComponent<Image>().sprite.name);
        }

        //right top
        if (onRight && onTop && !onBot && !onLeft)
        {
            //bot
            neighbors.Add(transform.parent.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name);

            //left
            neighbors.Add(transform.parent.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name);
            //left bot
            neighbors.Add(transform.parent.GetChild(index - 5 + 1).gameObject.GetComponent<Image>().sprite.name);
        }

        //right bot
        if (onRight && onBot && !onTop && !onLeft)
        {
            //top
            neighbors.Add(transform.parent.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name);

            //left top
            neighbors.Add(transform.parent.GetChild(index - 5 - 1).gameObject.GetComponent<Image>().sprite.name);
            //left
            neighbors.Add(transform.parent.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name);
        }
    }

    /// <summary>
    /// returns the neighbors list of a given cell
    /// </summary>
    /// <returns></returns>
    public List<string> FindNeighbors()
    {
        GetNeighbors();
        return neighbors;
    }

}
