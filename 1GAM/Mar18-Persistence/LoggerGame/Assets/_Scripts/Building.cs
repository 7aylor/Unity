using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Building : MonoBehaviour {

    [Tooltip("Holds each phase of the building being built")]
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    //keeps track of the sprite we are currently using in the array
    private int currentSprite;
    private float timeToNextBuildingStage;
    private int timePerBuildingStage;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        timePerBuildingStage = 3;
        timeToNextBuildingStage = 0f;
        currentSprite = 0;
        spriteRenderer.sprite = sprites[currentSprite];
	}
	
	// Update is called once per frame
	void Update () {
        timeToNextBuildingStage += Time.deltaTime;

        //if currentSprite is the last sprite, remove this script
        if (currentSprite == sprites.Length - 1)
        {
            Destroy(this);
        }
        //if we hit the time per build stage, swap the sprite with the next
        else if (timeToNextBuildingStage >= timePerBuildingStage)
        {
            spriteRenderer.sprite = sprites[++currentSprite];
            timeToNextBuildingStage = 0;
        }

	}
}
