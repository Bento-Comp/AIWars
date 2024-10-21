using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Template
{
	[AddComponentMenu("TemplateFolder/SingletonRawTemplate")]
	public class SingletonRawTemplate : MonoBehaviour
	{
		static SingletonRawTemplate instance;
		
		public static SingletonRawTemplate Instance
		{
			get
			{
				return instance;
			}
		}
		
		void Awake()
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
		
		void OnDestroy()
		{
			if(instance == this)
			{
				instance = null;
			}
		}
	}
}