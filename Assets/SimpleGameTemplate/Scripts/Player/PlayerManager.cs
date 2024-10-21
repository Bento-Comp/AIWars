using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/PlayerManager")]
	public class PlayerManager : UniSingleton.Singleton<PlayerManager>
	{
		[SerializeField]
		Player player = null;

		public float moveVelocity = 6.0f;

		public float jumpVelocity = 12.0f;

		public float gravity = 20.0f;

		public Player Player
		{
			get
			{
				return player;
			}
		}
	}
}