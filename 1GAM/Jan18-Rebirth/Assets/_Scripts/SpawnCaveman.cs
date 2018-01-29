using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCaveman : MonoBehaviour {

    private EnabledCaveman caveman;
    private Shaman shaman;
    private DialogueWindow dw;
    private bool firstLaunch = true;

    private void Awake()
    {
        caveman = GameObject.FindObjectOfType<EnabledCaveman>();
        shaman = GameObject.FindObjectOfType<Shaman>();
        dw = GameObject.FindObjectOfType<DialogueWindow>();
    }

    private void Start()
    {
        shaman.Summon(true);
    }

    public void EnableCaveman()
    {
        caveman.transform.position = new Vector3(-8, 1.3f, 0);
        caveman.EnableScripts(true);
        shaman.Summon(false);
    }

    public void ShowDialogueWindow()
    {
        dw.EnablePanel(true);
    }

}
