using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarchingCubesProject
{
    /// <summary>
    /// A helper class to hold voxel data.
    /// </summary>
    public class VoxelArray
    {
        /// <summary>
        /// Create a new voxel array.
        /// </summary>
        /// <param name="width">The size of the voxels on the x axis.</param>
        /// <param name="height">The size of the voxels on the y axis.</param>
        /// <param name="depth">The size of the voxels on the z axis.</param>
        public VoxelArray(int width, int height, int depth)
        {
            Voxels = new float[width, height, depth];
            FlipNormals = true;
        }

        /// <summary>
        /// The size of the voxels on the x axis.
        /// </summary>
        public int Width => Voxels.GetLength(0);

        /// <summary>
        /// The size of the voxels on the y axis.
        /// </summary>
        public int Height => Voxels.GetLength(1);

        /// <summary>
        /// The size of the voxels on the z axis.
        /// </summary>
        public int Depth => Voxels.GetLength(2);

        /// <summary>
        /// 
        /// </summary>
        public bool FlipNormals { get; set; }

        /// <summary>
        /// Get/set the voxel data.
        /// </summary>
        /// <param name="x">The index on the x axis.</param>
        /// <param name="y">The index on the y axis.</param>
        /// <param name="z">The index on the z axis.</param>
        /// <returns>The voxels data.</returns>
        public float this[int x, int y, int z]
        {
            get { return Voxels[x, y, z]; }
            set { Voxels[x, y, z] = value; }
        }

        /// <summary>
        /// THe voxel data.
        /// </summary>
        public float[,,] Voxels { get; private set; }

        /// <summary>
        /// Get the voxel data at clamped Indices x,y,z.
        /// </summary>
        /// <param name="x">The index on the x axis.</param>
        /// <param name="y">The index on the y axis.</param>
        /// <param name="z">The index on the z axis.</param>
        /// <returns>The voxels data.</returns>
        public float GetVoxel(int x, int y, int z)
        {
            x = Mathf.Clamp(x, 0, Width - 1);
            y = Mathf.Clamp(y, 0, Height - 1);
            z = Mathf.Clamp(z, 0, Depth - 1);
            return Voxels[x, y, z];
        }

        /// <summary>
        /// Get the voxel data at normalized (0-1) clamped Indices u,v,w.
        /// </summary>
        /// <param name="u">The normalized index (0-1) on the x axis.</param>
        /// <param name="v">The normalized index (0-1) on the y axis.</param>
        /// <param name="w">The normalized index (0-1) on the z axis.</param>
        /// <returns>The voxel data</returns>
        public float GetVoxel(float u, float v, float w)
        {
            var x = u * (Width - 1);
            var y = v * (Height - 1);
            var z = w * (Depth - 1);

            var xi = (int)Mathf.Floor(x);
            var yi = (int)Mathf.Floor(y);
            var zi = (int)Mathf.Floor(z);

            var v000 = GetVoxel(xi, yi, zi);
            var v100 = GetVoxel(xi + 1, yi, zi);
            var v010 = GetVoxel(xi, yi + 1, zi);
            var v110 = GetVoxel(xi + 1, yi + 1, zi);

            var v001 = GetVoxel(xi, yi, zi + 1);
            var v101 = GetVoxel(xi + 1, yi, zi + 1);
            var v011 = GetVoxel(xi, yi + 1, zi + 1);
            var v111 = GetVoxel(xi + 1, yi + 1, zi + 1);

            var tx = Mathf.Clamp01(x - xi);
            var ty = Mathf.Clamp01(y - yi);
            var tz = Mathf.Clamp01(z - zi);

            //use bilinear interpolation the find these values.
            var v0 = BLerp(v000, v100, v010, v110, tx, ty);
            var v1 = BLerp(v001, v101, v011, v111, tx, ty);

            //Now lerp those values for the final trilinear interpolation.
            return Lerp(v0, v1, tz);
        }

        /// <summary>
        /// Get the voxels normal at the Indices x,y,z.
        /// The normal will be all zeros in any areas of the voxels that are constant.
        /// </summary>
        /// <param name="x">The index on the x axis.</param>
        /// <param name="y">The index on the y axis.</param>
        /// <param name="z">The index on the z axis.</param>
        /// <returns></returns>
        public Vector3 GetNormal(int x, int y, int z)
        {
            var n = GetFirstDerivative(x, y, z);

            if (FlipNormals)
                return n.normalized * -1;
            else
                return n.normalized;
        }

        /// <summary>
        /// Get the voxels normal at the normalized Indices u,v,w.
        /// The normal will be all zeros in any areas of the voxels that are constant.
        /// </summary>
        /// <param name="u">The normalized index (0-1) on the x axis.</param>
        /// <param name="v">The normalized index (0-1 on the y axis.</param>
        /// <param name="w">The normalized index (0-1 on the z axis.</param>
        /// <returns></returns>
        public Vector3 GetNormal(float u, float v, float w)
        {
            var n = GetFirstDerivative(u, v, w);

            if (FlipNormals)
                return n.normalized * -1;
            else
                return n.normalized;
        }

        /// <summary>
        /// Get the voxels first derivative at the Indices x,y,z.
        /// The derivative will be all zeros in any areas of the voxels that are constant.
        /// The derivative is calculated using back and forward finite differance.
        /// </summary>
        /// <param name="x">The index on the x axis.</param>
        /// <param name="y">The index on the y axis.</param>
        /// <param name="z">The index on the z axis.</param>
        /// <returns></returns>
        public Vector3 GetFirstDerivative(int x, int y, int z)
        {
            var dx_p1 = GetVoxel(x + 1, y, z);
            var dy_p1 = GetVoxel(x, y + 1, z);
            var dz_p1 = GetVoxel(x, y, z + 1);

            var dx_m1 = GetVoxel(x - 1, y, z);
            var dy_m1 = GetVoxel(x, y - 1, z);
            var dz_m1 = GetVoxel(x, y, z - 1);

            var dx = (dx_p1 - dx_m1) * 0.5f;
            var dy = (dy_p1 - dy_m1) * 0.5f;
            var dz = (dz_p1 - dz_m1) * 0.5f;

            return new Vector3(dx, dy, dz);
        }

        /// <summary>
        /// Get the voxels first derivative at the normalized Indices u,v,w.
        /// The first derivative will be all zeros in any areas of the voxels that are constant.
        /// The derivative is calculated using back and forward finite differance.
        /// </summary>
        /// <param name="u">The normalized index (0-1) on the x axis.</param>
        /// <param name="v">The normalized index (0-1 on the y axis.</param>
        /// <param name="w">The normalized index (0-1 on the z axis.</param>
        /// <returns></returns>
        public Vector3 GetFirstDerivative(float u, float v, float w)
        {
            const float h = 0.005f;
            const float hh = h * 0.5f;
            const float ih = 1.0f / h;

            var dx_p1 = GetVoxel(u + hh, v, w);
            var dy_p1 = GetVoxel(u, v + hh, w);
            var dz_p1 = GetVoxel(u, v, w + hh);

            var dx_m1 = GetVoxel(u - hh, v, w);
            var dy_m1 = GetVoxel(u, v - hh, w);
            var dz_m1 = GetVoxel(u, v, w - hh);

            var dx = (dx_p1 - dx_m1) * ih;
            var dy = (dy_p1 - dy_m1) * ih;
            var dz = (dz_p1 - dz_m1) * ih;

            return new Vector3(dx, dy, dz);
        }

        /// <summary>
        /// Linear interpolation.
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static float Lerp(float v0, float v1, float t)
        {
            return v0 + (v1 - v0) * t;
        }

        /// <summary>
        /// Bilinear interpolation.
        /// </summary>
        /// <param name="v00"></param>
        /// <param name="v10"></param>
        /// <param name="v01"></param>
        /// <param name="v11"></param>
        /// <param name="tx"></param>
        /// <param name="ty"></param>
        /// <returns></returns>
        private static float BLerp(float v00, float v10, float v01, float v11, float tx, float ty)
        {
            return Lerp(Lerp(v00, v10, tx), Lerp(v01, v11, tx), ty);
        }

    }


}
