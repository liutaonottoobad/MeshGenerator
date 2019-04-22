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

		[EditorButton]
		private void CreateGridMesh(int width, int height, string path)
		{
			var cellSize = new Vector3(1f, 0f, 1f);

			var leftBottom = new Vector3(
				-(cellSize.x * width * 0.5f),
				0f,
				-(cellSize.z * height * 0.5f)
			);
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

			var lines = new int[(width + 1) * (height + 1) * 2];
			int trianglesIndex = 0;
			for (int i = 0; i <= height; i++)
			{
				int start = i * (width + 1);
				lines[trianglesIndex] = start;
				trianglesIndex++;
				lines[trianglesIndex] = start + width;
				trianglesIndex++;
			}

			for (int i = 0; i <= width; i++)
			{
				lines[trianglesIndex] = i;
				trianglesIndex++;
				lines[trianglesIndex] = i + (width + 1) * height;
				trianglesIndex++;
			}

			var mesh = new Mesh
			{
				vertices = vertices,
			};
			mesh.SetIndices(lines, MeshTopology.Lines, 0);
#if UNITY_EDITOR
			UnityEditor.AssetDatabase.CreateAsset(mesh, path);
#endif
		}
	}
}
