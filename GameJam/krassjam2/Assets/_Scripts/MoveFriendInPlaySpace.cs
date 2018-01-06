using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveFriendInPlaySpace : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool mouseDown = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveFriendWithMouse();
    }

    private void MoveFriendWithMouse()
    {
        if (mouseDown == true)
        {
            gameObject.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseDown = false;
    }
}
