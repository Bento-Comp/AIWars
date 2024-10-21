using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/CollectableFactory")]
	public class CollectableFactory : UniPool.Pool<Collectable>
	{
		static CollectableFactory instance;

		public static CollectableFactory Instance
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