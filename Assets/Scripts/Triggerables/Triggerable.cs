using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : MonoBehaviour
{
	// This is just a weird way to generalize a component.
	// Different triggerables inherit from this.

	// Called when a button is pressed.
	public virtual void Trigger()
	{
		// Do nothing.
	}

	// Called every frame while a button is held.
	public virtual void Hold()
	{
		// Do nothing.
	}

	// Called when a button is released.
	public virtual void UnTrigger()
	{
		// Do nothing.
	}
}
