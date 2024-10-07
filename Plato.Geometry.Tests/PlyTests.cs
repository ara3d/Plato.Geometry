using Ara3D.Logging;
using Ara3D.Utils;
using Plato.Geometry.IO;

namespace Plato.Geometry.Tests
{
    public static class PlyTests
    {
        public static DirectoryPath DataFileFolder = PathUtil.GetCallerSourceFolder().RelativeFolder("..", "..", "..", "3d-format-shootout", "data", "big", "ply");

        public static IEnumerable<FilePath> DataFiles = DataFileFolder.GetFiles("*.ply");

        [Test]
        [TestCaseSource(nameof(DataFiles))]
        public static void PlyFilesTest(FilePath fp)
        {
            var logger = Logger.Console; 
            var buffers = PlyImporter.LoadBuffers(fp);
            logger.Log($"Loaded {buffers.Count} buffers");
            var index = 0;
            foreach (var buffer in buffers)
            {
                logger.Log($"Buffer {index++} {buffer.Name} {buffer.Count} Type = {buffer.GetType().Name}");
            }
        }
    }
}