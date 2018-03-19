using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ActionPanel : MonoBehaviour {

    private Animator animator;
    private bool isUp;
    private GameObject actionsPanel;

    public enum SelectedPlayer { none, planter, lumberjack };
    public SelectedPlayer selectedPlayer;

    public Button[] hireButtons;
    public Button[] planterButtons;
    public Button[] lumberjackButtons;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        actionsPanel = transform.GetChild(0).gameObject;
    }

    // Use this for initialization
    void Start () {
        isUp = false;
        selectedPlayer = SelectedPlayer.none;
    }
	
    public void Animate()
    {
        if(isUp == true)
        {
            animator.SetTrigger("Slide_Down");
            isUp = false;
        }
        else
        {
            animator.SetTrigger("Slide_Up");
            isUp = true;
        }
    }

    public void ActionsButtonClick()
    {
        if(selectedPlayer != SelectedPlayer.planter && selectedPlayer != SelectedPlayer.lumberjack)
        {
            ActivateButtons(SelectedPlayer.none);
        }
        
        Animate();
    }

    public void ActivateButtons(SelectedPlayer player)
    {
        selectedPlayer = player;

        if(selectedPlayer == SelectedPlayer.none)
        {
            EnableDisableButtons(hireButtons, true);
            EnableDisableButtons(planterButtons, false);
            EnableDisableButtons(lumberjackButtons, false);
        }
        else if (selectedPlayer == SelectedPlayer.lumberjack)
        {
            EnableDisableButtons(hireButtons, false);
            EnableDisableButtons(planterButtons, false);
            EnableDisableButtons(lumberjackButtons, true);
        }
        else if (selectedPlayer == SelectedPlayer.planter)
        {
            EnableDisableButtons(hireButtons, false);
            EnableDisableButtons(planterButtons, true);
            EnableDisableButtons(lumberjackButtons, false);
        }
    }

    private void EnableDisableButtons(Button[] selectedButtons, bool isActive)
    {
        foreach (Button button in selectedButtons)
        {button.interactable = isActive;
        }
    }
}
