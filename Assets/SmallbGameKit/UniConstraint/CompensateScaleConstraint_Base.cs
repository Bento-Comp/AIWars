using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniConstraint
{
	[AddComponentMenu("UniConstraint/CompensateScaleConstraint_Base")]
	public abstract class CompensateScaleConstraint_Base : MonoBehaviour
	{
		public Transform controlledTransform;

		public Transform transformToCompensate;

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
			if(transformToCompensate == null)
				return;

			if(controlledTransform == null)
				return;

			Vector3 targetScale = transformToCompensate.localScale;

			Vector3 wantedScale = controlledTransform.localScale;

			if(freezeX == false)
				wantedScale.x = CompensateScale(targetScale.x);

			if(freezeY == false)
				wantedScale.y = CompensateScale(targetScale.y);

			if(freezeZ == false)
				wantedScale.z = CompensateScale(targetScale.z);

			controlledTransform.localScale = wantedScale;
		}

		float CompensateScale(float scaleToCompensate)
		{
			if(scaleToCompensate == 0.0f)
				return 1.0f;

			return 1.0f/scaleToCompensate;
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

			if(transformToCompensate == null)
				return;

			UpdateConstraint();
		}
		#endif
	}
}
