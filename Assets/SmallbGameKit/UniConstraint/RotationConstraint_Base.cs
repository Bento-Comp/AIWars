using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniConstraint
{
	[AddComponentMenu("UniConstraint/RotationConstraint_Base")]
	public abstract class RotationConstraint_Base : MonoBehaviour
	{
		public Transform controlledTransform;

		public Transform targetTransform;

		public bool local;

		public float damping = 0.0f;

		public bool freezeX;
		public bool freezeY;
		public bool freezeZ;

		[Header("Editor")]
		public bool editor_updateInEditMode = true;

		protected void UpdateConstraint()
		{
			if(damping <= 0.0f || Application.isPlaying == false)
			{
				if(local)
				{
					if(freezeX || freezeY || freezeZ)
					{
						Vector3 wantedRotation = controlledTransform.localEulerAngles;
						Vector3 targetRotation = targetTransform.localEulerAngles;

						if(freezeX == false)
							wantedRotation.x = targetRotation.x;

						if(freezeY == false)
							wantedRotation.y = targetRotation.y;

						if(freezeZ == false)
							wantedRotation.z = targetRotation.z;

						controlledTransform.localEulerAngles = wantedRotation;
					}
					else
					{
						controlledTransform.localRotation = targetTransform.localRotation;
					}
				}
				else
				{
					if(freezeX || freezeY || freezeZ)
					{
						Vector3 wantedRotation = controlledTransform.eulerAngles;
						Vector3 targetRotation = targetTransform.eulerAngles;

						if(freezeX == false)
							wantedRotation.x = targetRotation.x;

						if(freezeY == false)
							wantedRotation.y = targetRotation.y;

						if(freezeZ == false)
							wantedRotation.z = targetRotation.z;

						controlledTransform.eulerAngles = wantedRotation;
					}
					else
					{
						controlledTransform.rotation = targetTransform.rotation;
					}
				}
			}
			else
			{
				controlledTransform.rotation = ConstraintUtility.Damp(controlledTransform.rotation, targetTransform.rotation, damping, Time.deltaTime);
			}
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
