/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-20 23:49:11
* description : 
*******************************************
** ************************************* */

using UnityEngine;

namespace MaybeInside
{
	public class EditorBlock : MonoBehaviour
	{
		[Range(0, byte.MaxValue)]
		public byte Height;
		public BoxCollider BoxCollider;
		public Transform BoxColliderView;

		private void Awake()
		{
			gameObject.transform.hideFlags |= HideFlags.NotEditable;
		}

		private void Update()
		{
			//			transform.localScale = Vector3.one * Height;
			BoxCollider.center = new Vector3(0f, Height * 0.5f, 0f);
			BoxCollider.size = new Vector3(1f, Height, 1f);
			BoxColliderView.localScale = BoxCollider.size;
			BoxColliderView.localPosition = new Vector3(0f, Height * 0.5f, 0f);
			var pos = transform.localPosition;
			pos.y = 0f;
			transform.localPosition = pos;
		}
	}
}
