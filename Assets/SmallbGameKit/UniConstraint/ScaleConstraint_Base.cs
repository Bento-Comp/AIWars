using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniConstraint
{
	[AddComponentMenu("UniConstraint/ScaleConstraint_Base")]
	public abstract class ScaleConstraint_Base : MonoBehaviour
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

			Vector3 targetScale = targetTransform.localScale;

			Vector3 wantedScale = controlledTransform.localScale;

			if(freezeX == false)
				wantedScale.x = targetScale.x;

			if(freezeY == false)
				wantedScale.y = targetScale.y;

			if(freezeZ == false)
				wantedScale.z = targetScale.z;

			controlledTransform.localScale = wantedScale;
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
