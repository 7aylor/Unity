using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInNightSky : MonoBehaviour {

    Image nightSkyImage;

    private void Awake()
    {
        nightSkyImage = GetComponent<Image>();
    }

    private void Start()
    {
        //FadeInAlpha();
    }

    public void FadeInAlpha()
    {
        StartCoroutine("LightNightSky");
    }

    private IEnumerator LightNightSky()
    {
        for(int i = 0; i < 100; i++)
        {
            Color c = nightSkyImage.color;
            c.a += 0.01f;
            nightSkyImage.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
