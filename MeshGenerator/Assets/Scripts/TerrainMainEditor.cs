/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-21 00:04:37
* description : 
*******************************************
** ************************************* */

using UnityEngine;

namespace MaybeInside
{
	public class TerrainMainEditor : MonoBehaviour
	{
		[EditorButton]
		private void CreateTrianglesMesh(int width, int height, string path)
		{
			var cellSize = new Vector3(1f, 0f, 1f);

			var leftBottom = Vector3.zero;//this.GetLeftBottom(width, height, cellSize);
			var vertices = new Vector3[(width + 1) * (1 + height)];
			for (int i = 0; i <= height; i++)
			{
				for (int j = 0; j <= width; j++)
				{
					var x = leftBottom.x + cellSize.x * j;
					var z = leftBottom.z + cellSize.z * i;
					vertices[i * height + j + i] = new Vector3(x, 0f, z);
				}
			}

			var triangles = new int[width * height * 2 * 3];
			int trianglesIndex = 0;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					int start = i * width + j;
					triangles[trianglesIndex] = start;
					trianglesIndex++;
					triangles[trianglesIndex] = start + width + 1;
					trianglesIndex++;
					triangles[trianglesIndex] = start + width + 2;
					trianglesIndex++;

					triangles[trianglesIndex] = start;
					trianglesIndex++;
					triangles[trianglesIndex] = start + width + 2;
					trianglesIndex++;
					triangles[trianglesIndex] = start + 1;
					trianglesIndex++;
				}
			}
			var mesh = new Mesh
			{
				vertices = vertices,
				triangles = triangles
			};
#if UNITY_EDITOR
			UnityEditor.AssetDatabase.CreateAsset(mesh, path);
#endif
		}
	}
}
