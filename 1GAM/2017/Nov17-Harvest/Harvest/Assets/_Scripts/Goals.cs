using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goals : MonoBehaviour {

    public Text difficulty;
    public Text goal;

    // Use this for initialization
    void Awake () {
        difficulty.text = GameManager.instance.Diff.ToString().ToUpper();

        DefineGoals();
        goal.text = "Earn " + GameManager.instance.GoldGoal.ToString() + " Gold by the end of Fall";
    }

    private void DefineGoals()
    {
        if(GameManager.instance.Diff == GameManager.difficulty.easy)
        {
            GameManager.instance.GoldGoal = Random.Range(500, 1000);
            GameManager.instance.DiffIndex = 1;
        }
        if (GameManager.instance.Diff == GameManager.difficulty.medium)
        {
            GameManager.instance.GoldGoal = Random.Range(1000, 1500);
            GameManager.instance.DiffIndex = 2;
        }
        if (GameManager.instance.Diff == GameManager.difficulty.hard)
        {
            GameManager.instance.GoldGoal = Random.Range(1500, 2000);
            GameManager.instance.DiffIndex = 3;
        }
    }
}
