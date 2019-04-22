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

		private Vector3 NearestPos(Vector3 touchPos)
		{
			float halfWidth = 0.5f;
			float halfHeight = 0.5f;

			var pos = touchPos;

			var width = (int)(touchPos.x / halfWidth);
			var x = touchPos.x - width * halfWidth;
			if (Mathf.Abs(x) < halfWidth * 0.5f)
			{
				pos.x = width * halfWidth;
			}
			else
			{
				pos.x = (width + Mathf.Sign(width)) * halfWidth;
			}

			var height = (int)(touchPos.z / halfHeight);
			var z = touchPos.z - height * halfHeight;
			if (Mathf.Abs(z) < halfHeight * 0.5f)
			{
				pos.z = height * halfHeight;
			}
			else
			{
				pos.z = (height + Mathf.Sign(height)) * halfHeight;
			}

			return pos;
		}

		private void LateUpdate()
		{
			if (GameInput.IsTouchBegin())
			{
				TouchInfo touchInfo;
				if (TryRayCastGround(GameInput.Position, out touchInfo))
				{
					Debug.LogFormat("name:{0}, pos:{1}", touchInfo.HitTransform.name, touchInfo.WorldPos);
					_select = touchInfo;
					_select.Reactive.TouchBegin(touchInfo.WorldPos);

					Debug.DrawLine(Camera.transform.position, touchInfo.WorldPos, Color.red, 10);
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
			else if (_select.HitTransform != null && _select.Reactive != null && GameInput.IsTouchEnd())
			{
				TouchInfo touchInfo;
				if (TryRayCastGround(GameInput.Position, out touchInfo))
				{
					_select.Reactive.TouchEnd(touchInfo.WorldPos);
				}
				_select.HitTransform = null;
				_select.Reactive = null;
			}
			else
			{
				_select.HitTransform = null;
				_select.Reactive = null;
			}
		}
	}
}
