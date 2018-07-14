using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HirePlayer : MonoBehaviour {

    public GameObject lumberjack;
    public GameObject planter;

    public enum HireType { lumberjack, planter}
    public HireType typeOfHire; //chosen in the editor

    private UIActions uIActions;

    private void Awake()
    {
        uIActions = FindObjectOfType<UIActions>();
    }

    /// <summary>
    /// When you click the button to hire player
    /// </summary>
    public void Hire()
    {
        Debug.Log("Hired!");
        //get coordinates
        Vector2Int instantiateCoords = GetCoordinates();

        //if hire lumberjack is clicked and there is no lumberjack in the game, hire lumberjack
        if (typeOfHire == HireType.lumberjack && GameManager.instance.lumberjackHired == false)
        {
            GameObject spawn = Instantiate(lumberjack, new Vector3((float)instantiateCoords.x, (float)instantiateCoords.y - 0.2f, 0), Quaternion.identity);
            GameManager.instance.lumberjackHired = true;
        }
        //if hire planter is clicked and there is no planter in the game, hire planter
        else if (typeOfHire == HireType.planter && GameManager.instance.planterHired == false)
        {
            Instantiate(planter, new Vector3((float)instantiateCoords.x, (float)instantiateCoords.y - 0.2f, 0), Quaternion.identity);
            GameManager.instance.planterHired = true;
        }

        //disable this Hire button
        gameObject.SetActive(false);
    }

    /// <summary>
    /// returns integer coordinates that do not fall on a river or obstacle tile
    /// </summary>
    /// <returns></returns>
    private Vector2Int GetCoordinates()
    {
        Vector2Int coord = new Vector2Int();
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
