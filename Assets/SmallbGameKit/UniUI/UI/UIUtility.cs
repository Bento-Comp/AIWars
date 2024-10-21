using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniUI
{
	public static class UIUtility
	{
		public static Vector2 WorldToRectTransformLocalPosition(Canvas canvas, RectTransform parentRectTransform, Camera camera,
			Vector3 worldPosition)
		 {
			 Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, worldPosition);

			 Vector2 localPosition;
			 RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPoint,
				 canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera, out localPosition);
 
			return localPosition;
		 }

		public static Vector2 RectTransformToRectTransformLocalPosition(RectTransform parentRectTransformFrom,
			RectTransform parentRectTransformTo,
			Vector3 localPosition)
		 {
			Vector3 worldPosition = parentRectTransformFrom.TransformPoint(localPosition);
			Vector3 newLocalPosition = parentRectTransformTo.InverseTransformPoint(worldPosition);

			return newLocalPosition;
		 }
	}
}
