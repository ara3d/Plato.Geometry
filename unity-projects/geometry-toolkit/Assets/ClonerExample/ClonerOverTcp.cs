using System.Net.Sockets;
using System.Net;
using System.Text;
using Unity.Jobs;
using UnityEngine;
using System.Runtime.InteropServices;
using Unity.Mathematics;

namespace Assets.ClonerExample
{
    [ExecuteAlways]
    public class ClonerOverTcp : ClonerJobComponent, ICloneJob
    {
        [StructLayout(LayoutKind.Sequential, Size = 32, Pack = 1)]
        public struct RigidPose
        {
            public Quaternion Orientation;
            public Vector3 Position;
        }

        public Color Color = Color.blue;
        public Quaternion Rotation = Quaternion.identity;
        public Vector3 Scale = Vector3.one;
        [Range(0, 1)] public float Metallic = 0.5f;
        [Range(0, 1)] public float Smoothness = 0.5f;

        public int BytesRecieved;
        private CloneData _cloneData;
        private byte[] _buffer = new byte[17000 * 32];

        public override (CloneData, JobHandle) Schedule(CloneData previousData, JobHandle previousHandle, int batchSize)
        {
            throw new System.NotImplementedException();
        }

        public unsafe JobHandle Schedule(ICloneJob previous)
        {
            _cloneData.Resize(16000);
            const int port = 13;
            var ipEndPoint = new IPEndPoint(IPAddress.Loopback, port);

            using TcpClient client = new();
            client.Connect(ipEndPoint);
            using var stream = client.GetStream();
            var msg = "What time is it?";
            var buffer = Encoding.UTF8.GetBytes(msg);
            stream.Write(buffer);
            BytesRecieved = stream.Read(_buffer);
            fixed (byte* p = _buffer)
            {
                var posePtr = (RigidPose*)p;
                for (var i = 0; i < BytesRecieved / 32 && i < 16000; i++)
                {
                    _cloneData.GpuInstance(i) = new GpuInstanceData
                    {
                        Color = Color.ToFloat4(),
                        Id = (uint)i,
                        Metallic = Metallic,
                        Smoothness = Smoothness,
                        Orientation = posePtr->Orientation,
                        Pos = posePtr->Position,
                        Scl = new float3(1,1,1),
                    };
                    posePtr++;
                }
            }

            return Handle = default;
        }

        public JobHandle Handle { get; set;  }

        public ref CloneData CloneData => ref _cloneData;

        public int Count { get; set;  }
    }
}