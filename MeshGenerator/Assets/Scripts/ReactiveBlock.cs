/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-21 00:43:05
* description : 
*******************************************
** ************************************* */

using UnityEngine;

namespace MaybeInside
{
	public class ReactiveBlock : MonoBehaviour, IReactive
	{
		public BoxCollider BoxCollider;

		public void TouchBegin()
		{
			BoxCollider.enabled = false;
		}

		public void TouchMoved(Vector3 pos)
		{
			pos.y = transform.localPosition.y;
			transform.localPosition = pos;
		}

		public void TouchEnd()
		{
			BoxCollider.enabled = true;
		}
	}
}
