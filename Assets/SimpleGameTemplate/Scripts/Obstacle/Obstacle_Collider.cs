using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/Obstacle_Collider")]
	public class Obstacle_Collider : MonoBehaviour 
	{
		public Obstacle obstacle;

		void OnCollisionEnter(Collision collision)
		{
			Player_Collider playerCollider = collision.collider.GetComponent<Player_Collider>();
			if(playerCollider == null)
				return;

			playerCollider.player.Hit();

			obstacle.DestroyPoolInstance();
		}
	}
}