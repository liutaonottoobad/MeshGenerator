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

		public int ReactiveGroundLayer;
		public int ReactiveBlocksLayer;

		[EditorButton]
		private void RefreshReactiveLayer()
		{
			ReactiveGroundLayer = LayerMask.GetMask("ReactiveGround");
			ReactiveBlocksLayer = LayerMask.GetMask("ReactiveBlocks");
		}

		private struct TouchInfo
		{
			public Vector3 ScreenPos;
			public Vector3 WorldPos;
			public Transform HitTransform;
			public IReactive Reactive;
		}

		private bool TryRayCastReactiveObject(Vector3 screenPos, int layer, out TouchInfo touchInfo)
		{
			var ray = Camera.ScreenPointToRay(screenPos);
			RaycastHit rayCastHit;
			if (Physics.Raycast(ray, out rayCastHit, Mathf.Infinity, layer))
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
				if (TryRayCastReactiveObject(GameInput.Position, ReactiveBlocksLayer, out touchInfo))
				{
					_select = touchInfo;
					_select.Reactive.TouchBegin(touchInfo.WorldPos);

					Debug.DrawLine(Camera.transform.position, touchInfo.WorldPos, Color.red, 10);
				}
			}
			else if (_select.HitTransform != null && _select.Reactive != null && GameInput.IsTouchMove())
			{
				TouchInfo touchInfo;
				if (TryRayCastReactiveObject(GameInput.Position, ReactiveGroundLayer, out touchInfo))
				{
					_select.Reactive.TouchMoved(touchInfo.WorldPos);
				}
				else if (TryRayCastReactiveObject(GameInput.Position, ReactiveBlocksLayer, out touchInfo))
				{
					_select.Reactive.TouchMoved(touchInfo.WorldPos);
				}
			}
			else if (_select.HitTransform != null && _select.Reactive != null && GameInput.IsTouchEnd())
			{
				TouchInfo touchInfo;
				if (TryRayCastReactiveObject(GameInput.Position, ReactiveGroundLayer, out touchInfo))
				{
					_select.Reactive.TouchEnd(touchInfo.WorldPos);
				}
				else if (TryRayCastReactiveObject(GameInput.Position, ReactiveBlocksLayer, out touchInfo))
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
