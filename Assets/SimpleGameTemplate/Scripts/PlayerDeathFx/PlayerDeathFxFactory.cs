using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/PlayerDeathFxFactory")]
	public class PlayerDeathFxFactory : UniPool.Pool<PlayerDeathFx>
	{
		static PlayerDeathFxFactory instance;

		public static PlayerDeathFxFactory Instance
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