using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using MoreMountains.NiceVibrations;

namespace UniHapticFeedback
{
	public enum ForceHapticFeedbackSupportMode
	{
		DontForce,
		ForceSupported,
		ForceNotSupported
	}

	[AddComponentMenu("UniHapticFeedback/HapticFeedbackManager")]
	public class HapticFeedbackManager : MonoBehaviour
	{
		public bool hapticFeedbackEnabled = true;

		public bool debug_logEnabled = true;

		public bool simulateHapticWithVibrationOnAndroid;

		public ForceHapticFeedbackSupportMode editor_forceHapticSupportMode = ForceHapticFeedbackSupportMode.ForceSupported;

		static HapticFeedbackManager instance;

		static string hapticFeedbackUserEnable_savekey = "HapticFeedbackUserEnable";

		public static bool UserHapticFeedbackEnable
		{
			get
			{
				return PlayerPrefs.GetInt(hapticFeedbackUserEnable_savekey, 1) == 1;
			}

			set
			{
				PlayerPrefs.SetInt(hapticFeedbackUserEnable_savekey, value ? 1 : 0);
			}
		}

		public static bool HapticFeedbackEnabled
		{
			get
			{
				if(instance == null)
					return false;

				return instance.isActiveAndEnabled && instance.hapticFeedbackEnabled && UserHapticFeedbackEnable && instance.HapticFeedbackSupported;
			}

			set
			{
				if(instance == null)
					return;

				instance.hapticFeedbackEnabled = value;
			}
		}

		static bool Debug_LogEnabled
		{
			get
			{
				if(instance == null)
					return false;

				return instance.isActiveAndEnabled && instance.debug_logEnabled;
			}
		}

		public bool HapticFeedbackSupported
		{
			get
			{
				#if UNITY_EDITOR
				switch(editor_forceHapticSupportMode)
				{
					case ForceHapticFeedbackSupportMode.ForceSupported:
						return true;

					case ForceHapticFeedbackSupportMode.ForceNotSupported:
						return false;
				}
				#endif

				#if UNITY_ANDROID
				return simulateHapticWithVibrationOnAndroid;
				#else
				return MMVibrationManager.HapticsSupported();
				//return iOSHapticFeedback.Instance != null && iOSHapticFeedback.Instance.IsSupported();
				#endif
			}
		}

		public static HapticFeedbackManager Instance
		{
			get
			{
				return instance;
			}
		}

		public static void TriggerHapticFeedback(EHapticFeedbackType feedbackType)
		{
			if(HapticFeedbackEnabled == false)
				return;

			if(Debug_LogEnabled)
			{
				Debug.Log("TriggerHapticFeedback : " + feedbackType);
			}

			switch(feedbackType)
			{
				case EHapticFeedbackType.SelectionChange:
					{
						MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Selection);
						//iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.SelectionChange);
					}
				break;
					
				case EHapticFeedbackType.Light:
					{
						MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
						//iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
					}
				break;

				case EHapticFeedbackType.Medium:
					{
						MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
						//iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
					}
				break;

				case EHapticFeedbackType.Heavy:
					{
						MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.HeavyImpact);
						//iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
					}
				break;

				case EHapticFeedbackType.Success:
					{
						MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Success);
						//iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Success);
					}
				break;

				case EHapticFeedbackType.Warning:
					{
						MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Warning);
						//iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Warning);
					}
				break;
				case EHapticFeedbackType.Failure:
					{
                        MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Failure);
						//iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Failure);
					}
				break;

				case EHapticFeedbackType.None:
				default:
				{
				}
				break;
			}
		}

		void Awake()
		{
			if(instance == null)
			{
				instance = this;
			}
			else
			{
				Debug.LogWarning("A singleton can only be instantiated once!");
				Destroy(gameObject);
				return;
			}
		}

		void OnDestroy()
		{
			if(instance == this)
			{
				instance = null;
			}
		}
	}
}