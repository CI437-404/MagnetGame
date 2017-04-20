using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
	public Sprite up;
	public Sprite down;
	SpriteRenderer sp;

	public Triggerable[] toTrigger;

	void Start ()
	{
		sp = GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		foreach (Triggerable ii in toTrigger)
			ii.Trigger();
	}
	
	void OnTriggerStay2D (Collider2D col)
	{
		sp.sprite = down;
		foreach (Triggerable ii in toTrigger)
			ii.Hold();
    }

	void OnTriggerExit2D (Collider2D col)
	{
		sp.sprite = up;
		foreach (Triggerable ii in toTrigger)
			ii.UnTrigger();
    }
}
