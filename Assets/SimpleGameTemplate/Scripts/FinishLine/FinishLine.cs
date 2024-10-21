using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;

namespace SimpleGameTemplate
{
	[ExecuteInEditMode()]
	[AddComponentMenu("SimpleGameTemplate/FinishLine")]
	public class FinishLine : MonoBehaviour 
	{
		public bool success = true;

		public void ReachFinishLine()
		{
			Game.Instance.LevelCompleted(success);
		}
	}
}