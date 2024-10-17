using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Plato.DoublePrecision
{
    public class PolyLine2D : IPolyLine2D, IArray<Vector2D>, IArray<Vector3D>
    {
        public IArray<Vector2D> Points { get; }
        public Boolean Closed { get; }

        public PolyLine2D(IArray<Vector2D> points, Boolean closed)
        {
            Points = points;
            Closed = closed;
        }

        public IArray<Line2D> ToLines()
        {
            var n = Closed ? Points.Count : Points.Count - 1;
            return n.MapRange(i => new Line2D(Points[i], Points.ModuloAt(i + 1)));
        }

        public PolyLine3D To3D
            => Points.Map(p => p.To3D).ToPolyLine3D(Closed);

        public static implicit operator PolyLine3D(PolyLine2D lines)
            => lines.To3D;

        IEnumerator<Vector3D> IEnumerable<Vector3D>.GetEnumerator()
        {
            for (var i = 0; i < Points.Count; i++) yield return this[i];
        }

        public IEnumerator<Vector2D> GetEnumerator() => Points.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Points.GetEnumerator();
        int IReadOnlyCollection<Vector2D>.Count => Points.Count;
        Integer IArray<Vector3D>.Count => Points.Count;
        public Vector2D At(Integer n) => Points[n];
        Vector3D IArray<Vector3D>.this[Integer n] => Points[n];
        Vector3D IArray<Vector3D>.At(Integer n) => Points[n];
        public Vector2D this[Integer n] => Points[n];
        public Vector2D this[int index] => Points[index];
        Integer IArray<Vector2D>.Count => Points.Count;
        int IReadOnlyCollection<Vector3D>.Count => Points.Count;
        Vector3D IReadOnlyList<Vector3D>.this[int index] => Points[index];
    }
}