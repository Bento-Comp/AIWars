using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace UniTime
{
	[AddComponentMenu("UniTime/Time/UniTime")]
	public class UniTime : MonoBehaviour
	{	
		public static string mc_oDefaultPauseName = "";
		
		public static Action<bool> onPause; 
		
		public float maxDeltaTime = 0.1f;
		
		float deltaTime; 
		
		float lastUpdateRealTime;
		
		List<string> pauseNamesStack = new List<string>();
		
		bool paused;
		
		static UniTime ms_oInstance;
		
		static bool ms_bApplicationEnd;
		
		public static float DeltaTime
		{
			get
			{
				if(Time.deltaTime == 0.0f)
				{
					TryCreateInstanceIfNeeded();
					if(ms_oInstance == null)
					{
						return Time.deltaTime;
					}
					
					return ms_oInstance.deltaTime;
				}
				else
				{
					return Time.deltaTime;
				}
			}
		}
		
		public static bool Paused
		{
			get
			{
				if(Application.isPlaying == false)
					return false;

				TryCreateInstanceIfNeeded();
				if(ms_oInstance == null)
				{
					return false;
				}
				
				return ms_oInstance.paused;
			}
			
			set
			{	
				Pause(mc_oDefaultPauseName, value);
			}
		}
		
		static void TryCreateInstanceIfNeeded()
		{
			if(ms_bApplicationEnd)
			{
				return;
			}
			
			if(ms_oInstance != null)
			{
				return;
			}
			
			////Debug.Log("Create");
			UniEditor.ComponentBuilderUtility.BuildComponent<UniTime>();
		}
		
		void Awake()
		{
			if(ms_oInstance == null)
			{
				ms_oInstance = this;
			}
			else
			{
				Debug.LogWarning("A singleton can only be instantiated once!");
				Destroy(gameObject);
			}
		}
		
		void OnDestroy()
		{
			Time.timeScale = 1.0f;
		}
		
		void OnApplicationQuit()
		{
			ms_bApplicationEnd = true;
		}
		
		void Update()
		{
			if(paused)
			{
				Time.timeScale = 0.0f;
				deltaTime = Time.realtimeSinceStartup - lastUpdateRealTime;
			}
			else
			{
				deltaTime = Time.deltaTime;
			}
			if(deltaTime > maxDeltaTime)
			{
				deltaTime = maxDeltaTime;
			}
			lastUpdateRealTime = Time.realtimeSinceStartup;
		}
		
		static public bool ContainsTheCurrentPauseName(List<string> a_rPauseNames)
		{
			TryCreateInstanceIfNeeded();
			if(ms_oInstance == null)
			{
				return false;
			}
			
			return ms_oInstance._ContainsTheCurrentPauseName(a_rPauseNames);
		}
		
		static public bool IsTheCurrentPauseName(string a_rPauseName)
		{
			TryCreateInstanceIfNeeded();
			if(ms_oInstance == null)
			{
				return false;
			}
			
			return ms_oInstance._IsTheCurrentPauseName(a_rPauseName);
		}
		
		static public void Pause(bool a_bPause, bool a_bStackUpSamePause = false)
		{
			Pause(mc_oDefaultPauseName, a_bPause, a_bStackUpSamePause);
		}
		
		static public void Pause(string a_rPauseName, bool a_bPause, bool a_bStackUpSamePause = false)
		{
			TryCreateInstanceIfNeeded();
			if(ms_oInstance == null)
			{
				return;
			}
			
			ms_oInstance._Pause(a_rPauseName, a_bPause, a_bStackUpSamePause);
		}
		
		bool _ContainsTheCurrentPauseName(List<string> a_rPauseNames)
		{
			if(paused)
			{
				return a_rPauseNames.Contains(GetCurrentPauseName());
			}
			else
			{
				return false;
			}
		}
		
		bool _IsTheCurrentPauseName(string a_rPauseName)
		{
			if(paused)
			{
				return GetCurrentPauseName() == a_rPauseName;
			}
			else
			{
				return false;
			}
		}
		
		string GetCurrentPauseName()
		{
			return pauseNamesStack[pauseNamesStack.Count - 1];
		}
		
		
		void _Pause(string a_rPauseName, bool a_bPause, bool a_bStackUpSamePause)
		{
			bool bIgnoreAction = false;
			if(a_bPause)
			{
				if(a_bStackUpSamePause == false)
				{
					// Check if the pause is already in the stack and remove it if it the case
					int iPauseIndex = pauseNamesStack.FindLastIndex(pauseName => pauseName == a_rPauseName);
					if(iPauseIndex != -1)
					{
						pauseNamesStack.RemoveAt(iPauseIndex);
					}
				}
						
				// Add a pause
				pauseNamesStack.Add(a_rPauseName);
				paused = true;
			}
			else
			{
				// Remove the last pause that have been added with this name
				int iPauseIndex = pauseNamesStack.FindLastIndex(pauseName => pauseName == a_rPauseName);
				if(iPauseIndex != -1)
				{
					pauseNamesStack.RemoveAt(iPauseIndex);
				}
				else
				{
					bIgnoreAction = true;
				}
				
				// If there is no pause name left
				if(pauseNamesStack.Count == 0)
				{
					paused = false;
				}
			}
			
			if(bIgnoreAction == false)
			{
				OnPause(paused);
			}
		}
		
		void OnPause(bool a_bPause)
		{
			if(a_bPause)
			{
				Time.timeScale = 0.0f;	
			}
			else
			{
				Time.timeScale = 1.0f;
			}
			
			if(onPause != null)
			{
				onPause(a_bPause);
			}
		}
	}
}