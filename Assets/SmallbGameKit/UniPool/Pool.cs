using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UniPool
{
	[AddComponentMenu("UniPool/Pool")]
	public class Pool<ComponentType> : PoolBase where ComponentType : PoolInstance
	{
		public ComponentType model;

		public Transform root;

		public int size = 0;

		Stack<ComponentType> destroyed = new Stack<ComponentType>();

		public ComponentType CreatePoolInstance()
		{
			ComponentType instance;

			if(destroyed.Count > 0)
			{
				instance = destroyed.Pop();
				if(instance.PoolInstanceIsDestroyed == false)
				{
					Debug.LogError("Pop an active object : " + instance);
				}
			}
			else
			{
				instance = InstantiatePoolInstance();
				++size;
			}

			instance.PoolInstanceIsDestroyed = false;
			instance.gameObject.SetActive(true);

			if(root != null)
			{
				instance.transform.SetParent(root, false);
			}

			return instance;
		}

		public override void DestroyPoolInstance(PoolInstance instance)
		{
			if(instance.CanDestroy == false)
				return;
			
			base.DestroyPoolInstance(instance);
			destroyed.Push(instance as ComponentType);
		}

		protected virtual void Awake()
		{
			model.CanDestroy = false;
			model.gameObject.SetActive(false);
			model.PoolInstanceIsDestroyed = true;
			CreatePool();
		}

		void CreatePool()
		{
			for(int i = 0; i < size; ++i)
			{
				DestroyPoolInstance(InstantiatePoolInstance());
			}
		}

		ComponentType InstantiatePoolInstance()
		{
			ComponentType instance = Instantiate(model, transform) as ComponentType;
			instance.Initialize(this);

			return instance;
		}
	}
}