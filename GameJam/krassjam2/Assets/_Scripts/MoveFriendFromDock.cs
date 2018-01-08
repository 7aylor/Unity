using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MoveFriendFromDock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    public GameObject[] friends;
    private GameObject selectedFriend;
    private string friendName;
    private bool mouseDown = false;


	// Use this for initialization
	void Start () {
        friendName = GetComponent<Image>().sprite.name;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveFriendWithMouse();
    }

    private void MoveFriendWithMouse()
    {
        if (selectedFriend != null && mouseDown == true)
        {
            selectedFriend.transform.position = Input.mousePosition;
        }
    }

    /// <summary>
    /// Loop through the list of friends, find the object that has the same name as our 
    /// sprite, and instantiate the new gameobject
    /// </summary>
    private void InstantiateFriend()
    {
        foreach (GameObject friend in friends)
        {
            if (friend.name == friendName)
            {
                selectedFriend = Instantiate(friend, transform);
            }
        }
    }

    /// <summary>
    /// When mouse is down on this object, instantiate friend
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDown = true;
        InstantiateFriend();
    }

    /// <summary>
    /// when mouse button goes up, remove parent from selected friend 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        mouseDown = false;
        selectedFriend.transform.SetParent(transform.parent.parent);
        selectedFriend = null;
        Destroy(gameObject);
    }
}
