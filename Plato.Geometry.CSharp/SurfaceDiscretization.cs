using Plato.DoublePrecision;

namespace Plato.Geometry
{
    /*
    /// <summary>
    /// This class is used for sampling parameterized surfaces and computing quad-mesh strip indices.
    /// Note that the members are computed on demand, so be careful about over-computation. 
    /// x == column == u, y == row == v
    /// </summary>
    public class SurfaceDiscretization
    {
        public Array<double> Xs { get; }
        public Array<double> Ys { get; }
        public Array2D<Vector2D> Uvs { get; }
        public bool ClosedX { get; }
        public bool ClosedY { get; }
        public Array2D<Integer4> Indices { get; }

        public SurfaceDiscretization(int nColumns, int nRows, bool closedX, bool closedY)
        {
            ClosedX = closedX;
            ClosedY = closedY;
            
            var nx = nColumns + (closedX ? 0 : 1);
            var ny = nRows + (closedY ? 0 : 1);

            Xs = closedX ? nx.InterpolateExclusive() : nx.InterpolateInclusive();
            Ys = closedY ? ny.InterpolateExclusive() : ny.InterpolateInclusive();
            
            Uvs = Ys.CartesianProduct(Xs, (u, v) => new Vector2D(u, v));

            Indices = nRows.Range().CartesianProduct(nColumns.Range(), (x, y) => QuadMeshFaceVertices(x, y, nx, ny));
        }

        public static Integer4 QuadMeshFaceVertices(int col, int row, int nx, int ny)
        {
            var a = row * nx + col;
            var b = row * nx + (col + 1) % nx;
            var c = (row + 1) % ny * nx + (col + 1) % nx;
            var d = (row + 1) % ny * nx + col;
            return (a, b, c, d);
        }

    }
    */
}