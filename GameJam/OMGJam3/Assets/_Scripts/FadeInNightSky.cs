using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInNightSky : MonoBehaviour {

    Image nightSkyImage;
    Color c;

    private void Awake()
    {
        nightSkyImage = GetComponent<Image>();
        c = nightSkyImage.color;
    }

    private void Start()
    {
        //FadeInAlpha();
    }

    public void FadeInAlpha()
    {
        c.a = 0;
        nightSkyImage.color = c;
        StartCoroutine("LightNightSky");
    }

    private IEnumerator LightNightSky()
    {
        for (int i = 0; i < 100; i++)
        {
            c.a += 0.01f;
            nightSkyImage.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
