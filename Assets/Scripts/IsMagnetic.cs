using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// If this script exists on a GameObject, it will receive magnetic force, and potentially exert it.
public class IsMagnetic : MonoBehaviour
{
	// A public static list to store all magnetic items currently in the scene.
	public static List<IsMagnetic> magnets = new List<IsMagnetic>();

	// Let's just store a ton of values and look them up.
	static Dictionary<float, float> inverseSqrts = new Dictionary<float, float>();

	// How many frames should we spread magnet updates across?
	static int updateFreq = 5;
	// Based on the update frequency, we need to modify the force to achieve the same strength as updating every frame.
	static float cyclePowerModifier = Mathf.Pow(1.3f, updateFreq - 1f);

	// Properties of this specific instance
	// // Overall power of the magnetic force. +Push, -Pull.
	public float power = 0f;
	// // Current cycle this object is on. 
	// // Objects update every updateFreq frames, this is where this object is on the cycle.
	// // Random cycle position is decided in Start.
	int cycle = 1;
	

	void Start ()
	{
		// Add self to list of magnets immediately upon creation.
		magnets.Add(this);

		// Setup where we are on the update cycle.
		cycle = Random.Range(1, updateFreq + 1);
	}

	void OnDestroy()
	{
		// Remove self from the list when deleted. Null references = sad.
		magnets.Remove(this);
	}

	void Update ()
	{
		// Cycle. Update this object if it is time.
		if (cycle >= updateFreq)
		{
			UpdateMagnet();
			cycle = 1;
		}
		cycle++;
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
				if (toAffect != null && ii != this)
				{
					// Get the direction by normalizing the position vector between the two objects.
					Vector2 direction = (ii.gameObject.transform.position - gameObject.transform.position).normalized;

					// Get the magnitude, with a bit of memoization fanciness.
					float distance = Vector2.Distance(ii.gameObject.transform.position, gameObject.transform.position);
					distance = Mathf.Round(distance * 10f) / 10f;	
					
					float magnitude = 0f;
					if (!inverseSqrts.TryGetValue(distance, out magnitude))
					{
						inverseSqrts[distance] = magnitude = 1f / (Mathf.Pow(Vector2.Distance(ii.gameObject.transform.position, gameObject.transform.position), 2));
					}
					
					// Without memoization.
					//float magnitude = 1f / (Mathf.Pow(Vector2.Distance(ii.gameObject.transform.position, gameObject.transform.position), 2));

					magnitude *= power * cyclePowerModifier;

					// Create the force vector.
					Vector2 forceVec = magnitude * direction;

					// Apply the force.
					toAffect.AddForce(forceVec);
				}
			}
		}
	}
}
