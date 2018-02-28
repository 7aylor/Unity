using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentAlert : MonoBehaviour
{

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
        alertText.color = Color.red;
        alertText.text = "Clip Interrupted";
        StopAllCoroutines();
        StartCoroutine(WaitToClearText());
    }

    public void Success()
    {
        Debug.Log("Success!");

        alertText.color = Color.green;
        alertText.text = "Clip Complete!";
        StopAllCoroutines();
        StartCoroutine(WaitToClearText());
    }

    private IEnumerator WaitToClearText()
    {
        yield return new WaitForSeconds(0.5f);
        alertText.text = "";
    }
}
