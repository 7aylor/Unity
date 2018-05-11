using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

    public GameObject menu;
    private PauseGame pause;
    private bool isMenuActive;

    private void Awake()
    {
        pause = menu.GetComponent<PauseGame>();
    }

    // Use this for initialization
    void Start () {
        isMenuActive = false;
	}

    public void HandleClick()
    {
        isMenuActive = !isMenuActive;
        pause.Pause(isMenuActive);
        menu.SetActive(isMenuActive);
    }
}