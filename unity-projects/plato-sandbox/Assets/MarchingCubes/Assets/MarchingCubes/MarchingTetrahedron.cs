using System.Collections.Generic;
using Assets.ClonerExample;
using Unity.Mathematics;
using UnityEngine;

namespace MarchingCubesProject
{
    public class MarchingTetrahedron 
    {
        public float3[] EdgeVertex = new float3[6];
        public float3[] CubePosition = new float3[8];
        public float3[] TetrahedronPosition = new float3[4];
        public float[] TetrahedronValue = new float[4];
        public float[] Cube = new float[8];

        /// <summary>
        /// MarchCubeTetrahedron performs the Marching Tetrahedrons algorithm on a single cube
        /// </summary>
        public void March(float x, float y, float z, float[] cube, IList<Vector3> vertList)
        {
            //Make a local copy of the cube's corner positions
            for (var i = 0; i < 8; i++)
            {
                CubePosition[i].x = x + VertexOffset[i, 0];
                CubePosition[i].y = y + VertexOffset[i, 1];
                CubePosition[i].z = z + VertexOffset[i, 2];
            }

            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    var vertexInACube = TetrahedronsInACube[i, j];
                    TetrahedronPosition[j] = CubePosition[vertexInACube];
                    TetrahedronValue[j] = cube[vertexInACube];
                }

                MarchTetrahedron(vertList);
            }
        }

        /// <summary>
        /// MarchTetrahedron performs the Marching Tetrahedrons algorithm on a single tetrahedron
        /// </summary>
        public void MarchTetrahedron(ICollection<Vector3> vertList)
        {
             var flagIndex = 0;

             //Find which vertices are inside of the surface and which are outside
            for (var i = 0; i < 4; i++) 
                if (TetrahedronValue[i] <= 0) 
                    flagIndex |= 1 << i;

            //Find which edges are intersected by the surface
            var edgeFlags = TetrahedronEdgeFlags[flagIndex];

            //If the tetrahedron is entirely inside or outside of the surface, then there will be no intersections
            if (edgeFlags == 0) return;

            //Find the point of intersection of the surface with each edge
            for (var i = 0; i < 6; i++)
            {
                //if there is an intersection on this edge
                if ((edgeFlags & (1 << i)) != 0)
                {
                    var vert0 = TetrahedronEdgeConnection[i, 0];
                    var vert1 = TetrahedronEdgeConnection[i, 1];
                    var offset = GetOffset(TetrahedronValue[vert0], TetrahedronValue[vert1]);
                    var invOffset = 1.0f - offset;

                    EdgeVertex[i].x = invOffset * TetrahedronPosition[vert0].x + offset * TetrahedronPosition[vert1].x;
                    EdgeVertex[i].y = invOffset * TetrahedronPosition[vert0].y + offset * TetrahedronPosition[vert1].y;
                    EdgeVertex[i].z = invOffset * TetrahedronPosition[vert0].z + offset * TetrahedronPosition[vert1].z;
                }
            }

            // Save the triangles that were found. There can be up to 2 per tetrahedron
            for (var i = 0; i < 2; i++)
            {
                if (TetrahedronTriangles[flagIndex, 3 * i] < 0) 
                    break;
                
                for (var j = 0; j < 3; j++)
                {
                    var vert = TetrahedronTriangles[flagIndex, 3 * i + j];
                    vertList.Add(EdgeVertex[vert]);
                }
            }
        }

        /// <summary>
        /// TetrahedronEdgeConnection lists the index of the endpoint vertices for each of the 6 edges of the tetrahedron.
        /// tetrahedronEdgeConnection[6][2]
        /// </summary>
        public static readonly int[,] TetrahedronEdgeConnection = {
	        {0,1},  {1,2},  {2,0},  {0,3},  {1,3},  {2,3}
	    };

        /// <summary>
        /// TetrahedronEdgeConnection lists the index of verticies from a cube 
        /// that made up each of the six tetrahedrons within the cube.
        /// tetrahedronsInACube[6][4]
        /// </summary>
        public static readonly int[,] TetrahedronsInACube = {
	        {0,5,1,6},
	        {0,1,2,6},
	        {0,2,3,6},
	        {0,3,7,6},
	        {0,7,4,6},
	        {0,4,5,6}
	    };

        /// <summary>
        /// For any edge, if one vertex is inside of the surface and the other is outside of 
        /// the surface then the edge intersects the surface
        /// For each of the 4 vertices of the tetrahedron can be two possible states, 
        /// either inside or outside of the surface
        /// For any tetrahedron the are 2^4=16 possible sets of vertex states.
        /// This table lists the edges intersected by the surface for all 16 possible vertex states.
        /// There are 6 edges.  For each entry in the table, if edge #n is intersected, then bit #n is set to 1.
        /// tetrahedronEdgeFlags[16]
        /// </summary>
        public static readonly int[] TetrahedronEdgeFlags = {
		    0x00, 0x0d, 0x13, 0x1e, 0x26, 0x2b, 0x35, 0x38, 0x38, 0x35, 0x2b, 0x26, 0x1e, 0x13, 0x0d, 0x00
	    };

        /// <summary>
        /// For each of the possible vertex states listed in tetrahedronEdgeFlags there
        /// is a specific triangulation of the edge intersection points.  
        /// TetrahedronTriangles lists all of them in the form of 0-2 edge triples 
        /// with the list terminated by the invalid value -1.
        /// tetrahedronTriangles[16][7]
        /// </summary>
        public static readonly int[,] TetrahedronTriangles = {
            {-1, -1, -1, -1, -1, -1, -1},
            { 0,  3,  2, -1, -1, -1, -1},
            { 0,  1,  4, -1, -1, -1, -1},
            { 1,  4,  2,  2,  4,  3, -1},

            { 1,  2,  5, -1, -1, -1, -1},
            { 0,  3,  5,  0,  5,  1, -1},
            { 0,  2,  5,  0,  5,  4, -1},
            { 5,  4,  3, -1, -1, -1, -1},

            { 3,  4,  5, -1, -1, -1, -1},
            { 4,  5,  0,  5,  2,  0, -1},
            { 1,  5,  0,  5,  3,  0, -1},
            { 5,  2,  1, -1, -1, -1, -1},

            { 3,  4,  2,  2,  4,  1, -1},
            { 4,  1,  0, -1, -1, -1, -1},
            { 2,  3,  0, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1}
	    };

        public void Generate(float[,,] voxels, IList<Vector3> verts)
        {
            var width = voxels.GetLength(0);
            var height = voxels.GetLength(1);
            var depth = voxels.GetLength(2);
            
            for (var x = 0; x < width - 1; x++)
            {
                for (var y = 0; y < height - 1; y++)
                {
                    for (var z = 0; z < depth - 1; z++)
                    {
                        // Get the values in the 8 neighbours which make up a cube
                        for (var i = 0; i < 8; i++)
                        {
                            var ix = x + VertexOffset[i, 0];
                            var iy = y + VertexOffset[i, 1];
                            var iz = z + VertexOffset[i, 2];
                            Cube[i] = voxels[ix, iy, iz];
                        }
                        
                        March(x, y, z, Cube, verts);
                    }
                }
            }
        }

        public void Generate(VoxelData<float> voxelData, IList<Vector3> verts)
        {
            var width = voxelData.GridWidth;
            var height = voxelData.GridHeight;
            var depth = voxelData.GridDepth;

            for (var x = 0; x < width - 1; x++)
            {
                for (var y = 0; y < height - 1; y++)
                {
                    for (var z = 0; z < depth - 1; z++)
                    {
                        // Get the values in the 8 neighbours which make up a cube
                        for (var i = 0; i < 8; i++)
                        {
                            var ix = x + VertexOffset[i, 0];
                            var iy = y + VertexOffset[i, 1];
                            var iz = z + VertexOffset[i, 2];
                            Cube[i] = voxelData.GetValue(new int3(ix, iy, iz));
                        }

                        March(x, y, z, Cube, verts);
                    }
                }
            }
        }

        public void Generate(IReadOnlyList<float> voxels, int width, int height, int depth, IList<Vector3> verts)
        {
            for (var x = 0; x < width - 1; x++)
            {
                for (var y = 0; y < height - 1; y++)
                {
                    for (var z = 0; z < depth - 1; z++)
                    {
                        //Get the values in the 8 neighbours which make up a cube
                        for (var i = 0; i < 8; i++)
                        {
                            var ix = x + VertexOffset[i, 0];
                            var iy = y + VertexOffset[i, 1];
                            var iz = z + VertexOffset[i, 2];
                            Cube[i] = voxels[ix + iy * width + iz * width * height];
                        }

                        March(x, y, z, Cube, verts);
                    }
                }
            }
        }

        /// <summary>
        /// GetOffset finds the approximate point of intersection of the surface
        /// between two points with the values v1 and v2
        /// </summary>
        public float GetOffset(float v1, float v2)
        {
            var delta = v2 - v1;
            return (delta == 0.0f) ? 0 : (-v1) / delta;
        }

        /// <summary>
        /// VertexOffset lists the positions, relative to vertex0, 
        /// of each of the 8 vertices of a cube.
        /// vertexOffset[8][3]
        /// </summary>
        public static readonly int[,] VertexOffset =
        {
            {0, 0, 0},{1, 0, 0},{1, 1, 0},{0, 1, 0},
            {0, 0, 1},{1, 0, 1},{1, 1, 1},{0, 1, 1}
        };
    }
}
