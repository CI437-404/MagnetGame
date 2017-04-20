using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour
{
	// Settings
	public float maxSpeed = 7f;
	public float jumpPower = 2f;

	// For deciding what the player can collide with.
	public LayerMask hitLayers;
	// Set in inspector, where grounded check happens.
	public Transform hitSpot;


	// Internal Variables
	Rigidbody2D rb;
	bool grounded = true;
	bool jumping = false;
	bool facingRight = true;
	float move = 0f;
	KeyCode keyRight = KeyCode.D;
	KeyCode keyLeft = KeyCode.A;
	KeyCode keyUp = KeyCode.W;
	KeyCode keyDown = KeyCode.S;
	KeyCode keyJump = KeyCode.Space;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// FixedUpdate is called 60 times per second.
	void FixedUpdate()
	{
		// Check if we're on the ground.
		grounded = Physics2D.OverlapCircle(hitSpot.position, 0.1f, hitLayers);

		// Set velocity.
		rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

		// Check if we need to flip.
		if (move > 0 && !facingRight)
			Flip();
		else if (move < 0 && facingRight)
			Flip();
	}
	
	// Update is called once per frame.
	// Useful for scanning input.
	void Update ()
	{
		// Check inputs for left and right.
		if (Input.GetKey(keyRight) && Input.GetKey(keyLeft))
		{
			move = 0F;
		}
		else if (Input.GetKey(keyRight))
		{
			if (move < 0F) move = 0F;
			if (move < 1F) move += 0.15F;
		}
		else if (Input.GetKey(keyLeft))
		{
			if (move > 0F) move = 0F;
			if (move > -1F) move -= 0.15F;
		}
		else
		{
			if (Mathf.Abs(move) - 0.15F > 0)
				move += (move > 0F ? -0.15F : 0.15F);
			else
				move *= 0.9F;
		}

		// Check input for jump.
		if (Input.GetKeyDown(keyJump) && grounded && !jumping)
		{
			StartCoroutine(Jump(jumpPower));
		}
	}

	// Sprite flipping.
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	// Fancy jump.
	IEnumerator Jump(float pow)
	{
		jumping = true;
		rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
		for (int ii = 0; ii < 3; ii++)
		{
			yield return null;
			if (Input.GetKey(keyJump))
				rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
		}
		jumping = false;
	}
}
