using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.SimpleGame;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/Collectable_Collider")]
	public class Collectable_Collider : MonoBehaviour 
	{
		public Collectable collectable;

		void OnTriggerEnter(Collider collider)
		{
			Player_Collider playerCollider = collider.GetComponent<Player_Collider>();
			if(playerCollider == null)
				return;

			ScoreManager.Instance.AddScore(1);
			collectable.DestroyPoolInstance();
		}
	}
}