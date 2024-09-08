using Plato.DoublePrecision;

namespace Plato.Geometry.Tests
{
    public static class ArrayExtensions 
    {
        public static ArrayImplementation<T> Map<T>(this int count, Func<int, T> f)
            => new(count, new Func<Integer, T>(x => f(x)));
    }

    public class Tests
    {
        public static TriMesh CreateMesh(Array<SimpleVertex> vertices, Array<TriFace> faces)
        {
            return new TriMesh(vertices, faces);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}