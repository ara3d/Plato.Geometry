using Ara3D.Utils;

namespace Plato.Geometry.Tests
{
    public static class Tools
    {
        [Test, Explicit]
        public static void CountChars()
        {
            var d = new DirectoryPath(@"C:\Users\cdigg\git\ara3d\Plato.Geometry\plato-src");
            var sum = d.GetFiles("*.plato").Sum(fp => fp.ReadAllText().Length);
            Console.WriteLine($"Totla number of characters {sum}");
        }
    }
}