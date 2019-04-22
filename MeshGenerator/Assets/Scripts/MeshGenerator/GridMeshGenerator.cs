/* ****************************************
*******************************************
* author : Administrator
* create time : 2019-04-21 10:37:43
* description : 
*******************************************
** ************************************* */

using UnityEngine;

namespace MaybeInside
{
	public class GridMeshGenerator : IMeshGenerator
	{
		public Mesh GenMesh(MeshGeneratorFactory factory)
		{
			var cellSize = factory.CellSize;

			var leftBottom = new Vector3(
				-(cellSize.x * factory.Width * 0.5f),
				0f,
				-(cellSize.z * factory.Height * 0.5f)
			);

			var vertices = new Vector3[(factory.Width + 1) * (1 + factory.Height)];
			for (int i = 0; i <= factory.Height; i++)
			{
				for (int j = 0; j <= factory.Width; j++)
				{
					var x = leftBottom.x + cellSize.x * j;
					var z = leftBottom.z + cellSize.z * i;
					vertices[i * factory.Height + j + i] = new Vector3(x, 0f, z);
				}
			}

			var lines = new int[(factory.Width + 1) * (factory.Height + 1) * 2];
			int index = 0;
			for (int i = 0; i <= factory.Height; i++)
			{
				int start = i * (factory.Width + 1);
				lines[index] = start;
				index++;
				lines[index] = start + factory.Width;
				index++;
			}

			for (int i = 0; i <= factory.Width; i++)
			{
				lines[index] = i;
				index++;
				lines[index] = i + (factory.Width + 1) * factory.Height;
				index++;
			}

			var mesh = new Mesh
			{
				vertices = vertices,
			};
			mesh.SetIndices(lines, MeshTopology.Lines, 0);
			return mesh;
		}
	}
}
