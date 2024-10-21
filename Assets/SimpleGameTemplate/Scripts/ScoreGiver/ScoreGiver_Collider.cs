using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework.SimpleGame;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/ScoreGiver_Collider")]
	public class ScoreGiver_Collider : MonoBehaviour 
	{
		void OnCollisionEnter(Collision collision)
		{
			Player_Collider playerCollider = collision.collider.GetComponent<Player_Collider>();
			if(playerCollider == null)
				return;

			ScoreManager.Instance.AddScore(1);
		}
	}
}