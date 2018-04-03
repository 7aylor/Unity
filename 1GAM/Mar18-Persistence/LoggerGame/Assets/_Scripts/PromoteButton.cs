using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteButton : MonoBehaviour {

    public enum PromoteType { chop, lumberJump, plant, water, plantJump}

    public PromoteType type;

    private Animator lumberjackAnimator;
    private Animator planterAnimator;

    public void ClickPromote()
    {
        switch (type)
        {
            case PromoteType.chop:
                lumberjackAnimator = GameObject.FindGameObjectWithTag("Lumberjack").GetComponent<Animator>();
                //update speed in gamemanager
                break;
            case PromoteType.lumberJump:
                break;
            case PromoteType.plant:
                break;
            case PromoteType.plantJump:
                break;
            case PromoteType.water:
                break;
        }
    }
	
}
