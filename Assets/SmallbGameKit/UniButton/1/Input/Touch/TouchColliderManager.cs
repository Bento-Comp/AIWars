using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

namespace UniButton
{
	[DefaultExecutionOrder(-32000)]
	[AddComponentMenu("TemplateFolder/TouchColliderManager")]
	public class TouchColliderManager : UniSingleton.Singleton<TouchColliderManager>
	{
		class RaycastCommand
		{
			Ray ray;
			TouchColliderRaycastParameters raycastParameters;

			public RaycastCommand(Ray ray, TouchColliderRaycastParameters raycastParameters)
			{
				this.ray = ray;
				this.raycastParameters = raycastParameters;
			}

			public override bool Equals(object otherObject) =>
			otherObject is RaycastCommand other &&
				(other.ray, other.raycastParameters)
				.Equals((ray, raycastParameters));

			public override int GetHashCode() => (ray, raycastParameters).GetHashCode();

			public RaycastHit[] Execute()
			{
				if(raycastParameters.firstHitOnly)
				{
					RaycastHit hit;

					if(Physics.Raycast(ray, out hit, raycastParameters.MaxDistance, raycastParameters.layerMask))
					{
						return new RaycastHit[]{hit};
					}
					else
					{
						return new RaycastHit[]{};
					}
				}
				else
				{
					return Physics.RaycastAll(ray, raycastParameters.MaxDistance, raycastParameters.layerMask);
				}
			}
		}

		Dictionary<RaycastCommand, RaycastHit[]> raycastCommandHits = new Dictionary<RaycastCommand, RaycastHit[]>();

		public RaycastHit[] Raycast(Ray ray, TouchColliderRaycastParameters raycastParameters)
		{
			RaycastHit[] hits;

			RaycastCommand command = new RaycastCommand(ray, raycastParameters);

			if(raycastCommandHits.TryGetValue(command, out hits))
			{
				//Debug.Log("Reused command : command dictionnary entries count = " + raycastCommandHits.Count);
				return hits;
			}

			hits = command.Execute();

			raycastCommandHits.Add(command, hits);

			return hits;
		}

		void Update()
		{
			raycastCommandHits.Clear();
		}
	}
}