using System;
using Ara3D.Mathematics;

namespace Ara3D.Geometry
{
    public class ParametricSurface : 
        Procedural<Vector2, Vector3>, 
        IParametricSurface,
        IDeformable<ParametricSurface>
    {
        public bool ClosedX { get; }
        public bool ClosedY { get; }

        public ParametricSurface(Func<Vector2, Vector3> func, bool closedX, bool closedY)
            : base(func) => (ClosedX, ClosedY) = (closedX, closedY);

        public IParametricSurface TransformInput(Func<Vector2, Vector2> f)
            => new ParametricSurface(x => Eval(f(x)), ClosedX, ClosedY);

        IGeometry IDeformable<IGeometry>.Deform(Func<Vector3, Vector3> f)
            => ((IDeformable<ParametricSurface>)this).Deform(f);

        ParametricSurface IDeformable<ParametricSurface>.Deform(Func<Vector3, Vector3> f)
            => new ParametricSurface(uv => f(Eval(uv)), ClosedX, ClosedY);

        IGeometry ITransformable<IGeometry>.Transform(Matrix4x4 mat)
            => ((IDeformable<ParametricSurface>)this).Deform(p => p.Transform(mat));

        ParametricSurface ITransformable<ParametricSurface>.Transform(Matrix4x4 mat)
            => ((IDeformable<ParametricSurface>)this).Deform(p => p.Transform(mat));
    }
}