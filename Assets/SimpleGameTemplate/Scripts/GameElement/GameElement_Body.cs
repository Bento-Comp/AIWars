using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SimpleGameTemplate
{
	[ExecuteInEditMode()]
	[AddComponentMenu("SimpleGameTemplate/GameElement_Body")]
	public class GameElement_Body : MonoBehaviour
	{
		[SerializeField]
		float width = 1.0f;

		[SerializeField]
		float height = 1.0f;

		public Vector2 Size
		{
			get
			{
				return new Vector2(width, height);
			}

			set
			{
				if(width != value.x || height != value.y)
				{
					width = value.x;
					height = value.y;

					OnSizeChange();
				}
			}
		}

		public Vector2 Extent
		{
			get
			{
				return Size * 0.5f;
			}

			set
			{
				Size = value * 2.0f;
			}
		}

		public float Height
		{
			get
			{
				return height;
			}

			set
			{
				if(this.height != value)
				{
					this.height = value;
					OnSizeChange();
				}
			}
		}

		public float Width
		{
			get
			{
				return width;
			}

			set
			{
				if(this.width != value)
				{
					this.width = value;
					OnSizeChange();
				}
			}
		}

		public Vector2 Center
		{
			get
			{
				return (Vector2)transform.position;
			}

			set
			{
				transform.position = value;
			}
		}

		public Vector2 TopRight
		{
			get
			{
				return (Vector2)transform.position + new Vector2(Extent.x, Extent.y);
			}

			set
			{
				transform.position = value - new Vector2(Extent.x, Extent.y);
			}
		}

		public Vector2 BottomRight
		{
			get
			{
				return (Vector2)transform.position + new Vector2(Extent.x, -Extent.y);
			}

			set
			{
				transform.position = value - new Vector2(Extent.x, -Extent.y);
			}
		}

		public Vector2 TopLeft
		{
			get
			{
				return (Vector2)transform.position + new Vector2(-Extent.x, Extent.y);
			}

			set
			{
				transform.position = value - new Vector2(-Extent.x, Extent.y);
			}
		}

		public Vector2 BottomLeft
		{
			get
			{
				return (Vector2)transform.position - new Vector2(Extent.x, Extent.y);
			}

			set
			{
				transform.position = value + new Vector2(Extent.x, Extent.y);
			}
		}

		public Vector2 Top
		{
			get
			{
				return (Vector2)transform.position + new Vector2(0.0f, Extent.y);
			}

			set
			{
				transform.position = value - new Vector2(0.0f, Extent.y);
			}
		}

		public Vector2 Bottom
		{
			get
			{
				return (Vector2)transform.position - new Vector2(0.0f, Extent.y);
			}

			set
			{
				transform.position = value + new Vector2(0.0f, Extent.y);
			}
		}

		public Vector2 Left
		{
			get
			{
				return (Vector2)transform.position - new Vector2(Extent.x, 0.0f);
			}

			set
			{
				transform.position = value + new Vector2(Extent.x, 0.0f);
			}
		}

		public Vector2 Right
		{
			get
			{
				return (Vector2)transform.position + new Vector2(Extent.x, 0.0f);
			}

			set
			{
				transform.position = value - new Vector2(Extent.x, 0.0f);
			}
		}

		public float CenterY
		{
			get
			{
				return Center.y;
			}

			set
			{
				Vector2 position = Center;

				position.y = value;

				Center = position;
			}
		}

		public float CenterX
		{
			get
			{
				return Center.x;
			}

			set
			{
				Vector2 position = Center;

				position.x = value;

				Center = position;
			}
		}

		public float LeftX
		{
			get
			{
				return Left.x;
			}

			set
			{
				Vector2 position = Left;

				position.x = value;

				Left = position;
			}
		}

		public float RightX
		{
			get
			{
				return Right.x;
			}

			set
			{
				Vector2 position = Right;

				position.x = value;

				Right = position;
			}
		}

		public float TopY
		{
			get
			{
				return Top.y;
			}

			set
			{
				Vector2 position = Top;

				position.y = value;

				Top = position;
			}
		}

		public float BottomY
		{
			get
			{
				return Bottom.y;
			}

			set
			{
				Vector2 position = Bottom;

				position.y = value;

				Bottom = position;
			}
		}

		protected virtual void OnSizeChange()
		{
			transform.localScale = new Vector3(width, height, 1.0f);
		}

		#if UNITY_EDITOR
		void Update()
		{
			if(Application.isPlaying)
				return;

			OnSizeChange();
		}
		#endif
	}
}