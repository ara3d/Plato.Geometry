using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerFromMemoryMappedFile: ClonerJobComponent, ICloneJob
    {
        [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 1)]
        public struct RigidPose
        {
            public Quaternion Orientation;
            public Vector3 Position;
        }

        public Color Color = Color.blue;
        public Quaternion Rotation = Quaternion.identity;
        [Range(0, 1)] public float Metallic = 0.5f;
        [Range(0, 1)] public float Smoothness = 0.5f;
        public float Scale = 0.1f;

        public int BytesRecieved;
        private CloneData _cloneData;
        public string FileName = "DemoMappedFile";
        public const int Capacity = 32000;
        public MemoryMappedFile File;
        public MemoryMappedViewAccessor View;


        public void OnDisable()
        {
            View.Dispose();
            View = null;
            File.Dispose();
            File = null;
        }

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            throw new System.NotImplementedException();
        }

        public JobHandle Schedule(ICloneJob previous)
        {
            if (File == null)
                File = MemoryMappedFile.CreateOrOpen(FileName, Capacity * 32);
            if (View == null)
                View = File.CreateViewAccessor();
            if (!View.CanRead)
                return default;

            var n = View.ReadInt32(0);
            _cloneData.Resize(n);

            for (var i=0; i < n; i++)
            {
                View.Read<RigidPose>((i + 1) * 32, out var pose);
                _cloneData.GpuInstance(i) = new GpuInstanceData
                {
                    Color = Color.ToFloat4(),
                    Id = (uint)i,
                    Metallic = Metallic,
                    Smoothness = Smoothness,
                    Orientation = pose.Orientation,
                    Pos = pose.Position * Scale,
                    Scl = new float3(1, 1, 1),
                };
            }

            return Handle = default;
        }

        public JobHandle Handle { get; set; }

        public ref CloneData CloneData => ref _cloneData;

        public int Count => _cloneData.Count;
    }
}