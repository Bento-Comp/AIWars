using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.SimpleGame;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/Player_Movement")]
	public class Player_Movement : GameBehaviour
	{
		public Player player;

		Vector2 dragStartPosition;

		Vector2 lastCommandTargetPosition;

		protected override void OnLoadGame()
		{
		}

		protected override void OnAwake()
		{
			player.input.onDown += OnDown;
		}

		protected override void OnAwakeEnd()
		{
			player.input.onDown -= OnDown;
		}

		void FixedUpdate()
		{
			UpdateMovement();
		}

		void UpdateMovement()
		{
			if(Game.Instance.IsGameStarted == false)
			{
				player.body.Velocity = Vector2.zero;
				return;
			}
			
			PlayerManager playerManager = PlayerManager.Instance;

			player.body.VelocityX = playerManager.moveVelocity;

			player.body.AddForce(Vector2.down * playerManager.gravity);
		}

		void OnDown()
		{
			player.body.VelocityY += PlayerManager.Instance.jumpVelocity;
		}
	}
}