using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniConstraint
{
	[ExecuteInEditMode()]
	[AddComponentMenu("UniConstraint/SquashAndStretch")]
	public class SquashAndStretch : MonoBehaviour
	{
		[Range(0.0f, 2.0f)]
		public float x = 1.0f;

		[Range(0.0f, 2.0f)]
		public float y = 1.0f;

		[Range(0.0f, 2.0f)]
		public float z = 1.0f;

		public float intensityScale = 1.0f;

		public Transform controlledTransform;

		void LateUpdate()
		{
			#if UNITY_EDITOR
			if(Application.isPlaying == false && controlledTransform == null)
			{
				return;
			}
			#endif

			Vector3 squashAndStretch;

			float xEffective = (x - 1.0f) * intensityScale + 1.0f;
			float yEffective = (y - 1.0f) * intensityScale + 1.0f;
			float zEffective = (z - 1.0f) * intensityScale + 1.0f;

			squashAndStretch.x = xEffective * (2.0f - yEffective) * (2.0f - zEffective);
			squashAndStretch.y = yEffective * (2.0f - xEffective) * (2.0f - zEffective);
			squashAndStretch.z = zEffective * (2.0f - xEffective) * (2.0f - yEffective);

			controlledTransform.localScale = squashAndStretch;
		}
	}
}
