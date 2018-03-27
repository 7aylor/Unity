using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncreaseResource : MonoBehaviour {

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Use this for initialization
    void Start () {
        //gameObject.SetActive(false);
        SetIncreaseResourceText(40);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetIncreaseResourceText(int inceaseAmount)
    {
        text.text = "+" + inceaseAmount.ToString();
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {

        //Look into using Tweens here https://assetstore.unity.com/packages/tools/animation/tween-55983?aid=1011lGnL&utm_source=aff
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            transform.Translate(Vector3.up * 5);
            Color c = text.color;
            c.a -= 26;
            text.color = c;
        }

        transform.position += Vector3.down * 50;

    }

}
