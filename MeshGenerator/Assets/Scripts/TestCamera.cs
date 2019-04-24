/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-24 18:40:07
* description : 
*******************************************
** ************************************* */

using UnityEngine;

namespace MaybeInside
{
	public class TestCamera : MonoBehaviour
	{
		public Camera Camera;
		public Transform Point;

		[EditorButton]
		private void Test()
		{
			Debug.LogFormat("{0}", (Camera.projectionMatrix * Camera.worldToCameraMatrix).MultiplyPoint(Point.position));
		}
	}
}
