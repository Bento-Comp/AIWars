using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniUtilities
{
	public class ArithmeticUtility
	{
		public static int FlooredModulo(int number, int modulus)
		{
			return (number % modulus + modulus) % modulus;
		}

		public static float FlooredModulo(float number, float modulus)
		{
			return (number % modulus + modulus) % modulus;
		}
	}
}
