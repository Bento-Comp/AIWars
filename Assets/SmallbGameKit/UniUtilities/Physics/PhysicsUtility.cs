using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniUtilities
{
	public class PhysicsUtility
	{
		public static Vector3 ApplyDrag(Vector3 velocity, float drag, float deltaTime,
			float considerVelocityAsNullEpsilon = 0.01f)
		{
			float multiplier = 1.0f - drag * deltaTime;

			if (multiplier < 0.0f)
			{
				multiplier = 0.0f;
			}

			velocity *= multiplier;

			if(velocity.magnitude <= considerVelocityAsNullEpsilon)
				return Vector3.zero;

			return velocity;
		}

		public static Vector2 ApplyDrag(Vector2 velocity, float drag, float deltaTime,
			float considerVelocityAsNullEpsilon = 0.01f)
		{
			float multiplier = 1.0f - drag * deltaTime;

			if (multiplier < 0.0f)
			{
				multiplier = 0.0f;
			}

			velocity *= multiplier;

			if(velocity.magnitude <= considerVelocityAsNullEpsilon)
				return Vector2.zero;

			return velocity;
		}

		public static float ApplyDrag(float velocity, float drag, float deltaTime,
			float considerVelocityAsNullEpsilon = 0.01f)
		{
			float multiplier = 1.0f - drag * deltaTime;

			if (multiplier < 0.0f)
			{
				multiplier = 0.0f;
			}

			velocity *= multiplier;

			if(Mathf.Abs(velocity) <= considerVelocityAsNullEpsilon)
				return 0.0f;

			return velocity;
		}
	}
}
