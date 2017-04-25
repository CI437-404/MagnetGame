using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pushes things on the x axis of this object by using evil collider magic.
// Negative speed values to go the other way.
public class Conveyor : Triggerable
{
	bool active = false;
	Rigidbody2D rb;

	public Vector2 direction;
	public float speed = 2.0f;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
	{
		direction = transform.TransformDirection(new Vector2(1f, 0f));
		if (active)
		{
			rb.position -= direction * speed * Time.deltaTime;
			rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
		}
	}

	public override void Trigger()
	{
		active = true;
	}

	public override void UnTrigger()
	{
		active = false;
	}
}
