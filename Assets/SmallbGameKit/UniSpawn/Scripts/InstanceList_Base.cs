using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniSpawn
{
	[DefaultExecutionOrder(-32000)]
	[ExecuteAlways]
	[AddComponentMenu("UniSpawn/InstanceList_Base")]
	public abstract class InstanceList_Base : MonoBehaviour
	{
		public System.Action onUpdateList;

		public bool updateInEditMode;

		[SerializeField]
		int count = 3;

		public bool reverseSiblingOrder;

		public Transform instancesRootTransform;

		public int Count
		{
			get => count;

			set
			{
				if(count == value)
					return;

				count = value;

				UpdateList();
			}
		}

		protected void NotifyUpdateListCompleted()
		{
			onUpdateList?.Invoke();
		}

		protected abstract void UpdateList();
	}
}
