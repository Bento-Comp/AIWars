using UnityEngine;
using System.Collections;

using GameFramework;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/ViewFollowController")]
	public class ViewFollowController : GameBehaviour
	{
		public float smoothTime = 0.5f;

		public Vector3 centerOffset;

		public Transform target;

		public bool freezeX;
		public bool freezeY;
		public bool freezeZ = true;

		Vector3 currentVelocity;

		Vector3 offset;

		Vector3 initialPosition;

		protected override void OnLoadGame()
		{
			if(FirstGameLoad)
			{
				offset = transform.position - target.position;
				initialPosition = transform.position;
			}
			else
			{
				transform.position = initialPosition;
				currentVelocity = Vector3.zero;
			}
		}

		void LateUpdate()
		{
			UpdateFollow(Time.deltaTime);
		}

		void UpdateFollow(float deltaTime)
		{
			if(GameFramework.Game.Instance.IsGamePlay == false)
				return;
			
			Vector3 targetPoint = target.position;

			Vector3 position = transform.position;

			Vector3 nextPosition = Vector3.SmoothDamp(position - offset - centerOffset, targetPoint, ref currentVelocity, smoothTime) + offset + centerOffset;

			if(freezeX)
			{
				nextPosition.x = position.x;
			}

			if(freezeY)
			{
				nextPosition.y = position.y;
			}

			if(freezeZ)
			{
				nextPosition.z = position.z;
			}

			transform.position = nextPosition;
		}
	}
}