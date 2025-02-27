﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UniSkin
{
	[AddComponentMenu("UniSkin/SkinItem_FloatBase")]
	public abstract class SkinItem_FloatBase : SkinItemBase
	{
		public abstract float GetFloat(int index = 0, int count = 1);
	}
}
