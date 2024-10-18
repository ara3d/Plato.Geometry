using System;

namespace Plato.DoublePrecision
{
    public partial struct Quad3D : IDeformable3D<Quad3D>
    {
        public Quad3D Deform(Func<Vector3D, Vector3D> f)
            => (f(A), f(B), f(C), f(D));

        public Quad3D Transform(Matrix4x4 matrix)
            => Deform(matrix.TransformPoint);

        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);

        ITransformable3D ITransformable3D.Transform(Matrix4x4 matrix)
            => Transform(matrix);

        public static implicit operator PolyLine3D(Quad3D q)
            => q.ToPolyLine3D(true);

        public static implicit operator PointArray(Quad3D q)
            => q.ToPoints();

        public static implicit operator QuadMesh(Quad3D q)
            => q.ToQuadMesh();

        public static implicit operator TriangleMesh(Quad3D q)
            => q.ToQuadMesh().TriangleMesh;

        public Line3D Bottom 
            => (A, B);
        
        public Line3D Right 
            => (B, C);
        
        public Line3D Top 
            => (C, D);
        
        public Line3D Left 
            => (D, A);

        public static implicit operator QuadGrid(Quad3D q) 
            => q.Bottom.QuadStrip(q.Top.Reverse, false);

        public Quad3D Reverse
            => (D, C, B, A);
    }
}