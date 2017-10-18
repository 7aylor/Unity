using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Item item;
    
    public bool itemDocked = false;
    public float maximumWaitTimeToSpawn;
    public float minimumWaitTimeToSpawn;

    private float timeBetweenSpawns;
    private float timeSinceLastSpawn = 0f;
    private Item dockedItem;
    private SpawnerLight light;

    private Color defaultColor;

    // Use this for initialization
    void Start () {
        timeBetweenSpawns = Random.Range(minimumWaitTimeToSpawn, maximumWaitTimeToSpawn);
        light = GetComponentInChildren<SpawnerLight>();

        defaultColor = GetComponent<Renderer>().material.color;
        light.gameObject.GetComponent<Light>().color = defaultColor;
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLastSpawn += Time.deltaTime;
        SpawnItem();
        SetLightColor();
	}

    /// <summary>
    /// Checks if an item is docked and whether we have waited enough to spawn 
    /// another item, then spawns one
    /// </summary>
    private void SpawnItem()
    {
        if (transform.childCount == 1)
        {
            itemDocked = false;
        }

        if (itemDocked == false && timeSinceLastSpawn >= timeBetweenSpawns)
        {
            timeSinceLastSpawn = 0f;
            dockedItem = Instantiate(item);
            dockedItem.name = "item";
            dockedItem.transform.parent = gameObject.transform;
            dockedItem.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
    }

    private void SetLightColor()
    {
        if(itemDocked == false)
        {
            light.SetLightColor(defaultColor);
        }
        else
        {
            light.SetLightColor(dockedItem.GetComponent<Renderer>().material.color);
        }

    }

    /// <summary>
    /// checks if an item is positioned within the capsule collider attached to the spawner
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Item>())
        {
            itemDocked = true;
            timeSinceLastSpawn = 0f;
        }
    }

    /// <summary>
    /// detects if a docked item leaves the capsule collider so the spawner can spawn another item
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Item>() && dockedItem != null && other.gameObject == dockedItem.gameObject)
        {
            if(dockedItem.transform.parent == transform)
            {
                dockedItem.transform.parent = null;
            }
            itemDocked = false;
        }
    }


}
