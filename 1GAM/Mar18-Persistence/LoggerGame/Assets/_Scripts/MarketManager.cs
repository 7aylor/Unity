using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour {

    public int marketLumberUse;

    public float timeSinceLastUse;
    private Demand demand;
    private StatsManager stats;

    private void Awake()
    {
        demand = GetComponent<Demand>();
        stats = FindObjectOfType<StatsManager>();
    }
	
	// Update is called once per frame
	void Update () {

        timeSinceLastUse -= Time.deltaTime;

        if (timeSinceLastUse <= 0f && GameManager.instance.lumberInMarket > 0)
        {
            GameManager.instance.lumberInMarket -= marketLumberUse;
            stats.UpdateStats(StatsManager.stat.lumberInMarket);
            demand.UpdateDemand();
            timeSinceLastUse = 2;
        }
	}
}