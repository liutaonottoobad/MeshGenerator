/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-21 00:08:39
* description : 
*******************************************
** ************************************* */

using UnityEngine;

namespace MaybeInside
{
	public class TerrainTouchController : MonoBehaviour
	{
		public Camera Camera;

		private struct TouchInfo
		{
			public Vector3 ScreenPos;
			public Vector3 WorldPos;
			public Transform HitTransform;
			public IReactive Reactive;
		}

		private bool TryRayCastGround(Vector3 screenPos, out TouchInfo touchInfo)
		{
			var camera = Camera;
			var ray = camera.ScreenPointToRay(screenPos);
			RaycastHit rayCastHit;
			if (Physics.Raycast(ray, out rayCastHit, Mathf.Infinity))
			{
				touchInfo = new TouchInfo()
				{
					ScreenPos = screenPos,
					WorldPos = rayCastHit.point,
					HitTransform = rayCastHit.transform,
					Reactive = rayCastHit.transform.GetComponent<IReactive>()
				};
				return true;
			}
			touchInfo = new TouchInfo();
			return false;
		}

		private TouchInfo _select;

		private void LateUpdate()
		{
			if (GameInput.IsTouchBegin())
			{
				TouchInfo touchInfo;
				if (TryRayCastGround(GameInput.Position, out touchInfo))
				{
					Debug.LogFormat("name:{0}, pos:{1}", touchInfo.HitTransform.name, touchInfo.WorldPos);
					_select = touchInfo;
					_select.Reactive.TouchBegin();
				}
			}
			else if (_select.HitTransform != null && _select.Reactive != null && GameInput.IsTouchMove())
			{
				TouchInfo touchInfo;
				if (TryRayCastGround(GameInput.Position, out touchInfo))
				{
					_select.Reactive.TouchMoved(touchInfo.WorldPos);
				}
			}
			else
			{
				_select.HitTransform = null;
				if (_select.Reactive != null)
				{
					_select.Reactive.TouchEnd();
					_select.Reactive = null;
				}
			}
		}
	}
}
