using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exclamation : MonoBehaviour {

    private Animator animator;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Enabled(false);
	}

    public void Enabled(bool enabled)
    {
        animator.enabled = enabled;
        sprite.enabled = enabled;
    }
}
