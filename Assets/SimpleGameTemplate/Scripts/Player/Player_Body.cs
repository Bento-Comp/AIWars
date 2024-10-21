using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/Player_Body")]
	public class Player_Body : MonoBehaviour 
	{
		public Rigidbody rigidbodyComponent;

		public SphereCollider sphereCollider;

		public float TopY
		{
			get
			{
				return sphereCollider.transform.position.y + sphereCollider.radius;
			}
		}

		public Vector2 Position_Visual
		{
			get
			{
				return transform.position;
			}

			set
			{
				transform.position = value;
			}
		}

		public Vector2 Position_Physical
		{
			get
			{
				return rigidbodyComponent.position;
			}

			set
			{
				rigidbodyComponent.position = value;
				Position_Visual = value;
			}
		}

		public Vector2 Velocity
		{
			get
			{
				return rigidbodyComponent.velocity;
			}

			set
			{
				rigidbodyComponent.velocity = value;
			}
		}

		public float VelocityX
		{
			get
			{
				return Velocity.x;
			}

			set
			{
				Vector2 velocity = Velocity;

				velocity.x = value;

				Velocity = velocity;
			}
		}

		public float VelocityY
		{
			get
			{
				return Velocity.y;
			}

			set
			{
				Vector2 velocity = Velocity;

				velocity.y = value;

				Velocity = velocity;
			}
		}

		public void AddForce(Vector2 force)
		{
			rigidbodyComponent.AddForce(force);
		}
	}
}