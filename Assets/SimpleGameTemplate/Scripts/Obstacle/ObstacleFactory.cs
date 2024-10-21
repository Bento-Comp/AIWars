using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/ObstacleFactory")]
	public class ObstacleFactory : UniPool.Pool<Obstacle>
	{
		static ObstacleFactory instance;

		public static ObstacleFactory Instance
		{
			get
			{
				return instance;
			}
		}

		protected override void Awake()
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

			base.Awake();
		}
		
		void OnDestroy()
		{
			if(instance == this)
			{
				instance = null;
			}
		}
	}
}