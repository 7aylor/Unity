using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour {

    [Tooltip("Amount of lumber to change the market by every TimeSinceLastUse")]
    public int marketLumberUse;

    [Tooltip("Number of seconds between market changes")]
    public float timeBetweenMarketLumberUse;

    public float timeBetweenBuildingBuilds;

    [SerializeField]private float timeSinceLastMarketUse;
    [SerializeField]private float timeSinceLastBuilding;
    private Demand demand;
    private StatsManager stats;
    private BuildingManager buildingManager;

    private void Awake()
    {
        demand = GetComponent<Demand>();
        stats = FindObjectOfType<StatsManager>();
        buildingManager = FindObjectOfType<BuildingManager>();
        timeSinceLastMarketUse = timeBetweenMarketLumberUse;
        timeSinceLastBuilding = timeBetweenBuildingBuilds;
    }
	
	// Update is called once per frame
	void Update () {

        timeSinceLastMarketUse -= Time.deltaTime;

        if (timeSinceLastMarketUse <= 0f && GameManager.instance.lumberInMarket > 0)
        {
            GameManager.instance.lumberInMarket -= marketLumberUse;
            stats.UpdateStats(StatsManager.stat.lumberInMarket);
            demand.UpdateDemand();
            timeSinceLastMarketUse = timeBetweenMarketLumberUse;
        }

        timeSinceLastBuilding -= Time.deltaTime;

        if(timeSinceLastBuilding <= 0f && GameManager.instance.lumberInMarket > 200)
        {
            buildingManager.BuildFromMarket();
            timeSinceLastBuilding = timeBetweenBuildingBuilds;
        }
	}
}