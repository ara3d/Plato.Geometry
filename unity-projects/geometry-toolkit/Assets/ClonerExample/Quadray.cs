using System;
using System.Collections.Generic;
using System.Linq;
using Ara3D.Mathematics;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

namespace Assets.ClonerExample
{
    public readonly struct Quadray
    {
        public Quadray(float x, float y, float z, float w)
        {
            X = x; Y = y; Z = z; W = w;
        }

        public static implicit operator (float,float,float,float)(Quadray qr)
            => (qr.X, qr.Y, qr.Z, qr.W);

        public static implicit operator Quadray((float, float, float, float) t)
            => new(t.Item1, t.Item2, t.Item3, t.Item4);

        public static implicit operator float[](Quadray qr)
            => new[] { qr.X, qr.Y, qr.Z, qr.W };

        public static implicit operator Quadray(float[] t)
            => new(t[0], t[1], t[2], t[3]);

        public static implicit operator Vector3(Quadray qr)
            => qr.Vector3;

        public static implicit operator Quadray(Vector4 v)
            => (v.x, v.y,v.z, v.w);

        public static implicit operator Vector4(Quadray qr)
            => qr.Vector4;

        public static Quadray operator+(Quadray q1, Quadray q2)
            => q1.Vector4 + q2.Vector4;

        public static Quadray operator -(Quadray q)
            => -q.Vector4;

        public static Quadray operator *(Quadray q, float x)
            => (q.Vector4 * x);
            
        public Vector3 Vector3
            => X * XVector + Y * YVector + Z * ZVector + W * WVector;

        public Vector4 Vector4
            => new(X, Y, Z, W);

        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float W;

        public static float K = 2 * 3f.Sqrt();
        public static readonly Vector3 XVector = new Vector3(1, 1, 1) / K; 
        public static readonly Vector3 YVector = new Vector3(-1, -1, 1) / K;
        public static readonly Vector3 ZVector = new Vector3(-1, 1, -1) / K;
        public static readonly Vector3 WVector = new Vector3(1, -1, -1) / K;

        /*
        public static readonly Vector3 XVector = new(+1, -1 / Mathf.Sqrt(3f), -1 / 6f.Sqrt());
        public static readonly Vector3 YVector = new(-1, -1 / Mathf.Sqrt(3f), -1 / 6f.Sqrt());
        public static readonly Vector3 ZVector = new(0, 2 / Mathf.Sqrt(3f), -1 / 6f.Sqrt());
        public static readonly Vector3 WVector = new(0, 0, 3 / 6f.Sqrt());
        */

        /*
        public static readonly Vector3 XVector = new(+1, -1 / 6f.Sqrt() , - 1 / Mathf.Sqrt(3f));
        public static readonly Vector3 YVector = new(-1, -1 / 6f.Sqrt() , - 1 / Mathf.Sqrt(3));
        public static readonly Vector3 ZVector = new(0, -1 / 6f.Sqrt(), 2 / -Mathf.Sqrt(3f) );
        public static readonly Vector3 WVector = new(0, 3 / 6f.Sqrt(), 0);        
        */

        /*
        public static readonly Vector3 XVector = new(0, Mathf.Sqrt(8 / 9f), -1 / 3f);
        public static readonly Vector3 YVector = new(-Mathf.Sqrt(2 / 3f), -Mathf.Sqrt(8 / 9f), -1 / 3f);
        public static readonly Vector3 ZVector = new(Mathf.Sqrt(2 / 3f), -Mathf.Sqrt(8 / 9f), -1 / 3f);
        public static readonly Vector3 WVector = new(0, 0, 1);
        */

        public static readonly Quadray UnitX = (1, 0, 0, 0);
        public static readonly Quadray UnitY = (0, 1, 0, 0);
        public static readonly Quadray UnitZ = (0, 0, 1, 0);
        public static readonly Quadray UnitW = (0, 0, 0, 1);

        public static readonly Quadray[] Units = { UnitX, UnitY, UnitZ, UnitW };

        public static Quadray Zero = (0, 0, 0, 0);

        public static float x1 = UnitX.Vector3.x;
        public static float x2 = UnitY.Vector3.x;
        public static float x3 = UnitZ.Vector3.x;
        public static float x4 = UnitW.Vector3.x;
        public static float y1 = UnitX.Vector3.y;
        public static float y2 = UnitY.Vector3.y;
        public static float y3 = UnitZ.Vector3.y;
        public static float y4 = UnitW.Vector3.y;
        public static float z1 = UnitX.Vector3.z;
        public static float z2 = UnitY.Vector3.z;
        public static float z3 = UnitZ.Vector3.z;
        public static float z4 = UnitW.Vector3.z;

        public static float3x3 T = math.float3x3(
            x1 - x4, x2 - x4, x3 - x4, 
            y1 - y4, y2 - y4, y3 - y4, 
            z1 - z4, z2 - z4, z3 - z4);
        
        public static float3x3 Ti = math.inverse(T);

        public static Quadray ToQuadray(Vector3 p)
        {
            var r4 = (float3)UnitW.Vector3;
            var r = math.mul(Ti, ((float3)p - r4));
            return (r.x, r.y, r.z, 1 - r.x - r.y - r.z);
        }
    }

    public class Cuboctohedron
    {
        public static Quadray[] Points =
        {
            (0,1,1,2),
            (1,0,1,2),
            (1,1,0,2),
            
            (0,1,2,1),
            (1,0,2,1),
            (1,1,2,0),
            
            (0,2,1,1),
            (1,2,0,1),
            (1,2,1,0),
            
            (2,0,1,1),
            (2,1,0,1),
            (2,1,1,0),
        };
    }

    public static class QuadrayExtensions
    {
        public static Mesh ToTetrahedronMesh(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var r = new Mesh()
            {
                vertices = new[] { p0, p1, p2, p3 },
                triangles = new[]
                {
                    2, 1, 0,
                    0, 1, 3,
                    3, 2, 0,
                    1, 2, 3,
                },
            };
            r.RecalculateNormals();
            return r;
        }

        public static readonly Mesh Tetrahedron
            = ToTetrahedronMesh(Quadray.UnitX, Quadray.UnitY, Quadray.UnitZ, Quadray.UnitW);

        public static IEnumerable<T[]> PermutationsInPlace<T>(this T[] self, int from = 0)
        {
            if (from == self.Length - 1)
                yield return self;
            for (var i = from; i < self.Length; i++)
            {
                (self[from], self[i]) = (self[i], self[from]);
                foreach (var p in self.PermutationsInPlace(from + 1))
                    yield return p;
                (self[from], self[i]) = (self[i], self[from]);
            }
        }

        public static IEnumerable<T[]> Permutations<T>(this IReadOnlyList<T> self)
            => self.ToArray().PermutationsInPlace();

        public static IEnumerable<IReadOnlyList<int>> ChooseFrom(this int k, int n)
        {
            var result = new int[k];
            var stack = new Stack<int>();
            stack.Push(0);

            while (stack.Count > 0)
            {
                var index = stack.Count - 1;
                var value = stack.Pop();

                while (value < n)
                {
                    result[index++] = value++;
                    stack.Push(value);

                    if (index == k)
                    {
                        yield return (int[])result.Clone();
                        break;
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IReadOnlyList<T> self, int size = -1)
        {
            if (size < 0) size = self.Count;
            foreach (var r in size.ChooseFrom(self.Count))
                yield return r.Select(i => self[i]);
        }

    }

    
    public static class RationalTrig
    {
        // https://www.cut-the-knot.org/pythagoras/RationalTrig/CutTheKnot.shtml

        public static float Quadrance(Vector3 a, Vector3 b)
            => (b - a).sqrMagnitude;

        // Note: c is the projection of a on b (how do we get it?)
        // So, in
        public static float SpreadOfRightAngle(Vector3 a, Vector3 b, Vector3 c)
            => Quadrance(b, c) / Quadrance(a, b);

    }

    public class RationalQuaternion
    {
        // https://www.mdpi.com/2075-1702/10/9/749#
        public static RationalQuaternion Identity;

        public static RationalQuaternion Multiply(RationalQuaternion q1, RationalQuaternion q2)
            => throw new NotImplementedException();

        public static RationalQuaternion Negative(RationalQuaternion q)
            => throw new NotImplementedException();

        public static float HalfTurnsToCosTheta(float h)
            => (1 - h * h) / (1 + h * h);

        public static float HalfTurnsToSinTheta(float h)
            => 2 * h / (1 + h * h);

        //public float T;
        //public Vector3 V;
        //public float Quadrance;

        // u 
        public RationalQuaternion(Vector3 rotationalAxis, float halfTurn)
        {
        }

        public RationalQuaternion(Vector4 v)
        {

        }

        // Note: z0, y0 and z1 are "half-turns"
        public static RationalQuaternion FromEulerZYZ(float z0, float y, float z1)
        {
            var t1 = z0;
            var t2 = y;
            var t3 = z1;

            // Formula 16 from 
            // https://www.mdpi.com/2075-1702/10/9/749#app1-machines-10-00749 
            return new RationalQuaternion(
                new Vector4(t2 * (t1 * t3 - 1),
                    t1 - t3,
                    t1 * t3 + 1,
                    t2 * (t1 + t3)));
        }


        public static Vector3 Rotate(float halfTurns, Vector3 axis, Vector3 point)
        {
            // note: e.magnitude is a sqrt, but if we know e is normalized it is 1
            //var t = axis.magnitude / halfTurns;
            var t = 1 / halfTurns;

            var ex = axis.x;
            var ey = axis.y;
            var ez = axis.z;
            var px = point.x;
            var py = point.y;
            var pz = point.z;
            var ex2 = ex * ex;
            var ey2 = ey * ey;
            var ez2 = ez * ez;
            var t2 = t * t;
            var x = px * (t2 + ex2 - ey2 - ez2)
                    + 2 * py * (ex * ey - t * ez) 
                    + 2 * pz * (ex * ez + t * ey);
            var y = py * (t2 - ex2 + ey2 - ez2)
                    + 2 * pz * (ey * ez - t * ex)
                    + 2 * px * (ex * ey + t * ez);
            var z = pz * (t2 - ex2 - ey2 + ez2)
                    + 2 * px * (ex * ez - t * ey)
                    + 2 * py * (ey * ez + t * ex);
            return new(x, y, z);
        }


        // https://stackoverflow.com/questions/7937143/3d-rotation-without-trigonometry
        public static Vector3 Rotate_stackoverflow(float c, Vector3 uvw, Vector3 xyz)
        {
            var x = xyz.x;
            var y = xyz.y;
            var z = xyz.z;
            var u = uvw.x;
            var v = uvw.y;
            var w = uvw.z;
            var v2 = v * v;
            var w2 = w * w;
            var wy = w * y;
            var c2 = c * c;
            var vz = v * z;
            var vy = v * y;
            var wz = w * z;
            var wx = w * x;
            var uv = u * v;
            var cx = c * x;
            var cy = c * y;
            var uz = u * z;
            var vw = v * w;
            var cz = c * z;
            var vx = v * x;
            var uw = u * w;
            var uy = u * y;
            var t = Mathf.Sqrt(1 - c2);
            return new Vector3(x + (-1 + c) * v2 * x + (-1 + c) * w2 * x + t * (-wy + vz) - (-1 + c) * u * (vy + wz),
                t * wx + uv * (x - cx) + cy + v2 * (y - cy) - t * uz + vw * (z - cz),
                -t * vx + uw * (x - cx) + t * uy + vw * (y - cy) + cz  + w2 * (z - cz));
        }

        // https://learn.microsoft.com/en-us/previous-versions/xamarin/essentials/orientation-sensor
        public static Quaternion Create(float halfTurns, Vector3 axis)
        {
            var cosTheta = HalfTurnsToCosTheta(halfTurns);
            var sinTheta = HalfTurnsToSinTheta(halfTurns);
            return new(axis.x * sinTheta, axis.y * sinTheta, axis.z * sinTheta, cosTheta);
        }

        // https://learn.microsoft.com/en-us/previous-versions/xamarin/essentials/orientation-sensor
        public static Quaternion Create2(float halfTurns, Vector3 axis)
        {
            // Using the cofunction formulas
            var cosTheta = HalfTurnsToSinTheta(0.5f + 0.5f - halfTurns);
            var sinTheta = HalfTurnsToCosTheta(0.5f + 0.5f - halfTurns);
            return new(axis.x * sinTheta, axis.y * sinTheta, axis.z * sinTheta, cosTheta);
        }

        public static Quaternion CreateFromFirstPrinciples(float halfTurns, Vector3 axis)
        {
            // Circle:
            // X^2 + Y^2 = 1. 
            // Circle: X = Sin(Theta)
            // Circle: Y = Cos(Theta)

            // So for a circle? 
            // Sin(Theta)^2 + Cos(Theta)^2 = 1 
            // And if that is true:
            // Cos(Theta)^2 = 1 - Sin(Theta)^2
            // Cos(Theta) = Sqrt(1 - Sin(Theta)^2) 

            // Confirmed by 
            // https://en.wikipedia.org/wiki/List_of_trigonometric_identities
            // Cos(Theta) = +/- Sqrt(1 - Sin(Theta)^2)
            // And of course:
            // Sin(Theta) = +/- Sqrt(1 - Cos(Theta)^2)

            // https://www.euclideanspace.com/maths/geometry/rotations/conversions/angleToQuaternion/index.htm
            // We know:
            // Qx = Ax * sin(Theta / 2) 
            // Qy = Ay * sin(Theta / 2) 
            // Qz = Az * sin(Theta / 2) 
            // Qw = cos(Theta / 2)

            // Therefor:
            // Qx = Ax * Sqrt(1 - qW ^ 2) 
            // Qy = Ax * Sqrt(1 - qW ^ 2) 
            // Qz = Ax * Sqrt(1 - qW ^ 2) 
            // Qw = cos(Theta / 2)

            // According to https://www.mdpi.com/2075-1702/10/9/749
            // Cos(Theta) = (1 - h^2) / (1 + h^2) 
            // Sin(theta) = 2 * h / (1 + h^2)
            throw new NotImplementedException();
        }

        /*
         *
         */
    }
}