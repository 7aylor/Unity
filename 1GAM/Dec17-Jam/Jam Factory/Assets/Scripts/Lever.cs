using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    public float topY;
    public float botY;

    void OnMouseDrag()
    {
        float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        y = Mathf.Clamp(y, botY, topY);

        transform.position = new Vector3(transform.position.x, y, 0);
    }

    private void OnMouseDown()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
