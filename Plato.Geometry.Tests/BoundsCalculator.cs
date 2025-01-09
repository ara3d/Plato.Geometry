using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Ara3D.Utils;

namespace Plato.Geometry.Tests
{
    public static unsafe class BoundsCalculator
    {
        public static (Vector3, Vector3) ComputeBoundsLINQ(Vector3[] points)
            => points == null || points.Length == 0
                ? throw new ArgumentException("Points array cannot be null or empty.")
                : points.Aggregate(
                    (points[0], points[0]),
                    (acc, point) => (Vector3.Min(acc.Item1, point), Vector3.Max(acc.Item2, point)
                        ));

        public static (Vector3, Vector3) ComputeBoundsForEach(Vector3[] points)
        {
            var min = new Vector3(float.MaxValue);
            var max = new Vector3(float.MinValue);
            foreach (var p in points)
            {
                min = Vector3.Min(min, p);
                max = Vector3.Max(max, p);
            }

            return (min, max);
        }

        public static (Vector3, Vector3) ComputeBoundsForLoop(Vector3[] points)
        {
            var min = new Vector3(float.MaxValue);
            var max = new Vector3(float.MinValue);
            for (var i=0; i < points.Length; i++)
            {
                var p = points[i];
                min = Vector3.Min(min, p);
                max = Vector3.Max(max, p);
            }
            return (min, max);
        }

        public static (Vector3, Vector3) ComputeBoundsForLoopUnsafe(Vector3[] points)
        {
            var min = new Vector3(float.MaxValue);
            var max = new Vector3(float.MinValue);
            var n = points.Length;
            fixed (Vector3* ptr = points)
            {
                for (var i = 0; i < n; i++)
                {
                    var p = ptr[i];
                    min = Vector3.Min(min, p);
                    max = Vector3.Max(max, p);
                }
            }
            return (min, max);
        }

        public static (Vector3, Vector3) ComputeBoundsSIMD(Vector3[] points)
        {
            fixed (Vector3* ptr = points)
            {
                ComputeBounds(ptr, points.Length, out var min, out var max);
                return (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ComputeBounds(Vector3* points, int count, out Vector3 min, out Vector3 max)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero.");
            if (points == null)
                throw new ArgumentNullException(nameof(points));
            if (Avx.IsSupported)
            {
                ComputeBoundsAvx(points, count, out min, out max);
            }
            else
            {
                // Fallback to scalar implementation
                ComputeBoundsScalar(points, count, out min, out max);
            }
        }

        private static void ComputeBoundsAvx(Vector3* points, int count, out Vector3 min, out Vector3 max)
        {
            float* ptr = (float*)points;
            int total = count * 3;

            // Initialize min and max vectors with the first point
            Vector256<float> minVec = Vector256.Create(ptr[0], ptr[1], ptr[2], ptr[0], ptr[1], ptr[2], ptr[0], ptr[1]);
            Vector256<float> maxVec = minVec;

            int i = 3;
            for (; i <= total - 24; i += 24)
            {
                // Load and de-interleave 8 Vector3s (24 floats)
                Vector256<float> xVec, yVec, zVec;
                LoadDeInterleaveAvx(ptr + i, out xVec, out yVec, out zVec);

                // Update min and max for x components
                minVec = Avx.Min(minVec, xVec);
                maxVec = Avx.Max(maxVec, xVec);

                // Update min and max for y components
                minVec = Avx.Min(minVec, yVec);
                maxVec = Avx.Max(maxVec, yVec);

                // Update min and max for z components
                minVec = Avx.Min(minVec, zVec);
                maxVec = Avx.Max(maxVec, zVec);
            }

            // Handle remaining elements
            for (; i < total; i += 3)
            {
                float x = ptr[i];
                float y = ptr[i + 1];
                float z = ptr[i + 2];

                minVec = Avx.Min(minVec, Vector256.Create(x, y, z, x, y, z, x, y));
                maxVec = Avx.Max(maxVec, Vector256.Create(x, y, z, x, y, z, x, y));
            }

            // Reduce min and max vectors to scalar values
            min = new Vector3(
                ReduceMinAvx(minVec.GetLower()),
                ReduceMinAvx(minVec.GetUpper()),
                ReduceMinAvx(Avx.Blend(minVec.GetLower(), minVec.GetUpper(), 0b00110011))
            );

            max = new Vector3(
                ReduceMaxAvx(maxVec.GetLower()),
                ReduceMaxAvx(maxVec.GetUpper()),
                ReduceMaxAvx(Avx.Blend(maxVec.GetLower(), maxVec.GetUpper(), 0b00110011))
            );
        }

        private static void LoadDeInterleaveAvx(float* ptr, out Vector256<float> x, out Vector256<float> y, out Vector256<float> z)
        {
            // Load 24 floats (8 Vector3s)
            Vector256<float> v0 = Avx.LoadVector256(ptr);       // [x0 y0 z0 x1 y1 z1 x2 y2]
            Vector256<float> v1 = Avx.LoadVector256(ptr + 8);   // [z2 x3 y3 z3 x4 y4 z4 x5]
            Vector256<float> v2 = Avx.LoadVector256(ptr + 16);  // [y5 z5 x6 y6 z6 x7 y7 z7]

            // De-interleave x, y, z components
            // Note: This is a simplified example; actual implementation would need careful shuffling
            x = Avx.Shuffle(v0, v1, 0b11011000); // Extract x components
            y = Avx.Shuffle(v0, v1, 0b11011001); // Extract y components
            z = Avx.Shuffle(v0, v1, 0b11011010); // Extract z components
        }

        private static float ReduceMinAvx(Vector128<float> vec)
        {
            vec = Sse.Min(vec, Sse.Shuffle(vec, vec, 0b1110));
            vec = Sse.Min(vec, Sse.Shuffle(vec, vec, 0b0011));
            return vec.ToScalar();
        }

        private static float ReduceMaxAvx(Vector128<float> vec)
        {
            vec = Sse.Max(vec, Sse.Shuffle(vec, vec, 0b1110));
            vec = Sse.Max(vec, Sse.Shuffle(vec, vec, 0b0011));
            return vec.ToScalar();
        }

        private static (Vector3, Vector3) ComputeBoundsScalar(Vector3[] points)
        {
            fixed (Vector3* ptr = points)
            {
                ComputeBoundsScalar(ptr, points.Length, out var min, out var max);
                return (min, max);
            }
        } 

        private static void ComputeBoundsScalar(Vector3* points, int count, out Vector3 min, out Vector3 max)
        {
            float* ptr = (float*)points;
            int total = count * 3;

            float minX = ptr[0];
            float minY = ptr[1];
            float minZ = ptr[2];
            float maxX = minX;
            float maxY = minY;
            float maxZ = minZ;

            for (int i = 3; i < total; i += 3)
            {
                float x = ptr[i];
                float y = ptr[i + 1];
                float z = ptr[i + 2];

                minX = x < minX ? x : minX;
                minY = y < minY ? y : minY;
                minZ = z < minZ ? z : minZ;

                maxX = x > maxX ? x : maxX;
                maxY = y > maxY ? y : maxY;
                maxZ = z > maxZ ? z : maxZ;
            }

            min = new Vector3(minX, minY, minZ);
            max = new Vector3(maxX, maxY, maxZ);
        }

        public static Vector3[] Points = GenerateRandomPoints(1000 * 1000 * 100);
        
        public static Vector3[] GenerateRandomPoints(int count)
        {
            var points = new Vector3[count];
            var rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                points[i] = new Vector3((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble());
            }
            return points;
        }

        [Test]
        public static void TestSimple()
        {
            for (var i = 0; i < 3; i++)
            {
                //TimingUtils.TimeIt(() => { Console.WriteLine(ComputeBoundsLINQ(Points)); }, "LINQ");
                TimingUtils.TimeIt(() => { Console.WriteLine(ComputeBoundsForLoop(Points)); }, "For");
                TimingUtils.TimeIt(() => { Console.WriteLine(ComputeBoundsForEach(Points)); }, "ForEach");
                TimingUtils.TimeIt(() => { Console.WriteLine(ComputeBoundsForLoopUnsafe(Points)); }, "Unsafe");
                TimingUtils.TimeIt(() => { Console.WriteLine(ComputeBoundsScalar(Points)); }, "Scalar");
                TimingUtils.TimeIt(() => { Console.WriteLine(ComputeBoundsSIMD(Points)); }, "SIMD");
            }
        }
    }
}