using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniSkin
{
	[DefaultExecutionOrder(-32000)]
	[ExecuteInEditMode()]
	[AddComponentMenu("UniSkin/SkinManager")]
	public class SkinManager : SkinUserBase
	{
		public static System.Action<int> onSkinChange;

		public bool debug_updateSkinInRuntime = true;

		static SkinManager instance;

		public static SkinManager Instance
		{
			get
			{
				return instance;
			}
		}

		protected override void OnSkinChange(int skinIndex)
		{
			base.OnSkinChange(skinIndex);

			onSkinChange?.Invoke(skinIndex);
		}

		override protected void Awake()
		{
			base.Awake();

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

		override protected void OnDestroy()
		{
			base.OnDestroy();

			if(instance == this)
			{
				instance = null;
			}
		}

		#if UNITY_EDITOR
		void LateUpdate()
		{
			if(Application.isPlaying == false)
				instance = this;
		}
		#endif
	}
}
