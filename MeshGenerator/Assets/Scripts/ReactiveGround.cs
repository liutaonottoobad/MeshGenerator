/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-21 00:42:37
* description : 
*******************************************
** ************************************* */

using UnityEngine;

namespace MaybeInside
{
	public class ReactiveGround : MonoBehaviour, IReactive
	{
		private IReactive _reactive;

		public void TouchBegin(Vector3 pos)
		{
			pos.x = 0f;
			pos.z = 0f;
			transform.position = pos;
		}

		public void TouchMoved(Vector3 pos)
		{
			_reactive.TouchMoved(pos);
		}

		public void TouchEnd(Vector3 pos)
		{
			_reactive.TouchEnd(pos);
		}

		public void SetReactive(IReactive reactive)
		{
			this.gameObject.SetActive(reactive != null);
			_reactive = reactive;
		}
	}
}
