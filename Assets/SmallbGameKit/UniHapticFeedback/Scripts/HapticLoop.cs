using UnityEngine;
using System.Collections;

namespace UniHapticFeedback
{
	[AddComponentMenu("UniHapticFeedback/HapticLoop")]
	public class HapticLoop : MonoBehaviour
	{
		public EHapticFeedbackType feedbackType = EHapticFeedbackType.SelectionChange;

		public float period = 0.1f;

		float elapsedTime;

		void Update()
		{
			UpateLoop();
		}

		void UpateLoop()
		{
			elapsedTime += Time.deltaTime;

			if(elapsedTime >= period)
			{
				elapsedTime = 0.0f;
				TriggerHapticFeedback();
			}
		}

		void TriggerHapticFeedback()
		{
			HapticFeedbackManager.TriggerHapticFeedback(feedbackType);
		}
	}
}