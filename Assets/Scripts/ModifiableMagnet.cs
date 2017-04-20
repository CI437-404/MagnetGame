using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiableMagnet : MonoBehaviour
{
	IsMagnetic thisMagnet;
	public float chargeMagnitude = 20f;

	void Start ()
	{
		thisMagnet = GetComponent<IsMagnetic>();
	}
	
	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown(0))
		{
					if (thisMagnet.charge != chargeMagnitude)
						thisMagnet.charge = chargeMagnitude;
					else
						thisMagnet.charge = 0f;
		}
		if (Input.GetMouseButtonDown(1))
		{
				if (thisMagnet.charge != -1 *chargeMagnitude)
					thisMagnet.charge = -1 * chargeMagnitude;
				else
					thisMagnet.charge = 0f;
		}
	}
}
