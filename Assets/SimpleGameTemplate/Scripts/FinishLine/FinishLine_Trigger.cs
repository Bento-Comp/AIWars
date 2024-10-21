using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.SimpleGame;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/FinishLine_Trigger")]
	public class FinishLine_Trigger : MonoBehaviour 
	{
		public FinishLine finishLine;

		void OnTriggerEnter(Collider collider)
		{
			Player_Collider playerCollider = collider.GetComponent<Player_Collider>();
			if(playerCollider == null)
				return;
			
			finishLine.ReachFinishLine();
		}
	}
}