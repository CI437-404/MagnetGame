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

	// How many frames should we spread magnet updates across? I recommend 5 or 6 being the max.
	static int updateFreq = 5;
	// Based on the update frequency, we need to modify the force to achieve the same strength as updating every frame.
	static float cyclePowerModifier = Mathf.Pow(1.3f, updateFreq - 1f);
	// How far away should we care to update a magnet?
	static float maximumDistance = 1000f;

	// Properties of this specific instance
	// // Overall power of the magnetic force. +Push, -Pull.
	public float charge = 0f;
	// // Current cycle this object is on. 
	// // Objects update every updateFreq frames, this is where this object is on the cycle.
	// // Random cycle position is decided in Start.
	int cycle = 1;
	// // Blacklist. Objects in this list will be ignored by this magnet.
	public IsMagnetic[] blacklist;
	// // RigidBody. We need this reference so we can apply equal and opposite forces to this object as well as other objects.
	Rigidbody2D rb;

	void Start ()
	{
		// Add self to list of magnets immediately upon creation.
		magnets.Add(this);

		// Setup where we are on the update cycle.
		cycle = Random.Range(1, updateFreq + 1);

		// Get RigidBody reference.
		rb = GetComponent<Rigidbody2D>();
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
		if (charge != 0f)
		{
			// Loop through every object.
			foreach (IsMagnetic ii in magnets)
			{
				// Check Blacklist
				bool skip = false;
				foreach (IsMagnetic jj in blacklist)
				{
					if (jj == ii) skip = true;
				}
				if (skip)
				{
					Debug.Log("Skipping");
					continue;
				}
				

				// Get the RigidBody of the object so we can affect it. If it exists.
				Rigidbody2D toAffect = ii.gameObject.GetComponent<Rigidbody2D>();
				if (toAffect != null && ii != this)
				{
					// Get the magnitude, with a bit of memoization fanciness.
					float distance = Vector2.Distance(ii.gameObject.transform.position, gameObject.transform.position);
					if (distance > maximumDistance) continue;
					distance = Mathf.Round(distance * 10f) / 10f;	
					
					float magnitude = 0f;
					if (!inverseSqrts.TryGetValue(distance, out magnitude))
					{
						inverseSqrts[distance] = magnitude = 1f / (Mathf.Pow(Vector2.Distance(ii.gameObject.transform.position, gameObject.transform.position), 2));
						//Debug.Log("New Distance has been added to memo dict: " + distance);
					}
					float selfMagnitude = magnitude;

					if (charge * ii.charge < 0)
					{
						magnitude *= -1f;
						selfMagnitude *= 1f;
					}
					
					
					// Version without memoization.
					//float magnitude = 1f / (Mathf.Pow(Vector2.Distance(ii.gameObject.transform.position, gameObject.transform.position), 2));

					// Multiply by the power of this magnet, and the modifier based on the cycle count.
					magnitude *= Mathf.Abs(charge) * cyclePowerModifier;
					selfMagnitude *= Mathf.Abs(charge) * cyclePowerModifier;

					// Get the direction by normalizing the position vector between the two objects.
					Vector3 direction = (ii.gameObject.transform.position - gameObject.transform.position).normalized;

					// Create the force vector.
					Vector2 forceVec = magnitude * direction;

					// Apply the force.
					toAffect.AddForce(forceVec);
					Debug.DrawLine(toAffect.transform.position, toAffect.transform.position + direction, Color.cyan, 0.1f);

					// Now we need to deal with self forces from this object.
					if (rb != null)
					{
						rb.AddForce(-1f * selfMagnitude * direction);
						Debug.DrawLine(transform.position, transform.position - direction, Color.yellow, 0.1f);
					}
				}
			}
		}
	}
}
