using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Triggerable
{
	// -1 is back, 1 is forward.
	bool forward = false;

	// Interpolation speed.
	public float interpolationRate = 0.01f;
	float currentPosition = 0f;

	// Points for interpolation.
	public Transform start;
	public Transform end;

	void Start ()
	{
		
	}

	void FixedUpdate()
	{
		// Update current position marker.
		if (forward && currentPosition < 1)
			currentPosition += interpolationRate;
		//else if (forward) currentPosition = 1;

		if (!forward && currentPosition > 0)
			currentPosition -= interpolationRate;
		//else if (!forward) currentPosition = 0;

		// Interpolate.
		transform.position = Vector2.Lerp(start.position, end.position, currentPosition);
	}

	public override void Trigger()
	{
		forward = true;
	}

	public override void UnTrigger()
	{
		forward = false;
	}
}
