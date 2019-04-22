/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-21 10:36:48
* description : 
*******************************************
** ************************************* */

using UnityEngine;

namespace MaybeInside
{
	public interface IMeshGenerator
	{
		Mesh GenMesh(MeshGeneratorFactory factory);
	}
}
