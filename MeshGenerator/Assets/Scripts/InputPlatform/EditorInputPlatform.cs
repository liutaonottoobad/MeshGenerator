/* ****************************************
*******************************************
* author : kaka
* create time : 2019-04-01 22:43:55
* description : 
*******************************************
** ************************************* */

using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_WIN
namespace MaybeInside
{
	public static class GameInput
	{
		public static Vector3 Position => Input.mousePosition;

		public static bool IsTouchBegin()
		{
			return Input.GetMouseButtonDown(0);
		}

		public static bool IsTouchMove()
		{
			return Input.GetMouseButton(0);
		}

		public static bool IsTouchEnd()
		{
			return Input.GetMouseButtonUp(0);
		}

		public static bool IsPointerOverGameObject()
		{
			return EventSystem.current.IsPointerOverGameObject();
		}
	}
}
#endif
