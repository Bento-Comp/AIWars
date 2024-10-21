using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

namespace SimpleGameTemplate
{
	[AddComponentMenu("SimpleGameTemplate/PlayZone")]
	public class PlayZone : UniSingleton.Singleton<PlayZone>
	{
		public Vector2 extent = new Vector2(5.4f, 9.6f);

		public float referenceWidth = 1080.0f;

		public float referenceHeight = 1920.0f;

		public Transform centerTransform;

		Vector2 currentExtent;

		public Vector2 Extent
		{
			get
			{
				currentExtent = extent;
				currentExtent.x *= ((float)Screen.width * referenceHeight) / ((float)Screen.height * referenceWidth);

				return currentExtent;
			}
		}

		public Vector2 Size
		{
			get
			{
				return Extent * 2.0f;
			}
		}

		public Vector2 Center
		{
			get
			{
				return (Vector2)centerTransform.position;
			}
		}

		public Vector2 TopRight
		{
			get
			{
				return (Vector2)centerTransform.position + new Vector2(Extent.x, Extent.y);
			}
		}

		public Vector2 BottomRight
		{
			get
			{
				return (Vector2)centerTransform.position + new Vector2(Extent.x, -Extent.y);
			}
		}

		public Vector2 TopLeft
		{
			get
			{
				return (Vector2)centerTransform.position + new Vector2(-Extent.x, Extent.y);
			}
		}

		public Vector2 BottomLeft
		{
			get
			{
				return (Vector2)centerTransform.position - new Vector2(Extent.x, Extent.y);
			}
		}

		public Vector2 Top
		{
			get
			{
				return (Vector2)centerTransform.position + new Vector2(0.0f, Extent.y);
			}
		}

		public Vector2 Bottom
		{
			get
			{
				return (Vector2)centerTransform.position - new Vector2(0.0f, Extent.y);
			}
		}

		public Vector2 Left
		{
			get
			{
				return (Vector2)centerTransform.position - new Vector2(Extent.x, 0.0f);
			}
		}

		public Vector2 Right
		{
			get
			{
				return (Vector2)centerTransform.position + new Vector2(Extent.x, 0.0f);
			}
		}

		public float CenterY
		{
			get
			{
				return Center.y;
			}
		}

		public float CenterX
		{
			get
			{
				return Center.x;
			}
		}

		public float LeftX
		{
			get
			{
				return Left.x;
			}
		}

		public float RightX
		{
			get
			{
				return Right.x;
			}
		}

		public float TopY
		{
			get
			{
				return Top.y;
			}
		}

		public float BottomY
		{
			get
			{
				return Bottom.y;
			}
		}
	}
}