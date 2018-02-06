using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeerMovement : MonoBehaviour {

    public GameObject deer;
    public GameObject grass;
    private int index;
    private List<Neighbor> neighbors = new List<Neighbor>();
    public int NumCropsEaten { get; set; }

    private bool onTop;
    private bool onBot;
    private bool onLeft;
    private bool onRight;

    // Use this for initialization
    void Start () {
        NumCropsEaten = 0;
    }

    public void MoveDeer()
    {
        foreach(Transform cell in transform)
        {
            if(cell.GetComponent<Image>().sprite.name == "Deer")
            {
                index = cell.GetSiblingIndex();
                GetNeighbors();

                int moveIndex = GetUniqueCellIndex(index);

                transform.GetChild(moveIndex).GetComponent<Image>().sprite = deer.GetComponent<Image>().sprite;
                if (moveIndex != index)
                {
                    transform.GetChild(index).GetComponent<Image>().sprite = grass.GetComponent<Image>().sprite;
                }
            }
        }
    }

    /// <summary>
    /// Gets a unique index of a cell that is not in use
    /// </summary>
    /// <returns></returns>
    private int GetUniqueCellIndex(int index)
    {
        int rand;
        int rollCount = 0; //protects against cases where the dear has nowhere to move and crashes the program

        do
        {
            rand = Random.Range(0, neighbors.Count);
            rollCount++;

            if(rollCount >= 5)
            {
                return index;
            }
        }
        //check that the desired move index does not have an obstacle and the fence is not in play
        while (HandType.Obstacles.Contains(neighbors[rand].name) || CheckForFence(neighbors[rand].name));

        CheckForCropsEaten(neighbors[rand].name);


        return neighbors[rand].cellIndex;
    }

    /// <summary>
    /// Checks if a fence is in play and then protects the crops
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private bool CheckForFence(string name)
    {
        //get passivehand and check for fence
        GameObject passiveHand = GameObject.FindGameObjectWithTag("Hand_Passive");
        bool fenceInPlay = false;

        foreach(Transform card in passiveHand.transform)
        {
            string spriteName = card.GetComponent<Image>().sprite.name;
            if(spriteName == "Fence")
            {
                Debug.Log("Fence is in play");
                fenceInPlay = true;
            }
        }

        if(fenceInPlay && HandType.Crops.ContainsKey(name))
        {
            Console.instance.WriteToConsole("Your Fence protected you against the pesky Deer!");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckForCropsEaten(string tileName)
    {
        if (HandType.Crops.ContainsKey(tileName))
        {
            NumCropsEaten++;
        }
    }

    /// <summary>
    /// Gets the neighbor cells of the tile we are on
    /// </summary>
    private void GetNeighbors()
    {
        //clear neighbors
        neighbors.Clear();

        onTop = index % 5 == 0;
        onBot = index % 5 == 4;
        onLeft = index < 5;
        onRight = index > 54;

        //index is middle of grid
        if (!onTop && !onBot && !onLeft && !onRight)
        {
            //left
            neighbors.Add(new Neighbor(index - 5, transform.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name));
            
            //top
            neighbors.Add(new Neighbor(index - 1, transform.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name));

            //bot
            neighbors.Add(new Neighbor(index + 1, transform.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name));

            //right
            neighbors.Add(new Neighbor(index + 5, transform.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name));
        }

        //index is top of the grid
        if (onTop && !onBot && !onLeft && !onRight)
        {
            //left
            neighbors.Add(new Neighbor(index - 5, transform.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name));
            
            //bot
            neighbors.Add(new Neighbor(index + 1, transform.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name));

            //right
            neighbors.Add(new Neighbor(index + 5, transform.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name));
        }

        //index if bot of grid
        if (onBot && !onTop && !onLeft && !onRight)
        {
            //left
            neighbors.Add(new Neighbor(index - 5, transform.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name));

            //top
            neighbors.Add(new Neighbor(index - 1, transform.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name));
                        
            //right
            neighbors.Add(new Neighbor(index + 5, transform.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name));
        }


        //left
        if (onLeft && !onTop && !onBot && !onRight)
        {
            //top
            neighbors.Add(new Neighbor(index - 1, transform.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name));

            //bot
            neighbors.Add(new Neighbor(index + 1, transform.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name));
            
            //right
            neighbors.Add(new Neighbor(index + 5, transform.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name));
        }

        //left top
        if (onLeft && onTop && !onBot && !onRight)
        {
            //bot
            neighbors.Add(new Neighbor(index + 1, transform.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name));

            //right
            neighbors.Add(new Neighbor(index + 5, transform.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name));
        }

        //left bot
        if (onLeft && onBot && !onTop && !onRight)
        {
            //top
            neighbors.Add(new Neighbor(index - 1, transform.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name));

            //right
            neighbors.Add(new Neighbor(index + 5, transform.GetChild(index + 5).gameObject.GetComponent<Image>().sprite.name));
        }

        //right
        if (onRight && !onTop && !onBot && !onLeft)
        {
            //top
            neighbors.Add(new Neighbor(index - 1, transform.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name));

            //bot
            neighbors.Add(new Neighbor(index + 1, transform.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name));
                        
            //left
            neighbors.Add(new Neighbor(index - 5, transform.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name));
        }

        //right top
        if (onRight && onTop && !onBot && !onLeft)
        {
            //bot
            neighbors.Add(new Neighbor(index + 1, transform.GetChild(index + 1).gameObject.GetComponent<Image>().sprite.name));

            //left
            neighbors.Add(new Neighbor(index - 5, transform.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name));
        }

        //right bot
        if (onRight && onBot && !onTop && !onLeft)
        {
            //top
            neighbors.Add(new Neighbor(index - 1, transform.GetChild(index - 1).gameObject.GetComponent<Image>().sprite.name));

            //left
            neighbors.Add(new Neighbor(index - 5, transform.GetChild(index - 5).gameObject.GetComponent<Image>().sprite.name));
        }
    }
}

struct Neighbor
{
    public int cellIndex;
    public string name;

    public Neighbor(int index, string spriteName)
    {
        cellIndex = index;
        name = spriteName;
    }
}
