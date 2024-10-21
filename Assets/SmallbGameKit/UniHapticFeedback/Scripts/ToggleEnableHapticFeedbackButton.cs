using UnityEngine;
using System.Collections;

using UnityEngine.UI;

namespace UniHapticFeedback
{
	[AddComponentMenu("UniHapticFeedback/ToggleEnableHapticFeedbackButton")]
	public class ToggleEnableHapticFeedbackButton : MonoBehaviour
	{
		Toggle button;

		void Awake()
		{
			button = GetComponent<Toggle>();
			button.onValueChanged.AddListener(OnValueChange); 

			button.isOn = HapticFeedbackManager.UserHapticFeedbackEnable;
		}

		void OnDestroy()
		{
			if(button != null)
				button.onValueChanged.RemoveListener(OnValueChange);
		}

		void OnValueChange(bool value)
		{
			HapticFeedbackManager.UserHapticFeedbackEnable = value;
		}
	}
}