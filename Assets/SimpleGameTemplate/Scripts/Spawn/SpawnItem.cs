using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimpleGameTemplate
{
	[AddComponentMenu("StringRunner/SpawnItem")]
	public class SpawnItem : UniPool.PoolInstance, GameFramework.IGameBehaviour
	{
		public GameElement_Body body;

		public void NotifyLoadGame()
		{
		}

		public void NotifyLoadGameEnd(bool reloadSceneAfter)
		{
			if(IsPooled)
				DestroyPoolInstance();
		}

		public void NotifyGameStart()
		{
		}

		public void NotifyInterlude()
		{
		}

		public void NotifyInterludeEnd()
		{
		}

		public void NotifyGameOver()
		{
		}

		public void NotifyLevelCompleted(bool success)
		{
		}

		void Awake()
		{
			if(Application.isPlaying)
			{
				GameFramework.Game.Instance.Register(this);
			}
		}

		void OnDestroy()
		{
			if(GameFramework.Game.Instance != null)
				GameFramework.Game.Instance.Unregister(this);
		}

		void FixedUpdate()
		{
			if(Application.isPlaying == false)
				return;
			
			if(body.RightX < PlayZone.Instance.LeftX)
			{
				DestroyPoolInstance();
			}
		}
	}
}