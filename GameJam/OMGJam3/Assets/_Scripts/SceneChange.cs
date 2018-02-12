using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour {

    public Image clock;
    public Image picture;
    public Image window;
    public Image skyline;
    public Image skylineNight;
    public Image snowGlobe;
    public Image bonzai;
    public Image cup;
    public Image ledger;

    FadeInNightSky nightSky;

    private void Awake()
    {
        nightSky = skylineNight.GetComponent<FadeInNightSky>();
    }

    // Use this for initialization
    void Start () {
        EarlyGame();
	}
	
    public void EarlyGame()
    {
        clock.enabled = true;
        picture.enabled = true;
        window.enabled = false;
        skyline.enabled = false;
        skylineNight.enabled = false;
        snowGlobe.enabled = false;
        bonzai.enabled = false;
        cup.enabled = false;
        ledger.enabled = false;
    }

    public void MidGame()
    {
        clock.enabled = true;
        picture.enabled = false;
        window.enabled = true;
        skyline.enabled = false;
        skylineNight.enabled = false;
        snowGlobe.enabled = true;
        bonzai.enabled = false;
        cup.enabled = true;
        ledger.enabled = true;
    }

    public void EndGame()
    {
        clock.enabled = false;
        picture.enabled = false;
        window.enabled = false;
        skyline.enabled = true;
        skylineNight.enabled = true;
        nightSky.FadeInAlpha();
        snowGlobe.enabled = true;
        bonzai.enabled = true;
        cup.enabled = true;
        ledger.enabled = true;
    }
}
