using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Ara3D.Utils;

namespace Plato.Geometry.Tests
{
    public static class PerformanceTests
    {
        // --------------------------------------------------------------
        // Helper / Random generation methods
        // --------------------------------------------------------------

        public const int DefaultMaxCount = 1_000_000;
        public const int NumIterations = 10;

        // --------------------------------------------------------------
        // The RunTest utility (given)
        // --------------------------------------------------------------

        public static void RunTest<T, TR>(int maxCount, T[] input, Func<T[], TR[]> func,
            [CallerMemberName] string name = "")
        {
            for (var i = 10000; i <= maxCount; i *= 10)
            {
                try
                {
                    var localInput = input.Take(i).ToArray();
                    var sw = Stopwatch.StartNew();
                    for (var j = 0; j < 10; j++)
                    {
                        var r = func(localInput);
                    }

                    sw.OutputTimeElapsed($"{name} with {i:N} iterations");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error occurred {e.Message}");
                }
            }
        }

        [Test]
        public static void ComputeSimdNormals()
        {
            RunTest(DefaultMaxCount, RandomInputs.SimdTriangles,
                tris =>
                {
                    // Each set of 3 points -> one normal
                    var normals = new SimdVector3[tris.Length];
                    for (int i = 0; i < tris.Length; i += 3)
                    {
                        normals[i] = tris[i].Normal();
                    }

                    return normals;
                });
        }

        [Test]
        public static void ComputeNormals()
        {
            RunTest(DefaultMaxCount, RandomInputs.Triangles,
                tris =>
                {
                    // Each set of 3 points -> one normal
                    var normals = new Vector3[tris.Length];
                    for (int i = 0; i < tris.Length; i += 3)
                    {
                        normals[i] = tris[i].Normal();
                    }

                    return normals;
                });
        }
    }
}
