using System;

namespace Plato.DoublePrecision
{
    /// <summary>
    /// This implements a quad mesh that has a fixed number of columns and rows.
    /// It may be closed on the X (columns) meaning that the last column is connected to the first column.
    /// And/or it may be closed on the Y (rows) meaning that the last row is connected to the first row.
    /// </summary>
    public partial struct QuadGrid 
    {
        /*
        public bool ClosedX { get; }
        public bool ClosedY { get; }
        public IArray<Vector3D> Points => PointGrid;
        public IArray2D<Vector3D> PointGrid { get; }
        public Integer PrimitiveSize => 4;
        public IArray<Integer> Indices { get; }
        public Integer NumColumns => PointGrid.ColumnCount;
        public Integer NumRows => PointGrid.RowCount;

        public QuadGrid(IArray2D<Vector3D> points, Boolean closedX, Boolean closedY)
        {
            ClosedX = closedX;
            ClosedY = closedY;
            PointGrid = points;
            Indices = AllQuadFaceIndicesFlat(NumColumns, NumRows, ClosedX, ClosedY);
        }

        public static implicit operator QuadGrid((IArray2D<Vector3D>, Boolean closedX, Boolean closedY) tuple)
            => new QuadGrid(tuple.Item1, tuple.Item2, tuple.Item3);

        // d -- c
        // |    |
        // a -- b
        // Where a == (col,row), b == (col+1,row), c == (col+1,row+1), d == (col,row+1)
        public static Integer4 QuadFaceIndices(int col, int row, int nCols, int nRows)
        {
            var a = row * nCols + col;
            var b = row * nCols + (col + 1) % nCols;
            var c = (row + 1) % nRows * nCols + (col + 1) % nCols;
            var d = (row + 1) % nRows * nCols + col;
            return (a, b, c, d);
        }

        public static IArray2D<Integer4> AllQuadFaceIndices(Integer nCols, Integer nRows, Boolean closedX, Boolean closedY)
        {
            var nx = nCols - (closedX ? 0 : 1);
            var ny = nRows - (closedY ? 0 : 1);
            return nx.Range.CartesianProduct(ny.Range, (col, row) => QuadFaceIndices(col, row, nCols, nRows));
        }

        public static IArray<Integer> ToIndexArray(IArray2D<Integer4> xs)
            => xs.FlatMap(x => x);

        public static IArray<Integer> AllQuadFaceIndicesFlat(Integer nCols, Integer nRows, Boolean closedX, Boolean closedY)
            => ToIndexArray(AllQuadFaceIndices(nCols, nRows, closedX, closedY));

        public QuadGrid Deform(Func<Vector3D, Vector3D> f)
            => (PointGrid.Map(f), ClosedX, ClosedY);

        public QuadGrid Transform(Matrix4x4 matrix)
            => Deform(matrix.TransformPoint);

        IDeformable3D IDeformable3D.Deform(Func<Vector3D, Vector3D> f)
            => Deform(f);
        
        public static implicit operator QuadMesh(QuadGrid q)
            => (q.Points, q.Indices);

        public static implicit operator PointArray(QuadGrid q)
            => q.Points.ToPoints();

        public static implicit operator TriangleMesh(QuadGrid q)
            => q.ToTriangleMesh();
        */
    }
}