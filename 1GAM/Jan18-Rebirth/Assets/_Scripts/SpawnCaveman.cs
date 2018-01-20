using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCaveman : MonoBehaviour {

    private EnabledCaveman caveman;
    private Shaman shaman;

    private void Awake()
    {
        caveman = GameObject.FindObjectOfType<EnabledCaveman>();
        shaman = GameObject.FindObjectOfType<Shaman>();
        shaman.Summon(true);
    }

    public void EnableCaveman()
    {
        caveman.EnableScripts(true);
        shaman.Summon(false);
    }

}
