using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Difficulty", menuName = "Difficulty Setting")]
public class Difficulty : ScriptableObject {

    public enum DifficultyOptions { Easy, Medium, Hard }

    public float maxTime;
    public int numRounds;

    public void SetDifficulty(DifficultyOptions difficulty)
    {
        DiffOptions diff = new DiffOptions(difficulty);
        maxTime = diff.maxTime;
        numRounds = diff.numRounds;
    }

}

struct DiffOptions
{
    public float maxTime;
    public int numRounds;

    public DiffOptions(Difficulty.DifficultyOptions difficulty)
    {
        if(difficulty == Difficulty.DifficultyOptions.Easy)
        {
            maxTime = 3f;
            numRounds = 10;
        }
        else if (difficulty == Difficulty.DifficultyOptions.Medium)
        {
            maxTime = 2f;
            numRounds = 7;
        }
        else
        {
            maxTime = 1.5f;
            numRounds = 5;
        }
    }

}