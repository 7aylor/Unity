using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ClickAndDragFriend : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    public GameObject[] friends;
    private GameObject selectedFriend;
    private string friendName;
    private bool mouseDown = false;


	// Use this for initialization
	void Start () {
        friendName = GetComponent<Image>().sprite.name;

        Debug.Log(Physics2D.queriesHitTriggers);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (selectedFriend != null && mouseDown == true)
        {
            selectedFriend.transform.position = Input.mousePosition;
        }
    }

    private void AttachFriendToMouse()
    {
        //attach gameobject to mousecursor
        foreach (GameObject friend in friends)
        {
            if (friend.name == friendName)
            {
                //selectedFriend = friend;
                selectedFriend = Instantiate(friend, transform);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDown = true;
        AttachFriendToMouse();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseDown = false;
        selectedFriend.transform.SetParent(transform.parent.parent);
        selectedFriend = null;
    }
}
