using System.Diagnostics;
using Ara3D.Utils;
using BitMiracle.LibTiff.Classic;
using Plato.Geometry.IO;
using Plato.Geometry.Tests;
using Plato.SinglePrecision;
using Console = System.Console;

namespace Plato.Geometry.Tests
{
    public static class Tests
    {
        public static FilePath[] InputFiles =
        {

            //@"C:\tmp\copernicus_montreal.tif",
            //@"C:\tmp\copernicus_montreal2.tif",
            @"C:\tmp\srtm_22_03.tif",
            //@"C:\Users\cdigg\Copernicus_DSM_COG_30_S90_00_W172_00_DEM.tif",
            //@"C:\Users\cdigg\Downloads\gpxz_elevation.geotiff"
        };

        [Test]
        public static void TiffToObj()
        {
            foreach (var fp in InputFiles)
            {
                var outputFile = fp.ChangeDirectoryAndExt(@"c:\tmp", "obj");
                var tiffData = new GeoTiff(fp);
                OutputToObjFile(tiffData, outputFile);
            }
        }

        public static void OutputDetails(GeoTiff td)
        {
            Console.WriteLine($"Width = {td.width}, Height = {td.height}");
        }

        public static void OutputToObjFile(GeoTiff td, string filePath)
        {
            var triMesh = td.BuildTriMesh(500, 500, 500, 500);
            ObjExporter.WriteObj(triMesh, filePath);
        }
    }

    /// <summary>
    /// Reads a GeoTIFF using LibTiff, 
    /// - Handles both TILED and STRIPPED images.
    /// - Converts pixel samples (8-bit, 16-bit int, 32-bit float, 64-bit float, etc.) into double.
    /// - Uses ModelPixelScaleTag (33550) + ModelTiepointTag (33922) for basic georeferencing.
    /// </summary>
    public class GeoTiff
    {
        public int width;
        public int height;
        public int bitsPerSample;
        public double pixelSizeX;
        public double pixelSizeY;
        public double tiePointX;
        public double tiePointY;
        public bool isTiled;
        public double[] raster;

        public List<DataPoint> Points = new List<DataPoint>();

        public Vector3D ToVector3D(DataPoint p)
        {
            return new Vector3D(p.Col, p.Row, p.Elevation / 20);
        }

        public DataPoint GetDataPoint(int col, int row)
        {
            var r = Points[col + row * width];
            Debug.Assert(r.Row == row);
            Debug.Assert(r.Col == col);
            return r;
        }

        public Vector3D GetVertex(int col, int row)
        {
            return ToVector3D(GetDataPoint(col, row));
        }

        public TriangleMesh3D BuildTriMesh(int xOffset, int yOffset, int maxWidth, int maxHeight)
        {
            var vertices = new List<Vector3D>();
            maxHeight = Math.Min(maxHeight, height);
            maxWidth = Math.Min(maxWidth, width);
            for (var row = 0; row < maxHeight - 1; row++)
            {
                for (var col = 0; col < maxWidth - 1; col++)
                {
                    var v1 = GetVertex(xOffset + col, yOffset + row);
                    var v2 = GetVertex(xOffset + col, yOffset + row + 1);
                    var v3 = GetVertex(xOffset + col + 1, yOffset + row + 1);
                    var v4 = GetVertex(xOffset + col + 1, yOffset + row);
                    vertices.Add(v1);
                    vertices.Add(v2);
                    vertices.Add(v3);
                    vertices.Add(v4);
                }
            }

            return vertices.ToIArray().ToQuadMesh().ToTriangleMesh();
        }
        
        // For copernicus we default to this data 
        public const SampleFormat DEFAULT_SAMPLE_FORMAT = SampleFormat.IEEEFP;

        public GeoTiff(string filePath)
        {
            //Console.WriteLine($"Loading {filePath}");
            using (var tiff = Tiff.Open(filePath, "r"))
            {
                if (tiff == null)
                    throw new IOException($"Could not open {filePath}");

                // Make sure we go to the first directory (the full-resolution DEM)
                // If you want IFD #0, do this:
                short dirIndex = 0;
                int bestWidth = 0, bestHeight = 0;
                short bestDirIndex = 0;

                do
                {
                    int w = tiff.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
                    int h = tiff.GetField(TiffTag.IMAGELENGTH)[0].ToInt();
                    if (w * h > bestWidth * bestHeight)
                    {
                        bestWidth = w;
                        bestHeight = h;
                        bestDirIndex = dirIndex;
                    }
                    dirIndex++;
                }
                while (tiff.ReadDirectory());

                // Now jump to the directory with the largest resolution
                tiff.SetDirectory(bestDirIndex);

                // Get basic image size
                width = tiff.GetField(TiffTag.IMAGEWIDTH)[0].ToInt();
                height = tiff.GetField(TiffTag.IMAGELENGTH)[0].ToInt();

                // Bits per sample & sample format (int vs float)
                bitsPerSample = tiff.GetField(TiffTag.BITSPERSAMPLE)[0].ToInt();

                var sampleFormat = bitsPerSample == 32 ? DEFAULT_SAMPLE_FORMAT : SampleFormat.UINT;
                var sfField = tiff.GetField(TiffTag.SAMPLEFORMAT);
                if (sfField != null)
                    sampleFormat = (SampleFormat)sfField[0].ToInt();

                // Single-band check (for simplicity)
                var samplesPerPixel = 1;
                var sppField = tiff.GetField(TiffTag.SAMPLESPERPIXEL);
                if (sppField != null)
                    samplesPerPixel = sppField[0].ToInt();
                if (samplesPerPixel != 1)
                    throw new NotSupportedException("This example only handles single-band imagery.");

                // Determine if image is tiled
                isTiled = tiff.IsTiled();

                // 5. Before reading the older ModelPixelScaleTag & ModelTiepointTag, 
                //    see if a ModelTransformationTag (34264) is present.
                var transformTag = tiff.GetField((TiffTag)34264);  // ModelTransformationTag
                if (transformTag != null)
                {
                    // Parse the 4x4 affine transform matrix (16 doubles)
                    byte[] transformBytes = transformTag[1].GetBytes();
                    if (transformBytes.Length >= 8 * 16)
                    {
                        double[] matrix = new double[16];
                        for (int i = 0; i < 16; i++)
                            matrix[i] = BitConverter.ToDouble(transformBytes, i * 8);

                        // A typical "north‐up, no rotation" transform looks like:
                        // [ scaleX,  0,       0,  tiePointX ]
                        // [ 0,      -scaleY,  0,  tiePointY ] 
                        // [ 0,       0,       1,       0     ]
                        // [ 0,       0,       0,       1     ]

                        // If there is no rotation/shear, you can directly read:
                        pixelSizeX = matrix[0]; // scale in X
                        pixelSizeY = matrix[5]; // scale in Y (often negative for north‐up)
                        tiePointX = matrix[3]; // translation in X
                        tiePointY = matrix[7]; // translation in Y

                        // NOTE: If rotation/shear is nonzero, you must interpret the full affine properly. 
                        // we are skipping that 
                    }
                }
                else
                {
                    // ModelPixelScaleTag = 33550 => array of 3 doubles: scaleX, scaleY, scaleZ
                    var scaleTag = tiff.GetField((TiffTag)33550);
                    if (scaleTag != null)
                    {
                        var scaleBytes = scaleTag[1].GetBytes();
                        pixelSizeX = BitConverter.ToDouble(scaleBytes, 0);
                        pixelSizeY = BitConverter.ToDouble(scaleBytes, 8);
                        // scaleZ is at index 16, if needed
                    }

                    // ModelTiepointTag = 33922 => array of 6 doubles:
                    // (i, j, k, x, y, z) mapping the upper-left corner of the raster (usually 0,0) to real coords
                    var tieTag = tiff.GetField((TiffTag)33922);
                    if (tieTag != null)
                    {
                        var tieBytes = tieTag[1].GetBytes();
                        // We usually only need tiePointX, tiePointY
                        // Raster i=0 is at offset 0, j=0 is at offset 8, k=0 is at offset 16
                        // Geo x is at offset 24, y is offset 32, z offset 40
                        tiePointX = BitConverter.ToDouble(tieBytes, 24);
                        tiePointY = BitConverter.ToDouble(tieBytes, 32);
                    }
                }

                // Prepare to read raster into a double[] array
                raster = new double[width * height];

                if (isTiled)
                {
                    ReadTiledRaster(tiff, width, height, bitsPerSample, sampleFormat, raster);
                }
                else
                {
                    ReadStrippedRaster(tiff, width, height, bitsPerSample, sampleFormat, raster);
                }

                // Convert the linear raster array into yield of DataPoint with lat/lon
                // We assume top-left is (tiePointX, tiePointY) and each pixel is pixelSizeX, pixelSizeY
                // Note: If pixelSizeY is negative, row i means Y = tiePointY + i * pixelSizeY
                //       That is typically how GeoTIFFs define "north up" images.
                for (var row = 0; row < height; row++)
                {
                    var geoY = tiePointY + row * pixelSizeY;
                    for (var col = 0; col < width; col++)
                    {
                        var geoX = tiePointX + col * pixelSizeX;
                        var value = raster[row * width + col];

                        Points.Add(new DataPoint
                        {
                            Col = col,
                            Row = row,
                            Longitude = geoX, // or easting if in projected coords
                            Latitude = geoY, // or northing if in projected coords
                            Elevation = value
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Reads the entire image (single band) for a TILED TIFF into the given <paramref name="raster"/> array.
        /// The array is row-major: raster[row * width + col].
        /// </summary>
        private static void ReadTiledRaster(
            Tiff tiff,
            int width,
            int height,
            int bitsPerSample,
            SampleFormat sampleFormat,
            double[] raster)
        {
            // Get tile dimensions
            var tileWidth = tiff.GetField(TiffTag.TILEWIDTH)[0].ToInt();
            var tileHeight = tiff.GetField(TiffTag.TILELENGTH)[0].ToInt();
            var tileSize = tiff.TileSize();

            // Temporary buffer for reading each tile
            var tileBuffer = new byte[tileSize];

            for (var row = 0; row < height; row += tileHeight)
            {
                for (var col = 0; col < width; col += tileWidth)
                {
                    // Read tile into tileBuffer
                    // Note: The Tiff.ReadEncodedTile() index is the tile index 
                    // that covers (col, row). The library calculates the tile index 
                    // from (col, row), so we pass them in:
                    var tileIndex = tiff.ComputeTile(col, row, 0, 0);
                    var bytesRead = tiff.ReadEncodedTile(tileIndex, tileBuffer, 0, tileSize);
                    if (bytesRead <= 0)
                        throw new Exception("Could not read tile data");

                    // Figure out how many columns/rows of *actual* image are in this tile 
                    // (the tile might extend beyond the right/bottom edge)
                    var colsInTile = Math.Min(tileWidth, width - col);
                    var rowsInTile = Math.Min(tileHeight, height - row);

                    // Parse tileBuffer and store into raster
                    CopyToRaster(tileBuffer, 0,
                        raster, width,
                        col, row,
                        colsInTile, rowsInTile,
                        bitsPerSample, sampleFormat, tileWidth);
                }
            }
        }

        /// <summary>
        /// Reads the entire image (single band) for a STRIPPED TIFF into the given <paramref name="raster"/> array.
        /// We read by strip(s). Many TIFFs have multiple strips.
        /// </summary>
        private static void ReadStrippedRaster(
            Tiff tiff,
            int width,
            int height,
            int bitsPerSample,
            SampleFormat sampleFormat,
            double[] raster)
        {
            // Number of strips
            var strips = tiff.NumberOfStrips();
            // Usually each strip is some chunk of rows. We decode each strip, then place its rows into 'raster'.
            // We read each strip via ReadEncodedStrip.

            for (var stripIndex = 0; stripIndex < strips; stripIndex++)
            {
                // Byte size of this strip:
                var stripSize = tiff.RawStripSize(stripIndex);
                var buffer = new byte[stripSize];
                var readCount = tiff.ReadEncodedStrip(stripIndex, buffer, 0, (int)stripSize);
                if (readCount <= 0)
                    throw new Exception($"Could not read strip {stripIndex}");

                // Figure out which rows are covered by this strip.
                // Some TIFFs store the tag ROWSPERSTRIP
                var rpsField = tiff.GetField(TiffTag.ROWSPERSTRIP);
                var rowsPerStrip = (rpsField != null) ? rpsField[0].ToInt() : height;

                // The first row for this strip:
                var firstRow = stripIndex * rowsPerStrip;
                // The last row for this strip:
                var lastRow = Math.Min(firstRow + rowsPerStrip, height);

                // Now we need to place these rows into the raster array. 
                // The buffer is a contiguous chunk of (lastRow - firstRow) * width * (bitsPerSample/8) bytes (assuming single band).
                // We'll parse row by row.
                var bytesPerSample = bitsPerSample / 8;
                var rowSizeBytes = width * bytesPerSample;

                var offsetInBuffer = 0;
                for (var row = firstRow; row < lastRow; row++)
                {
                    CopyToRaster(buffer, offsetInBuffer,
                        raster, width,
                        0 /* colStart */, row,
                        width, 1, // entire row
                        bitsPerSample, sampleFormat, width);

                    offsetInBuffer += rowSizeBytes;
                }
            }
        }

        /// <summary>
        /// Copies pixel data from <paramref name="buffer"/> into the <paramref name="raster"/> array, 
        /// interpreting the data type (bitsPerSample, sampleFormat) as a single band, 
        /// and converting each pixel to double.
        /// 
        /// This function is used both by the tile and st    rip readers, to parse raw pixel bytes
        /// into the final double array.
        /// 
        /// - buffer: raw tile/strip data
        /// - bufferOffset: offset in buffer to start reading
        /// - raster: the final image array in row-major order
        /// - rasterWidth: the full width of the image
        /// - startCol, startRow: top-left in the final image where we place these pixels
        /// - cols, rows: how many columns/rows in this chunk
        /// - tileOrRowWidth: how many *columns* were actually allocated in the tile/row buffer 
        ///   (used to jump to the next row within the tile buffer)
        /// </summary>
        private static void CopyToRaster(
            byte[] buffer,
            int bufferOffset,
            double[] raster,
            int rasterWidth,
            int startCol,
            int startRow,
            int cols,
            int rows,
            int bitsPerSample,
            SampleFormat sampleFormat,
            int tileOrRowWidth)
        {
            var bytesPerSample = bitsPerSample / 8;
            // Distance in bytes between the start of row R and row R+1 in the buffer
            var bufferRowStride = tileOrRowWidth * bytesPerSample;

            var localOffset = bufferOffset;
            for (var r = 0; r < rows; r++)
            {
                var rasterRowIndex = (startRow + r) * rasterWidth + startCol;
                var rowBufferOffset = localOffset;

                for (var c = 0; c < cols; c++)
                {
                    var value = ReadSampleAsDouble(buffer, rowBufferOffset + c * bytesPerSample, bitsPerSample, sampleFormat);
                    raster[rasterRowIndex + c] = value;
                }

                // move to next row in the tile/strip buffer
                localOffset += bufferRowStride;
            }
        }

        /// <summary>
        /// Interprets the bytes at <paramref name="offset"/> in <paramref name="buffer"/>
        /// as a single pixel sample, converting it to double.
        /// 
        /// Handles common combos: 
        ///  - 8-bit uint
        ///  - 16-bit int or uint
        ///  - 32-bit float
        ///  - 32-bit int or uint
        ///  - 64-bit float
        ///  - 64-bit int or uint  (less common, but can be handled similarly)
        /// 
        /// Extend as needed for other sample formats.
        /// </summary>
        private static double ReadSampleAsDouble(byte[] buffer, int offset, int bitsPerSample, SampleFormat sampleFormat)
        {
            switch (bitsPerSample)
            {
                case 8: // 1 byte
                    // Usually unsigned
                    var v8 = buffer[offset];
                    return (double)v8;

                case 16: // 2 bytes
                    if (sampleFormat == SampleFormat.UINT)
                    {
                        var v16 = BitConverter.ToUInt16(buffer, offset);
                        return (double)v16;
                    }
                    else if (sampleFormat == SampleFormat.INT)
                    {
                        var s16 = BitConverter.ToInt16(buffer, offset);
                        return (double)s16;
                    }
                    break;

                case 32: // 4 bytes
                    if (sampleFormat == SampleFormat.IEEEFP)
                    {
                        var f32 = BitConverter.ToSingle(buffer, offset);
                        return (double)f32;
                    }
                    else if (sampleFormat == SampleFormat.UINT)
                    {
                        var u32 = BitConverter.ToUInt32(buffer, offset);
                        return (double)u32;
                    }
                    else if (sampleFormat == SampleFormat.INT)
                    {
                        var i32 = BitConverter.ToInt32(buffer, offset);
                        return (double)i32;
                    }
                    break;

                case 64: // 8 bytes
                    if (sampleFormat == SampleFormat.IEEEFP)
                    {
                        var f64 = BitConverter.ToDouble(buffer, offset);
                        return f64;
                    }
                    else if (sampleFormat == SampleFormat.UINT || sampleFormat == SampleFormat.INT)
                    {
                        // 64-bit integer. 
                        var i64 = BitConverter.ToInt64(buffer, offset);
                        // If it's truly unsigned 64-bit, you may need separate logic,
                        // but C# doesn't have a built-in unsigned 64 -> double method 
                        // that differs from signed 64. Typically they'll interpret the same bits.
                        return (double)i64;
                    }
                    break;
            }

            // If we get here, we encountered a combo we didn't explicitly handle
            throw new NotSupportedException(
                $"Unsupported sample format: bitsPerSample={bitsPerSample}, sampleFormat={sampleFormat}");
        }
    }
}