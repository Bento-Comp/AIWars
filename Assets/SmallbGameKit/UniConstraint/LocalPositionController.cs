using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniConstraint
{
	[ExecuteInEditMode()]
	[AddComponentMenu("UniConstraint/LocalPositionController")]
	public class LocalPositionController : MonoBehaviour
	{
		public Transform controlledTransform;

		void LateUpdate()
		{
			#if UNITY_EDITOR
			if(Application.isPlaying == false && controlledTransform == null)
			{
				return;
			}
			#endif

			controlledTransform.localPosition = transform.localPosition;
		}
	}
}
