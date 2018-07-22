using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This script handles player movement when a tile is clicked
/// </summary>
public class EnvironmentClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(GameManager.instance.selectedPlayer != null)
        {
            GameManager.instance.selectedPlayer.HandleMovePlayer(transform.position.x, transform.position.y);
        }
    }
}
