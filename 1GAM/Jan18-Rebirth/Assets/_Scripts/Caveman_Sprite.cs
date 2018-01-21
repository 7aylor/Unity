using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caveman_Sprite : MonoBehaviour {

    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}

    private void Update()
    {
        //SGetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }


    public void AdjustSpriteLayer(int newLayer)
    {
        sprite.sortingOrder = newLayer;
    }
}
