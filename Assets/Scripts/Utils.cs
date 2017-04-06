using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
	// Does not really work. Oh well.
	public static unsafe float Q_rsqrt(float n)
	{
		long i;
		float x2, y;

		x2 = n * 0.5f;
		y = n;
		i = *(long*)&y;
		i = 0x5f3759df - (i >> 1);
		y = *(float*)&i;
		y = y * (1.5f - (x2 * y * y));

		return y;
	}
}