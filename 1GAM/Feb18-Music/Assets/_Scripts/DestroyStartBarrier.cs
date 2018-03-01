using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyStartBarrier : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DestoryBarrier());
	}

    private IEnumerator DestoryBarrier()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
