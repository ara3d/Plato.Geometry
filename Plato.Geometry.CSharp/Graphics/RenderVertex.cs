using System.Runtime.InteropServices;
using Plato.SinglePrecision;

namespace Plato.Geometry.Graphics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RenderVertex
    {
        public RenderVertex(Vector3D position) 
            : this(position, Vector3D.Default.Zero, Vector2D.Default.Zero, Color32.Default)
        { }

        public RenderVertex(Vector3D position, Vector3D normal) 
            : this(position, normal, Vector2D.Default.Zero, Color32.Default)
        { }

        public RenderVertex(Vector3D position, Vector3D normal, Vector2D uv) 
            : this(position, normal, uv, Color32.Default)
        { }

        public RenderVertex(Vector3D position, Color color) 
            : this(position, Vector3D.Default.Zero, Vector2D.Default.Zero, color)
        { }

        public RenderVertex(Vector3D position, Vector3D normal, Vector2D uv, Color color)
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

        public static implicit operator RenderVertex(Vector3D v) => new RenderVertex(v);
        public static implicit operator RenderVertex((Vector3D v, Vector3D n) args) => new RenderVertex(args.v, args.n);
        public static implicit operator RenderVertex((Vector3D v, Vector3D n, Vector2D uv) args) => new RenderVertex(args.v, args.n, args.uv);
        public static implicit operator RenderVertex((Vector3D v, Vector3D n, Vector2D uv, Color c) args) => new RenderVertex(args.v, args.n, args.uv, args.c);
        public static implicit operator RenderVertex((Vector3D v, Color c) args) => new RenderVertex(args.v, args.c);

        public Vector3D Position => (PX, PY, PZ);
        public Vector3D Normal => (NX, NY, NZ);
        public Vector2D UV => (U, V);
        public Color Color => RGBA;
    }
}