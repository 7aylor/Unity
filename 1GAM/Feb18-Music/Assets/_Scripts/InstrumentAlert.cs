using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentAlert : MonoBehaviour
{

    public Color[] colors;
    private Text alertText;

    private void Awake()
    {
        alertText = GetComponent<Text>();
    }

    private void Start()
    {
        alertText.text = "";
    }

    public void Interrupted()
    {
        Debug.Log("Interrupted!");
        alertText.color = colors[1];
        alertText.text = "Clip Interrupted";
        StopAllCoroutines();
        StartCoroutine(WaitToClearText());
    }

    public void Success()
    {
        Debug.Log("Success!");

        alertText.color = colors[0];
        alertText.text = "Clip Complete!";
        //StopAllCoroutines();
        //StartCoroutine(WaitToClearText());
    }

    private IEnumerator WaitToClearText()
    {
        yield return new WaitForSeconds(2);
        alertText.text = "";
    }
}
