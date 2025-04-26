using System.Runtime.InteropServices;
using Plato.SinglePrecision;

namespace Plato.Geometry.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RenderVertex
    {
        public RenderVertex(Vector3 position) 
            : this(position, Vector3.Default.Zero, Vector2.Default.Zero, Color32.Default)
        { }

        public RenderVertex(Vector3 position, Vector3 normal) 
            : this(position, normal, Vector2.Default.Zero, Color32.Default)
        { }

        public RenderVertex(Vector3 position, Vector3 normal, Vector2 uv) 
            : this(position, normal, uv, Color32.Default)
        { }

        public RenderVertex(Vector3 position, Color color) 
            : this(position, Vector3.Default.Zero, Vector2.Default.Zero, color)
        { }

        public RenderVertex(Vector3 position, Vector3 normal, Vector2 uv, Color color)
        {
            PX = (float)position.X;
            PY = (float)position.Y;
            PZ = (float)position.Z;
            NX = (float)normal.X;
            NY = (float)normal.Y;
            NZ = (float)normal.Z;
            U = (float)uv.X;
            V = (float)uv.Y;
            RGBA = color;
        }

        public float PX, PY, PZ; // Position = 12 bytes
        public float NX, NY, NZ; // Normal = 6 bytes
        public float U, V; // UV = 4 bytes
        public Color32 RGBA; // Colors = 4 bytes

        public static implicit operator RenderVertex(Vector3 v) => new RenderVertex(v);
        public static implicit operator RenderVertex((Vector3 v, Vector3 n) args) => new RenderVertex(args.v, args.n);
        public static implicit operator RenderVertex((Vector3 v, Vector3 n, Vector2 uv) args) => new RenderVertex(args.v, args.n, args.uv);
        public static implicit operator RenderVertex((Vector3 v, Vector3 n, Vector2 uv, Color c) args) => new RenderVertex(args.v, args.n, args.uv, args.c);
        public static implicit operator RenderVertex((Vector3 v, Color c) args) => new RenderVertex(args.v, args.c);

        public Vector3 Position => (PX, PY, PZ);
        public Vector3 Normal => (NX, NY, NZ);
        public Vector2 UV => (U, V);
        public Color Color => RGBA;
    }
}