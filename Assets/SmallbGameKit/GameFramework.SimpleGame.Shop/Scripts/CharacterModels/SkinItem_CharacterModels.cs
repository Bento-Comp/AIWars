using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniSkin;

namespace GameFramework.SimpleGame
{
	[ExecuteInEditMode()]
	[AddComponentMenu("GameFramework/SimpleGame/SkinItem_CharacterModels")]
	public class SkinItem_CharacterModels : SkinItemBase
	{
		[SerializeField]
		CharacterModel model = null;

		public CharacterModel GetModel()
		{
			return model;
		}
	}
}