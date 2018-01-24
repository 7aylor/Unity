using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledCaveman : MonoBehaviour {

    private Animator animator;
    private Caveman_Move moves;
    private Caveman_Throw throws;
    private SpriteRenderer sprite;

    private void Awake()
    {
        moves = GetComponent<Caveman_Move>();
        throws = GetComponent<Caveman_Throw>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        EnableScripts(false);
    }

    public void EnableScripts(bool isEnabled)
    {
        animator.enabled = isEnabled;
        moves.enabled = isEnabled;
        throws.enabled = isEnabled;
        sprite.enabled = isEnabled;
    }
}
