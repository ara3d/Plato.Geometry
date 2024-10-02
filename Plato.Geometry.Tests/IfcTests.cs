using Ara3D.IfcLoader;
using Ara3D.Utils;
using Plato.Geometry.Ifc;
using Plato.Geometry.IO;
using Plato.Geometry.Scenes;

namespace Plato.Geometry.Tests
{
    public static class IfcTests
    {
        public static DirectoryPath TestFileFolder =
            PathUtil.GetCallerSourceFolder().RelativeFolder("..", "..", "IFC-toolkit", "test-files");

        public static DirectoryPath OutputFolder =
            PathUtil.GetCallerSourceFolder().RelativeFolder("..", "..", "IFC-toolkit", "test-output");

        [Test]
        public static void TestMergedIfc()
        {
            var fp = TestFileFolder.RelativeFile("AC20-FZK-Haus.ifc");
            var ifcFile = IfcFile.Load(fp, true);
            var scene = ifcFile.ToScene();
            var mesh = scene.ToTriangleMesh();
            var output = fp.ChangeDirectoryAndExt(OutputFolder, ".obj");
            mesh.WriteObj(output);
        }

        [Test]
        public static void TestIfcObjects()
        {
            var fp = TestFileFolder.RelativeFile("AC20-FZK-Haus.ifc");
            var ifcFile = IfcFile.Load(fp, true);
            foreach (var g in ifcFile.Model.GetGeometries())
            {
                foreach (var m in g.GetMeshes())
                {
                    var mesh = m.ToTriangleMesh();
                    var output = OutputFolder.RelativeFile($"AC20_{g.Id}_{m.Id}.obj");
                    mesh.WriteObj(output);
                }
            }
        }
    }
}