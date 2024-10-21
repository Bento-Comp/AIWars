using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/Spawner")]
	public class Spawner : GameFramework.GameBehaviour
	{
		float nextItemLeft;

		static Spawner instance;

		public static Spawner Instance
		{
			get
			{
				return instance;
			}
		}

		protected override void OnLoadGame()
		{
			nextItemLeft = PlayZone.Instance.RightX;
		}

		protected override void OnAwake()
		{
			if(instance == null)
			{
				instance = this;
			}
			else
			{
				Debug.LogWarning("A singleton can only be instantiated once!");
				Destroy(gameObject);
				return;
			}
		}

		protected override void OnAwakeEnd()
		{
			if(instance == this)
			{
				instance = null;
			}
		}

		void Update()
		{
			SpawnManager spawnManager = SpawnManager.Instance;

			if(nextItemLeft <= PlayZone.Instance.RightX)
			{
				GameElement_Body body;
				if(Random.Range(0.0f, 1.0f) < spawnManager.collectableProbability)
				{
					Obstacle obstacle = ObstacleFactory.Instance.CreatePoolInstance();
					body = obstacle.body;
				}
				else
				{
					Collectable collectable = CollectableFactory.Instance.CreatePoolInstance();
					body = collectable.body;
				}

				body.Width = Random.Range(spawnManager.widthMin, spawnManager.widthMax);
				body.Height = Random.Range(spawnManager.heightMin, spawnManager.heightMax);

				body.Left = new Vector2( nextItemLeft, Random.Range(spawnManager.yMin, spawnManager.yMax) );

				nextItemLeft = body.RightX + Random.Range(spawnManager.spaceBetweenMin, spawnManager.spaceBetweenMax);
			}
		}

		Obstacle SpawnObstacle(float left)
		{
			Obstacle obstacle = ObstacleFactory.Instance.CreatePoolInstance();

			obstacle.body.Left = new Vector2(left, 0.0f);

			return obstacle;
		}
	}
}