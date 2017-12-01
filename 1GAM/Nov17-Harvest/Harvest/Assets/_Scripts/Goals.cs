using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goals : MonoBehaviour {

    public Text difficulty;
    public Text goal;

    // Use this for initialization
    void Start () {
        difficulty.text = GameManager.instance.Diff.ToString().ToUpper();

        DefineGoals();
        goal.text = "Earn " + GameManager.instance.GoldGoal.ToString() + " Gold by the end of Fall";
    }

    private void DefineGoals()
    {
        if(GameManager.instance.Diff == GameManager.difficulty.easy)
        {
            GameManager.instance.GoldGoal = Random.Range(500, 1000);
        }
        if (GameManager.instance.Diff == GameManager.difficulty.medium)
        {
            GameManager.instance.GoldGoal = Random.Range(1000, 1500);
        }
        if (GameManager.instance.Diff == GameManager.difficulty.hard)
        {
            GameManager.instance.GoldGoal = Random.Range(1500, 2000);
        }
    }
}
