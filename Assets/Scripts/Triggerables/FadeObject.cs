using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Makes an object slightly transparent and disables its collision.
// Useful for fading doors and platforms and stuff.

// Does not account for objects which already have a transparent tint.
// Does not account for objects which have more than one collider.
public class FadeObject : Triggerable
{
	SpriteRenderer sp;
	Collider2D col;
	
	void Start ()
	{
		sp = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();
	}

	public override void Trigger()
	{
		// Fade alpha
		Color current = sp.color;
		current.a = 0.4f;
        sp.color = current;

		// Disable collider
		col.enabled = false;
	}

	public override void UnTrigger()
	{
		// Reset alpha
		Color current = sp.color;
		current.a = 1f;
		sp.color = current;

		// Enable collider
		col.enabled = true;
	}
}
