/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-21 10:35:18
* description : 
*******************************************
** ************************************* */

using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace MaybeInside
{
	public class MeshGeneratorFactory : MonoBehaviour
	{
		public Vector3 CellSize;
		public int Width;
		public int Height;
		public string Path;

		public string[] MeshGeneratorTypes;

		private static readonly Type GeneratorType = typeof (IMeshGenerator);
		[EditorButton]
		private void GenMesh(int index)
		{
			if (index < 0 || index >= MeshGeneratorTypes.Length)
			{
				Debug.LogErrorFormat("out of range:{0}", index.ToString());
				return;
			}

			var n = MeshGeneratorTypes[index];
			var type = GeneratorType.Assembly.GetType(n);
			if (type == null)
			{
				Debug.LogErrorFormat("没有找到对应的生成类型:{0}", n);
				return;
			}
			var generator = Activator.CreateInstance(type) as IMeshGenerator;
			var mesh = generator.GenMesh(this);
			AssetDatabase.CreateAsset(mesh, Path);
		}
	}
}
#endif
