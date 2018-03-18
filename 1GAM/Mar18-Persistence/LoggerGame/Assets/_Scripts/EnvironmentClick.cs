using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnvironmentClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.instance.selectedPlayer != null)
        {
            GameManager.instance.selectedPlayer.HandleMovePlayer();
        }
    }
}
