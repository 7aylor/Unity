using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanDialogue : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public void SetDialogueCursor(bool cursorNeeded)
    {
        if(cursorNeeded == true)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, hotSpot, CursorMode.Auto);
        }
        
    }
}
