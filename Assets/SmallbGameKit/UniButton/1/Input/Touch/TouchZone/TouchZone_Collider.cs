using UnityEngine;
using System.Collections;
using System;

namespace UniButton
{
	[AddComponentMenu("UniButton/Input/Touch/TouchZone/TouchZone_Collider")]
	public class TouchZone_Collider : TouchZoneBase
	{
		public Collider colliderComponent;
		
		public bool invertZone;

		public bool useTouchColliderRaycastManager;
		public TouchColliderRaycastParameters raycastParameters;
		
		protected override bool _ContainsScreenPoint(Vector2 screenPoint, Camera camera)
		{
			Ray ray = camera.ScreenPointToRay(screenPoint);
			RaycastHit hit;

			bool isIn;
			if(useTouchColliderRaycastManager)
			{
				RaycastHit[] hits = TouchColliderManager.Instance.Raycast(ray, raycastParameters);
				
				isIn = HitsContainCollider(hits);
			}
			else
			{
				isIn = colliderComponent.Raycast(ray, out hit, float.PositiveInfinity);
			}

			if(invertZone)
			{
				return !isIn;
			}
			else
			{
				return isIn;
			}
		}

		bool HitsContainCollider(RaycastHit[] hits)
		{
			for(int i = 0; i < hits.Length; ++i)
			{
				if(hits[i].collider == colliderComponent)
					return true;
			}

			return false;
		}
	}
}