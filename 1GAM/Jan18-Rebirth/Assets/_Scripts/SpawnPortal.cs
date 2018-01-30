using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour {

    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	private void EnablePortal()
    {
        sprite.enabled = true;
        StartCoroutine("LerpColorAlpha");
    }

    private IEnumerator LerpColorAlpha()
    {

        Color spriteColor = sprite.color;
        for (int i = 1; i <= 100; i++)
        {
            spriteColor.a = i/100f;
            sprite.color = spriteColor;
            yield return new WaitForEndOfFrame();
        }
    }

}
