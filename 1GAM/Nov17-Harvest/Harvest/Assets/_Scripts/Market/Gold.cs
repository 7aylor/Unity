using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour {

    public int startingGold = 100;
    private int gold;
    private Text text;

    private void Start()
    {
        gold = startingGold;
        text = GetComponent<Text>();
    }

    /// <summary>
    /// gets the amount of gold in our bank
    /// </summary>
    /// <returns></returns>
    public int GetGoldAmount()
    {
        return gold;
    }

    /// <summary>
    /// sets the amount of gold in our bank to the new amount
    /// </summary>
    /// <param name="newGoldAmount"></param>
    public void SetGoldAmount(int newGoldAmount)
    {
        gold = newGoldAmount;
        SetGoldText(newGoldAmount);
    }

    /// <summary>
    /// checks if we can afford to purchase a card
    /// </summary>
    /// <param name="purchaseAmount"></param>
    /// <returns></returns>
    public bool canBuy(int purchaseAmount)
    {
        return purchaseAmount <= gold;
    }

    /// <summary>
    /// sets the gold text
    /// </summary>
    /// <param name="goldAmount"></param>
    private void SetGoldText(int goldAmount)
    {
        text.text = goldAmount.ToString();
    }
}
