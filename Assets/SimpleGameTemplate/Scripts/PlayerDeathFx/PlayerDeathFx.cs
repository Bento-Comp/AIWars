using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UniSkin;

namespace SimpleGameTemplate
{
	[SelectionBase()]
	[AddComponentMenu("SimpleGameTemplate/PlayerDeathFx")]
	public class PlayerDeathFx : UniPool.PoolInstance
	{
		public ParticleSystem particleSystemComponent;

		void LateUpdate()
		{
			if(particleSystemComponent.IsAlive() == false)
			{
				DestroyPoolInstance();
			}
		}
	}
}