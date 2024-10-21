using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniConstraint
{
	[AddComponentMenu("UniConstraint/PositionConstraint_Base")]
	public abstract class PositionConstraint_Base : MonoBehaviour
	{
		public Transform controlledTransform;

		public Transform targetTransform;

		public bool freezeX;
		public bool freezeY;
		public bool freezeZ;

		[Header("Editor")]
		public bool editor_updateInEditMode = true;

		void OnEnable()
		{
			UpdateConstraint();
		}

		protected void UpdateConstraint()
		{
			if(targetTransform == null)
				return;

			if(controlledTransform == null)
				return;

			Vector3 targetPosition = targetTransform.position;

			Vector3 wantedPosition = controlledTransform.position;

			if(freezeX == false)
				wantedPosition.x = targetPosition.x;

			if(freezeY == false)
				wantedPosition.y = targetPosition.y;

			if(freezeZ == false)
				wantedPosition.z = targetPosition.z;

			controlledTransform.position = wantedPosition;
		}

		#if UNITY_EDITOR
		protected void Editor_Update()
		{
			if(Application.isPlaying)
				return;

			if(editor_updateInEditMode == false)
				return;

			if(controlledTransform == null)
				return;

			if(targetTransform == null)
				return;

			UpdateConstraint();
		}
		#endif
	}
}
