using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : Triggerable
{
	public override void Trigger()
	{
		Debug.Log("Triggered a thing");
	}
}
