using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
	public Sprite up;
	public Sprite down;
	int objects = 0;
	SpriteRenderer sp;

	public Triggerable[] toTrigger;

	void Start ()
	{
		sp = GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		objects++;
		foreach (Triggerable ii in toTrigger)
			ii.Trigger();
	}
	
	void OnTriggerStay2D (Collider2D col)
	{
		if (objects != 0)
		{
			sp.sprite = down;
			foreach (Triggerable ii in toTrigger)
				ii.Hold();
		}
    }

	void OnTriggerExit2D (Collider2D col)
	{
		objects--;
		if (objects == 0)
		{
			sp.sprite = up;
			foreach (Triggerable ii in toTrigger)
				ii.UnTrigger();
		}
    }
}
