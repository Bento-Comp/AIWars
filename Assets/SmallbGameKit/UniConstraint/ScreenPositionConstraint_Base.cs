using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniConstraint
{
	[AddComponentMenu("UniConstraint/ScreenPositionConstraint_Base")]
	public abstract class ScreenPositionConstraint_Base : MonoBehaviour
	{
		public Transform controlledTransform;

		public Transform targetTransform;

		public Camera constraintCamera;

		protected void UpdateConstraint()
		{
			float controlledDepth = constraintCamera.WorldToViewportPoint(controlledTransform.position).z;

			Vector3 projectedTargetScreen = constraintCamera.WorldToViewportPoint(targetTransform.position);
			projectedTargetScreen.z = controlledDepth;

			Vector3 targetProjectedWorld = constraintCamera.ViewportToWorldPoint(projectedTargetScreen);

			controlledTransform.position = targetProjectedWorld;
		}

		#if UNITY_EDITOR
		protected void Editor_Update()
		{
			if(Application.isPlaying)
				return;

			if(controlledTransform == null)
				return;

			if(targetTransform == null)
				return;

			if(constraintCamera == null)
				return;

			UpdateConstraint();
		}
#endif
	}
}
