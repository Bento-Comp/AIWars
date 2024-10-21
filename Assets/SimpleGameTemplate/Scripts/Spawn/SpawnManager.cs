using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/SpawnManager")]
	public class SpawnManager : UniSingleton.Singleton<SpawnManager>
	{
		public float spaceBetweenMin = 3.0f;
		public float spaceBetweenMax = 6.0f;

		public float yMin = -5.0f;
		public float yMax = 5.0f;

		public float widthMin = 0.5f;
		public float widthMax = 2.0f;

		public float heightMin = 0.5f;
		public float heightMax = 2.0f;

		public float collectableProbability = 0.25f;
	}
}