using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The simplest possible movement script. It ignores literally everything because I don't care.
public class WASD : MonoBehaviour
{
	public float speed = 0.1f;

	void Update ()
	{
		if (Input.GetKey(KeyCode.W))
			transform.Translate(0f, speed, 0f);
		if (Input.GetKey(KeyCode.A))
			transform.Translate(-speed, 0f, 0f);
		if (Input.GetKey(KeyCode.S))
			transform.Translate(0f, -speed, 0f);
		if (Input.GetKey(KeyCode.D))
			transform.Translate(speed, 0f, 0f);
	}
}
