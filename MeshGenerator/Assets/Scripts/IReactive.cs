/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-21 00:43:36
* description : 
*******************************************
** ************************************* */

using UnityEngine;

namespace MaybeInside
{
	public interface IReactive
	{
		void TouchBegin();
		void TouchMoved(Vector3 pos);
		void TouchEnd();
	}
}
