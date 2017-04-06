using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// If this script exists on a GameObject, it will receive magnetic force, and potentially exert it.
public class IsMagnetic : MonoBehaviour
{
	// A public static list to store all magnetic items currently in the scene.
	public static List<IsMagnetic> magnets = new List<IsMagnetic>();

	// Properties of this specific instance
	public float power = 0f;		// Overall power of the magnetic force. +Push, -Pull.
	

	void Start ()
	{
		// Add self to list of magnets immediately upon creation.
		magnets.Add(this);
	}

	void OnDestroy()
	{
		// Remove self from the list when deleted. Null references = sad.
		magnets.Remove(this);
	}

	void Update ()
	{
		UpdateMagnet();
	}

	void UpdateMagnet()
	{
		// Only bother updating if the magnetic force isn't 0.
		if (power != 0)
		{
			// Loop through every object.
			foreach (IsMagnetic ii in magnets)
			{
				// Get the RigidBody of the object so we can affect it. If it exists.
				Rigidbody2D toAffect = ii.gameObject.GetComponent<Rigidbody2D>();
				if (toAffect != null)
				{
					Vector2 direction = (ii.gameObject.transform.position - gameObject.transform.position).normalized;
					float magnitude = power / (Mathf.Pow(Vector2.Distance(ii.gameObject.transform.position, gameObject.transform.position), 2));
					//float magnitude = power * Utils.Q_rsqrt(Vector2.Distance(ii.gameObject.transform.position, gameObject.transform.position)) / 10f;

					// Create the force vector.
					Vector2 forceVec = magnitude * direction;

					// Skip this one if something goes horribly wrong. I'll figure out what the hell is happening later.
					if (float.IsNaN(forceVec.x)) continue;
					toAffect.AddForce(forceVec);
				}
			}
		}
	}
}
