using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyEarned : MonoBehaviour {

    Text moneyEarnedText;
    float speed = 1f;
    public static bool successfullyTyped;

	// Use this for initialization
	void Awake () {
        moneyEarnedText = GetComponent<Text>();
	}

    private void Start()
    {
        InitMoneyText();
        StartCoroutine("MoveAndFade");
    }

    /// <summary>
    /// Translates the text upwards and fades out
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveAndFade()
    {
        for(int i = 0; i < 20; i++)
        {
            transform.Translate(Vector2.up * speed);
            Color moneyTextColor = moneyEarnedText.color;
            moneyTextColor.a -= 0.05f;
            moneyEarnedText.color = moneyTextColor;
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Sets the color and text string depending on if the user types successfully
    /// </summary>
    private void InitMoneyText()
    {
        if (successfullyTyped)
        {
            //play sound effect
            moneyEarnedText.text = "+$" + Stock.Price;
            moneyEarnedText.color = Color.green;
        }
        else
        {
            //play sound effect
            moneyEarnedText.text = "-$" + Stock.Price;
            moneyEarnedText.color = Color.red;
        }
    }
}
