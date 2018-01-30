using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanDialogue : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private bool cursorDialogueIcon = false;
    private DialogueWindow dw;
    private Exclamation exclamation;
    private SoulCounter soulCounter;

    private void Start()
    {
        dw = FindObjectOfType<DialogueWindow>();
        exclamation = GetComponentInChildren<Exclamation>();
        soulCounter = FindObjectOfType<SoulCounter>();
        Debug.Log("Exclamation: " + exclamation);
    }

    public void SetDialogueCursor(bool cursorNeeded)
    {
        if(cursorNeeded == true)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            cursorDialogueIcon = true;
        }
        else
        {
            Cursor.SetCursor(null, hotSpot, CursorMode.Auto);
            cursorDialogueIcon = false;
        }
        
    }

    public void OnMouseDown()
    {
        if(cursorDialogueIcon == true)
        {
            Debug.Log("Called mouse event on shaman");
            SetDialogueCursor(false);
            dw.EnablePanel(true);
            exclamation.Enabled(false);
            //soulCounter.BuildRune();
            GetComponent<Shaman>().Talk(true);
        }
    }
}
