using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniConstraint
{
	[ExecuteInEditMode()]
	[AddComponentMenu("UniConstraint/UniformScale")]
	public class UniformScale : MonoBehaviour
	{
		public float scale = 1.0f;

		public Transform controlledTransform;

		void LateUpdate()
		{
			#if UNITY_EDITOR
			if(Application.isPlaying == false && controlledTransform == null)
			{
				return;
			}
			#endif

			controlledTransform.localScale = scale * Vector3.one;
		}
	}
}
