using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.SimpleGame;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/Player_KillWhenOutOfScreen")]
	public class Player_KillWhenOutOfScreen : GameBehaviour
	{
		public Player player;

		void LateUpdate()
		{
			UpdateKillY();
		}

		void UpdateKillY()
		{
			if(Game.Instance.IsGamePlay == false)
				return;

			if(player.body.TopY < PlayZone.Instance.BottomY)
			{
				Game.Instance.GameOver();
			}
		}
	}
}