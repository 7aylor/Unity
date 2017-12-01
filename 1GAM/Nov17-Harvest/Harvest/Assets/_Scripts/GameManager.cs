using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public enum difficulty { easy, medium, hard}
    public difficulty Diff { get; set; }
    public int GoldGoal { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(instance);
    }

    // Use this for initialization
    void Start () {
        Diff = difficulty.easy;
	}


}
