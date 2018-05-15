using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour {

    public int marketLumberUse;

    public float timeSinceLastUse;
    private Demand demand;

    private void Awake()
    {
        demand = GetComponent<Demand>();
    }
	
	// Update is called once per frame
	void Update () {

        timeSinceLastUse -= Time.deltaTime;

        if (timeSinceLastUse <= 0f && GameManager.instance.lumberInMarket > 0)
        {
            GameManager.instance.lumberInMarket -= marketLumberUse;
            Debug.Log("Market Lumber:" + GameManager.instance.lumberInMarket);
            demand.UpdateDemand();
            timeSinceLastUse = 2;
        }
	}
}