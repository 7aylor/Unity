using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCompass : MonoBehaviour {

    private GameObject player;
    RectTransform r;

    private void Start()
    {
        r = GetComponent<RectTransform>();
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update () {
        transform.rotation = Quaternion.Euler(0, 0, player.gameObject.transform.localEulerAngles.y);
	}
}
