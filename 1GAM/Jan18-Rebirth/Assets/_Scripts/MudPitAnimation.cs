using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudPitAnimation : MonoBehaviour {

    private Animator animator;
    private float timeSinceLastPop = 0f;
    private float timeBetweenPops;
    private string[] triggerNames = { "PopTop", "PopBot" };

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        GetRandomTimeBetweenPops();
	}
	
	// Update is called once per frame
	void Update () {
        if(timeSinceLastPop >= timeBetweenPops)
        {
            animator.SetTrigger(GetRandomTrigger());
            timeSinceLastPop = 0;
            GetRandomTimeBetweenPops();
        }
        else
        {
            timeSinceLastPop += Time.deltaTime;
        }
	}

    private void GetRandomTimeBetweenPops()
    {
        timeBetweenPops = Random.Range(5f, 10f);
    }

    private string GetRandomTrigger()
    {
        return triggerNames[Random.Range(0, triggerNames.Length)];
    }
}
