using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour {

    private SpriteRenderer sprite;
    private bool canClickPortal = false;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	public void EnablePortal()
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

    private void OnMouseEnter()
    {
        SetDialogueCursor(true);
    }

    private void OnMouseExit()
    {
        SetDialogueCursor(false);
    }

    private void OnMouseDown()
    {
        LevelManager.instance.LoadScene("End");
    }

    public void SetDialogueCursor(bool cursorNeeded)
    {
        if (cursorNeeded == true)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, hotSpot, CursorMode.Auto);
        }

    }

}
