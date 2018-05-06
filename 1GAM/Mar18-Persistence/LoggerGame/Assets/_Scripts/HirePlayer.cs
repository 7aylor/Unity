using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HirePlayer : MonoBehaviour {

    public GameObject lumberjack;
    public GameObject planter;

    public enum HireType { lumberjack, planter}
    public HireType typeOfHire;

    [SerializeField]
    private ActionPanel actionPanel;

    private void Awake()
    {
        actionPanel = GetComponentInParent<ActionPanel>();
    }


    //int count = 0;

    //private void Update()
    //{
    //    if(count < 1000)
    //    {
    //        Hire();
    //        count++;
    //    }

    //}

    /// <summary>
    /// When you click the button to hire player
    /// </summary>
    public void Hire()
    {
        //get coordinates
        IntVector2 instantiateCoords = GetCoordinates();

        //hire lumberjack
        if(typeOfHire == HireType.lumberjack && GameManager.instance.lumberjackHired == false)
        {
            GameObject spawn = Instantiate(lumberjack, new Vector3((float)instantiateCoords.x, (float)instantiateCoords.y - 0.2f, 0), Quaternion.identity);
            GameManager.instance.lumberjackHired = true;
            actionPanel.EnableDisableSingleButton(actionPanel.hireButtons[0].gameObject, false);

        }
        //hire planter
        else if(typeOfHire == HireType.planter && GameManager.instance.planterHired == false)
        {
            Instantiate(planter, new Vector3((float)instantiateCoords.x, (float)instantiateCoords.y - 0.2f, 0), Quaternion.identity);
            GameManager.instance.planterHired = true;
            actionPanel.EnableDisableSingleButton(actionPanel.hireButtons[1].gameObject, false);
        }
    }

    /// <summary>
    /// returns integer coordinates that do not fall on a river or obstacle tile
    /// </summary>
    /// <returns></returns>
    private IntVector2 GetCoordinates()
    {
        IntVector2 coord = new IntVector2();
        int mapX;
        int mapY;

        //get random x and y values that are valid spawn points
        do
        {
            //random gamespace coordinates
            coord.x = Random.Range(-GameManager.instance.sizeX / 2, GameManager.instance.sizeX / 2 + 1);
            coord.y = Random.Range(-(GameManager.instance.sizeY / 2) + 1, (GameManager.instance.sizeY / 2) + 2);

            //convert from gamespace to array coords
            mapX = coord.x + GameManager.instance.sizeX / 2;
            mapY = coord.y + GameManager.instance.sizeY / 2 - 1;
        }
        while (GameManager.instance.map[mapX, mapY] != 0 && //0 is grass, 1 is trees
               GameManager.instance.map[mapX, mapY] != 1);

        return coord;
    }

}

//vector2 that is int values
struct IntVector2
{
    public int x, y;

    public IntVector2(int newX, int newY)
    {
        x = newX;
        y = newY;
    }
}
