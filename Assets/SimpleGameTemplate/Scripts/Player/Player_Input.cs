using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.SimpleGame;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/Player_Input")]
	public class Player_Input : MonoBehaviour 
	{
		public System.Action onDown;

		public UniButton.TouchButtonController touchButtonController;

		public Camera cameraComponent;

		public bool Pressed
		{
			get
			{
				return GameScreenButton.Instance.Pressed;
			}
		}

		void Awake()
		{
			GameScreenButton.onDownDuringGamePlay += OnDown;
		}

		void OnDestroy()
		{
			GameScreenButton.onDownDuringGamePlay -= OnDown;
		}

		void OnDown()
		{
			if(onDown != null)
				onDown();
		}
	}
}