using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Switch sprites and disable collider.
public class OpenDoor : Triggerable
{
	SpriteRenderer sp;
	Collider2D col;

	public Sprite closed;
	public Sprite open;

	void Start ()
	{
		sp = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();
	}

	public override void Trigger()
	{
		sp.sprite = open;
		col.enabled = false;
	}

	public override void UnTrigger()
	{
		sp.sprite = closed;
		col.enabled = true;
	}
}
