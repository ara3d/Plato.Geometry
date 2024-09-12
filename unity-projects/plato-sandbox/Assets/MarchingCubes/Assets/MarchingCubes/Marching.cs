using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MarchingCubesProject
{
    public abstract class Marching
    {
        /// <summary>
        /// The surface value in the voxels. Normally set to 0. 
        /// </summary>
        public float Surface { get; set; }
        private float[] Cube { get; set; }

        public Marching(float surface)
        {
            Surface = surface;
            Cube = new float[8];
        }

        public void Generate(float[,,] voxels, IList<Vector3> verts)
        {
            var width = voxels.GetLength(0);
            var height = voxels.GetLength(1);
            var depth = voxels.GetLength(2);

            int x, y, z, i;
            int ix, iy, iz;
            for (x = 0; x < width - 1; x++)
            {
                for (y = 0; y < height - 1; y++)
                {
                    for (z = 0; z < depth - 1; z++)
                    {
                        //Get the values in the 8 neighbours which make up a cube
                        for (i = 0; i < 8; i++)
                        {
                            ix = x + VertexOffset[i, 0];
                            iy = y + VertexOffset[i, 1];
                            iz = z + VertexOffset[i, 2];

                            Cube[i] = voxels[ix, iy, iz];
                        }

                        //Perform algorithm
                        March(x, y, z, Cube, verts);
                    }
                }
            }

        }

       
        public void Generate(IReadOnlyList<float> voxels, int width, int height, int depth, IList<Vector3> verts)
        {
            int x, y, z, i;
            int ix, iy, iz;
            for (x = 0; x < width - 1; x++)
            {
                for (y = 0; y < height - 1; y++)
                {
                    for (z = 0; z < depth - 1; z++)
                    {
                        //Get the values in the 8 neighbours which make up a cube
                        for (i = 0; i < 8; i++)
                        {
                            ix = x + VertexOffset[i, 0];
                            iy = y + VertexOffset[i, 1];
                            iz = z + VertexOffset[i, 2];

                            Cube[i] = voxels[ix + iy * width + iz * width * height];
                        }

                        //Perform algorithm
                        March(x, y, z, Cube, verts);
                    }
                }
            }
        }

        /// <summary>
        /// MarchCube performs the Marching algorithm on a single cube
        /// </summary>
        protected abstract void March(float x, float y, float z, float[] cube, IList<Vector3> vertList);

        /// <summary>
        /// GetOffset finds the approximate point of intersection of the surface
        /// between two points with the values v1 and v2
        /// </summary>
        protected float GetOffset(float v1, float v2)
        {
            var delta = v2 - v1;
            return (delta == 0.0f) ? Surface : (Surface - v1) / delta;
        }

        /// <summary>
        /// VertexOffset lists the positions, relative to vertex0, 
        /// of each of the 8 vertices of a cube.
        /// vertexOffset[8][3]
        /// </summary>
        protected static readonly int[,] VertexOffset =
	    {
	        {0, 0, 0},{1, 0, 0},{1, 1, 0},{0, 1, 0},
	        {0, 0, 1},{1, 0, 1},{1, 1, 1},{0, 1, 1}
	    };

    }

}
