using System;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class RotatePerformanceTest : MonoBehaviour
    {
        public int Iterations;
        public void OnEnable()
        {
        }
    }

    [ExecuteAlways]
    public class UtilRotateTest : MonoBehaviour
    {
        public Vector3 Axis = Vector3.up + Vector3.back;
        public Vector3 NormalizedAxis;
        public Vector3 Point = new Vector3(3, 4, 5);
        public Quaternion Rotation;
        public Quaternion RationalRotation;
        public Quaternion RationalRotation2;

        public Mesh Mesh;
        public Material Material;
        private Vector3 Result1;
        private Vector3 Result2;
        private Vector3 Result3;
        private Vector3 Result4;
        private Vector3 Result5;
        private Vector3 Result6;

        public bool ShowQuaternion;
        public bool ShowRational1;
        public bool ShowRational2;
        public bool ShowRational3;
        public bool ShowRational4;

        [Range(0, 1)] public float HalfTurns = 0.5f;
        public float Radians;
        public float CosTheta;
        public float SinTheta;
        public float CosThetaRational;
        public float SinThetaRational;

        public void Update()
        {
            NormalizedAxis = Vector3.Normalize(Axis);
            Radians = HalfTurns * Mathf.PI;
            Rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Radians, NormalizedAxis);
            RationalRotation = RationalQuaternion.Create(HalfTurns, NormalizedAxis);
            RationalRotation2 = RationalQuaternion.Create2(HalfTurns, NormalizedAxis);
            Result1 = Rotation * Point;
            //Result2 = RationalQuaternion.Rotate(-HalfTurns, NormalizedAxis, Point);
            Result2 = RationalQuaternion.Rotate_stackoverflow(-HalfTurns * 2 + 1, NormalizedAxis, Point);
            Result3 = RationalRotation2 * Point;
            Result4 = RationalRotation * Point;
            Result5 = Vector3.Lerp(Result3, Result4, 0.5f);
            CosTheta = Mathf.Cos(Radians);
            SinTheta = Mathf.Sin(Radians);
            CosThetaRational = RationalQuaternion.HalfTurnsToCosTheta(HalfTurns);
            SinThetaRational = RationalQuaternion.HalfTurnsToSinTheta(HalfTurns);

            var rp = new RenderParams(Material);
            rp.matProps = new MaterialPropertyBlock();
            rp.matProps.SetColor("_Color", Color.blue);
            Graphics.RenderMesh(rp,Mesh, 0, Matrix4x4.Translate(Point));
            rp.matProps.SetColor("_Color", Color.red);
            if (ShowQuaternion)
                Graphics.RenderMesh(rp, Mesh, 0, Matrix4x4.Translate(Result1));
            rp.matProps.SetColor("_Color", Color.green);
            if (ShowRational1)
                Graphics.RenderMesh(rp, Mesh, 0, Matrix4x4.Translate(Result2));
            rp.matProps.SetColor("_Color", Color.cyan);
            if (ShowRational2)
                Graphics.RenderMesh(rp, Mesh, 0, Matrix4x4.Translate(Result3));
            rp.matProps.SetColor("_Color", Color.magenta);
            if (ShowRational3)
                Graphics.RenderMesh(rp, Mesh, 0, Matrix4x4.Translate(Result4));
            rp.matProps.SetColor("_Color", Color.yellow);
            if (ShowRational4)
                Graphics.RenderMesh(rp, Mesh, 0, Matrix4x4.Translate(Result5));

            {
                var minDist = float.MaxValue;
                var maxDist = float.MinValue;
                for (var i = 0; i < 100; i++)
                {
                    var t0 = i / 100f;
                    var t1 = (i + 1) / 100f;
                    var v0 = new Vector2(
                        RationalQuaternion.HalfTurnsToCosTheta(t0),
                        RationalQuaternion.HalfTurnsToSinTheta(t0));
                    var v1 = new Vector2(
                        RationalQuaternion.HalfTurnsToCosTheta(t1),
                        RationalQuaternion.HalfTurnsToSinTheta(t1));
                    var d = (v1 - v0).magnitude;
                    minDist = Math.Min(minDist, d);
                    maxDist = Math.Max(maxDist, d);
                }

                Debug.Log($"Distances min={minDist} max={maxDist}");
            }
            {
                var minDist = float.MaxValue;
                var maxDist = float.MinValue;
                for (var i = 0; i < 100; i++)
                {
                    var t0 = i / 100f;
                    var t1 = (i + 1) / 100f;
                    var v0 = new Vector2(
                        Mathf.Cos(t0 * Mathf.PI),
                        Mathf.Sin(t0 * Mathf.PI));
                    var v1 = new Vector2(
                        Mathf.Cos(t1 * Mathf.PI),
                        Mathf.Sin(t1 * Mathf.PI));
                    var d = (v1 - v0).magnitude;
                    minDist = Math.Min(minDist, d);
                    maxDist = Math.Max(maxDist, d);
                }

                Debug.Log($"Distances min={minDist} max={maxDist}");

            }
        }
    }
}