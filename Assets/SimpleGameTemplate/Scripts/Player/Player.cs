using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameFramework;
using GameFramework.SimpleGame;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/Player")]
	public class Player : GameBehaviour 
	{
		public Player_Body body;
		public Player_Input input;
		public Player_Movement movement;

		Vector2 initialPosition;

		Vector2 gameOverPosition;

		bool Active
		{
			set
			{
				gameObject.SetActive(value);
			}
		}

		public void Hit()
		{
			if(Game.Instance.IsGamePlay == false)
				return;

			Kill();
			Game.Instance.GameOver();
		}

		public void Kill()
		{
			if(Game.Instance.IsGamePlay == false)
				return;

			Active = false;

			PlayerDeathFx playerDeathFx = PlayerDeathFxFactory.Instance.CreatePoolInstance();
			playerDeathFx.transform.position = body.Position_Visual;
		}

		protected override void OnLoadGame()
		{
			if(FirstGameLoad)
			{
				initialPosition = body.Position_Physical;
			}
			else
			{
				body.Position_Physical = initialPosition;
			}

			Active = true;
		}

		protected override void OnGameOver()
		{
			base.OnGameOver();

			gameOverPosition = body.Position_Visual;
		}

		protected override void OnAwake()
		{
			base.OnAwake();

			ContinueManager.onContinue += OnContinue;
		}

		protected override void OnAwakeEnd()
		{
			base.OnAwakeEnd();

			ContinueManager.onContinue -= OnContinue;
		}

		void OnContinue()
		{
			Vector2 continuePosition = gameOverPosition;
			continuePosition.y = initialPosition.y;

			body.Position_Physical = continuePosition;

			Active = true;
		}
	}
}