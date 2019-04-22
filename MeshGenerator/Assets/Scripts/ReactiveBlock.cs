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

		private Vector3 NearestPos(Vector3 touchPos)
		{
			float width = 1f;
			float height = 1f;

			var pos = touchPos;

			var w = (int)(touchPos.x / width);
			var x = touchPos.x - w * width;
			if (Mathf.Abs(x) < width * 0.5f)
			{
				pos.x = w * width;
			}
			else
			{
				pos.x = (w + Mathf.Sign(touchPos.x)) * width;
			}

			var h = (int)(touchPos.z / height);
			var z = touchPos.z - h * height;
			if (Mathf.Abs(z) < height * 0.5f)
			{
				pos.z = h * height;
			}
			else
			{
				pos.z = (h + Mathf.Sign(touchPos.z)) * height;
			}

			return pos;
		}

		public void TouchBegin(Vector3 pos)
		{
			var p = this.NearestPos(pos);
			p.y = transform.localPosition.y;
			transform.localPosition = p;
			//			BoxCollider.enabled = false;
		}

		public void TouchMoved(Vector3 pos)
		{
			var p = pos;//this.NearestPos(pos);
			p.y = transform.localPosition.y;
			transform.localPosition = p;
		}

		public void TouchEnd(Vector3 pos)
		{
			var p = this.NearestPos(pos);
			p.y = transform.localPosition.y;
			transform.localPosition = p;
			//			BoxCollider.enabled = true;
		}
	}
}
