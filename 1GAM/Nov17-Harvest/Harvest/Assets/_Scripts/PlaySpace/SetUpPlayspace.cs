﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpPlayspace : MonoBehaviour {

    List<GameObject> cells = new List<GameObject>();
    public GameObject house;
    public GameObject lake;
    public GameObject[] obstacles;
    private List<int> indexes = new List<int>();

	// Use this for initialization
	void Start () {

        //gets the cells that are children
		foreach(Transform child in transform)
        {
            cells.Add(child.gameObject);
        }

        //assigns the house and lake sprites to a random cell

        int houseIndex = GetUniqueCellIndex();
        indexes.Add(houseIndex);
        cells[houseIndex].GetComponent<Image>().sprite = house.GetComponent<Image>().sprite;

        int lakeIndex = GetUniqueCellIndex();
        indexes.Add(lakeIndex);
        cells[lakeIndex].GetComponent<Image>().sprite = lake.GetComponent<Image>().sprite;

        //assigns obstacles ***May need to edit with the addition of more levels***

        int numObstacles = Random.Range(1, (LevelManager.instance.GetCurrentSceneIndex() * obstacles.Length));
        Debug.Log(numObstacles);

        for (int i = 0; i < numObstacles; i++)
        {
            int index = GetUniqueCellIndex();
            indexes.Add(index);

            int obstacleIndex = Random.Range(0, obstacles.Length);

            cells[index].GetComponent<Image>().sprite = obstacles[obstacleIndex].GetComponent<Image>().sprite;
        }

    }

    /// <summary>
    /// Gets a unique index of a cell that is not in use
    /// </summary>
    /// <returns></returns>
    private int GetUniqueCellIndex()
    {
        int rand;
        do
        {
            rand = Random.Range(0, cells.Count - 1);
        } while (indexes.Contains(rand));

        return rand;
    }

}