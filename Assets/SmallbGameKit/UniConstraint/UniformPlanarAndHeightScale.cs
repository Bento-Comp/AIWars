using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniConstraint
{
	[ExecuteInEditMode()]
	[AddComponentMenu("UniConstraint/UniformPlanarAndHeightScale")]
	public class UniformPlanarAndHeightScale : MonoBehaviour
	{
		public float planarScale = 1.0f;

		public float heightScale = 1.0f;

		public Transform controlledTransform;

		void LateUpdate()
		{
			#if UNITY_EDITOR
			if(Application.isPlaying == false && controlledTransform == null)
			{
				return;
			}
			#endif

			controlledTransform.localScale = new Vector3(planarScale, heightScale, planarScale);
		}
	}
}
