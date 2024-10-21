using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Template
{
	[ExecuteInEditMode()]
	[AddComponentMenu("TemplateFolder/SingletonTemplate_ExecuteInEditor")]
	public class SingletonRawTemplate_ExecuteInEditMode : MonoBehaviour
	{
		static SingletonRawTemplate_ExecuteInEditMode instance;
		
		public static SingletonRawTemplate_ExecuteInEditMode Instance
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
				#if UNITY_EDITOR
				if(Application.isPlaying == false)
					return;
				#endif

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

		#if UNITY_EDITOR
		void LateUpdate()
		{
			if(Application.isPlaying == false)
			{
				instance = this;
			}
		}
		#endif
	}
}