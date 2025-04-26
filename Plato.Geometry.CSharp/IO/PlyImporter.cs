using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Plato.Geometry.Memory;

namespace Plato.Geometry.IO
{
    public interface IPlyBuffer
    {
        string Name { get; }
        int Count { get; }
        void LoadValue(string s);
        void LoadValue(System.IO.BinaryReader br);
        int GetInt(int index);
        double GetDouble(int index);
    }

    public abstract class PlyBuffer : IPlyBuffer
    {
        public string Name { get; }
        public int Count { get; set; }
        public abstract void LoadValue(string s);
        public abstract void LoadValue(System.IO.BinaryReader br);
        public abstract int GetInt(int index);
        public virtual double GetDouble(int index) => GetInt(index);
        public IBuffer<byte> Bytes;
        public int Capacity => Bytes.Count / ElementSize;
        public readonly int ElementSize;
        public bool IsFull => Count == Capacity;
        public IEnumerable<int> GetInts() => Enumerable.Range(0, Count).Select(GetInt);
        public int RecentInt => GetInt(Count - 1);

        protected PlyBuffer(int count, int elementSize, string name)
        {
            ElementSize = elementSize;
            Bytes = new MemoryBlockBuffer<byte>(count * elementSize);
            Name = name;
        }

        public PlyBuffer Clone()
        {
            var b = (PlyBuffer)MemberwiseClone();
            b.Bytes = new MemoryBlockBuffer<byte>(Bytes.Count);
            b.Count = 0;
            return b;
        }
    }

    public unsafe class UInt8Buffer : PlyBuffer
    {
        public UInt8Buffer(int count, string name) : base(count, 1, name) { }
        public byte* Pointer => Bytes.Pointer;
        public override int GetInt(int n) => Pointer[n];
        public override void LoadValue(string s) => Pointer[Count++] = byte.Parse(s);
        public override void LoadValue(System.IO.BinaryReader br) => Pointer[Count++] = br.ReadByte();
    }

    public unsafe class Int8Buffer : PlyBuffer
    {
        public Int8Buffer(int count, string name) : base(count, 1, name) { }
        public sbyte* Pointer => (sbyte*)Bytes.Pointer;
        public override int GetInt(int n) => Pointer[n];
        public override void LoadValue(string s) => Pointer[Count++] = sbyte.Parse(s);
        public override void LoadValue(System.IO.BinaryReader br) => Pointer[Count++] = br.ReadSByte();
    }

    public unsafe class Int16Buffer : PlyBuffer
    {
        public Int16Buffer(int count, string name) : base(count, 2, name) { }
        public short* Pointer => (short*)Bytes.Pointer;
        public override int GetInt(int n) => Pointer[n];
        public override void LoadValue(string s) => Pointer[Count++] = short.Parse(s);
        public override void LoadValue(System.IO.BinaryReader br) => Pointer[Count++] = br.ReadInt16();
    }

    public unsafe class UInt16Buffer : PlyBuffer
    {
        public UInt16Buffer(int count, string name) : base(count, 2, name) { }
        public ushort* Pointer => (ushort*)Bytes.Pointer;
        public override int GetInt(int n) => Pointer[n];
        public override void LoadValue(string s) => Pointer[Count++] = ushort.Parse(s);
        public override void LoadValue(System.IO.BinaryReader br) => Pointer[Count++] = br.ReadUInt16();
    }

    public unsafe class UInt32Buffer : PlyBuffer
    {
        public UInt32Buffer(int count, string name) : base(count, 4, name) { }
        public uint* Pointer => (uint*)Bytes.Pointer;
        public override int GetInt(int n) => (int)Pointer[n];
        public override void LoadValue(string s) => Pointer[Count++] = uint.Parse(s);
        public override void LoadValue(System.IO.BinaryReader br) => Pointer[Count++] = br.ReadUInt32();
    }

    public unsafe class Int32Buffer : PlyBuffer
    {
        public Int32Buffer(int count, string name) : base(count, 4, name) { }
        public int* Pointer => (int*)Bytes.Pointer;
        public override int GetInt(int n) => Pointer[n];
        public override void LoadValue(string s) => Pointer[Count++] = int.Parse(s);
        public override void LoadValue(System.IO.BinaryReader br) => Pointer[Count++] = br.ReadInt32();
    }

    public unsafe class SingleBuffer : PlyBuffer
    {
        public SingleBuffer(int count, string name) : base(count, 4, name) { }
        public float* Pointer => (float*)Bytes.Pointer;
        public override int GetInt(int n) => (int)Pointer[n];
        public override double GetDouble(int n) => Pointer[n];
        public override void LoadValue(string s) => Pointer[Count++] = float.Parse(s);
        public override void LoadValue(System.IO.BinaryReader br) => Pointer[Count++] = br.ReadSingle();
    }

    public unsafe class DoubleBuffer : PlyBuffer
    {
        public DoubleBuffer(int count, string name) : base(count, 8, name) { }
        public double* Pointer => (double*)Bytes.Pointer;
        public override int GetInt(int n) => (int)Pointer[n];
        public override double GetDouble(int n) => Pointer[n];
        public override void LoadValue(string s) => Pointer[Count++] = double.Parse(s);
        public override void LoadValue(System.IO.BinaryReader br) => Pointer[Count++] = br.ReadDouble();
    }

    public class ListBuffer : IPlyBuffer
    {
        public readonly List<PlyBuffer> Buffers = new List<PlyBuffer>();
        public PlyBuffer Current { get; private set; }
        public ListBuffer(PlyBuffer buffer) => AddBuffer(buffer);
        private void AddBuffer(PlyBuffer buffer) => Buffers.Add(Current = buffer);
        public string Name => Current.Name;
        public int Count { get; set; }

        public void LoadValue(string s)
        {
            Current.LoadValue(s);
            if (Current.IsFull)
                AddBuffer(Current.Clone());
            Count++;
        }

        public void LoadValue(System.IO.BinaryReader br)
        {
            Current.LoadValue(br);
            if (Current.IsFull)
                AddBuffer(Current.Clone());
            Count++;
        }

        public int Cap => Current.Capacity;
        public int GetInt(int index) => Buffers[index / Cap].GetInt(index % Cap);
        public double GetDouble(int index) => Buffers[index / Cap].GetInt(index % Cap);
    }

    public static class PlyImporter
    {
        public static PlyBuffer CreateBuffer(string s, int size, string name)
        {
            switch (s.ToLowerInvariant())
            {
                case "uchar": return new UInt8Buffer(size, name);
                case "uint8": return new UInt8Buffer(size, name);
                case "byte": return new UInt8Buffer(size, name);
                case "ubyte": return new UInt8Buffer(size, name);
                case "char": return new UInt8Buffer(size, name);

                case "int8": return new Int8Buffer(size, name);
                case "sbyte": return new Int8Buffer(size, name);
                
                case "ushort": return new UInt16Buffer(size, name);
                case "uint16": return new UInt16Buffer(size, name);
                
                case "short": return new Int16Buffer(size, name);
                case "int16": return new Int16Buffer(size, name);

                case "uint32": return new UInt32Buffer(size, name);
                case "uint": return new UInt32Buffer(size, name);
                
                case "int": return new Int32Buffer(size, name);
                case "int32": return new UInt32Buffer(size, name);
                
                case "float": return new SingleBuffer(size, name);
                case "float32": return new SingleBuffer(size, name);
                case "single": return new SingleBuffer(size, name);

                case "double": return new DoubleBuffer(size, name);
                case "float64": return new DoubleBuffer(size, name);

                default: throw new Exception("bad PLY format " + s);
            }
        }

        public static IReadOnlyList<string> SplitStrings(string line)
            => line.Split(new[] { ' ', ',', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);


        public static string ReadLineFromFileStream(FileStream fs)
        {
            var bytes = new List<byte>();
            int b;
            while ((b = fs.ReadByte()) != -1)
            {
                if (b == '\n')
                    break;
                if (b == '\r')
                {
                    int next = fs.ReadByte();
                    if (next != '\n' && next != -1)
                        fs.Position--;
                    break;
                }
                bytes.Add((byte)b);
            }
            if (bytes.Count == 0 && b == -1)
                return null;
            return System.Text.Encoding.UTF8.GetString(bytes.ToArray());
        }

        public static TriangleMesh3D LoadMesh(string fileName)
            => LoadBuffers(fileName).ToMesh();

        public static List<IPlyBuffer> LoadBuffers(string fileName)
        {
            var vertexBuffers = new List<PlyBuffer>();
            PlyBuffer faceSizeBuffer = null;
            ListBuffer indexBuffer = null;
            var faceBuffers = new List<PlyBuffer>();
            var materialBuffers = new List<PlyBuffer>();
            var fmt = "";

            var num_materials = 0;
            var num_vertices = 0;
            var num_faces = 0;
            var cur_element = "";

            using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                // Parse the ASCII text at the start up to "end_header\n"
                var done = false;
                while (!done)
                {
                    var line = ReadLineFromFileStream(file);
                    var words = SplitStrings(line);

                    if (words.Count == 0)
                        continue;

                    switch (words[0])
                    {
                        case "ply":
                            break;
                        case "comment":
                            break;
                        case "format":
                            fmt = words[1];
                            break;
                        case "element":
                        {
                            switch (cur_element = words[1])
                            {
                                case "vertex":
                                    num_vertices = int.Parse(words[2]);
                                    break;
                                case "face":
                                    num_faces = int.Parse(words[2]);
                                    break;
                                case "tristrips":
                                    num_faces = int.Parse(words[2]);
                                    break;
                                case "material":
                                    num_materials = int.Parse(words[2]);
                                    break;
                                default:
                                    throw new Exception($"Bad PLY element: {line}");
                            }
                            break;
                        }
                        case "property":
                        {
                            if (cur_element == "vertex")
                            {
                                if (words.Count < 3)
                                    throw new Exception($"Bad PLY property: {line}");

                                vertexBuffers.Add(CreateBuffer(words[1], num_vertices, words[2]));
                            }
                            else if (cur_element == "face" || cur_element == "tristrips")
                            {
                                if (words[1] == "list")
                                {
                                    if (words.Count != 5 && words[4] != "vertex_indices")
                                        throw new Exception("Only vertex_indices support being in a list");

                                    if (indexBuffer != null || faceSizeBuffer != null)
                                        throw new Exception("Already found a face size or index buffer");

                                    faceSizeBuffer = CreateBuffer(words[2], num_faces, "face_sizes");
                                    indexBuffer =
                                        new ListBuffer(CreateBuffer(words[3], num_faces, "vertex_indices"));
                                }
                                else
                                {
                                    if (indexBuffer == null || faceSizeBuffer == null)
                                        throw new Exception(
                                            "The vertex_indices should be before other face properties");

                                    faceBuffers.Add(CreateBuffer(words[1], num_faces, words[2]));
                                }
                            }
                        }
                            break;
                        case "end_header":
                            done = true;
                            break;

                        default:
                            throw new Exception($"bad PLY field: {line}");
                    }
                }

                if (faceSizeBuffer == null)
                    throw new Exception("No face size buffer found");
                if (indexBuffer == null)
                    throw new Exception("No index buffer found");
                if (vertexBuffers.Count == 0)
                    throw new Exception("No vertex buffers found");

                if (fmt == "ascii")
                {
                    using (var streamReader = new StreamReader(file))
                    {

                        for (var i = 0; i != num_vertices; ++i)
                        {
                            var line = streamReader.ReadLine();
                            if (line == null)
                                throw new Exception("Unexpected end of file");

                            var values = SplitStrings(line);

                            if (values.Count != vertexBuffers.Count)
                                throw new Exception(
                                    $"bad PLY vertex line, expected {vertexBuffers.Count} properties, but found {values.Count}");

                            var index = 0;
                            foreach (var buffer in vertexBuffers)
                                buffer.LoadValue(values[index++]);
                        }

                        for (var i = 0; i != num_faces; ++i)
                        {
                            var line = streamReader.ReadLine();
                            if (line == null)
                                throw new Exception("Unexpected end of file");

                            var values = SplitStrings(line);
                            faceSizeBuffer.LoadValue(values[0]);
                            var cnt = faceSizeBuffer.RecentInt;
                            if (cnt < 0)
                                throw new Exception("Could not determine face size");

                            if (values.Count != cnt + 1)
                                throw new Exception($"Expected {cnt + 1} values, but foumd {values.Count}");

                            for (var j = 0; j != cnt; ++j)
                                indexBuffer.LoadValue(values[j + 1]);

                            if (faceBuffers.Count > 0)
                                throw new Exception("Face buffers not yet implemented");
                        }   

                        for (var i = 0; i != num_materials; ++i)
                        {
                            var line = streamReader.ReadLine();
                            if (line == null)
                                throw new Exception("Unexpected end of file");

                            var values = SplitStrings(line);
                            faceSizeBuffer.LoadValue(values[0]);

                            if (values.Count != materialBuffers.Count)
                                throw new Exception(
                                    $"bad PLY vertex line, expected {materialBuffers.Count} properties, but found {values.Count}");

                            var index = 0;
                            foreach (var buffer in materialBuffers)
                                buffer.LoadValue(values[index++]);
                        }
                    }
                }
                else if (fmt == "binary_little_endian")
                {
                    using (var binaryReader = new System.IO.BinaryReader(file))
                    {
                        for (var i = 0; i != num_vertices; ++i)
                        {
                            foreach (var buffer in vertexBuffers)
                                buffer.LoadValue(binaryReader);
                        }

                        for (var i = 0; i != num_faces; ++i)
                        {
                            faceSizeBuffer.LoadValue(binaryReader);
                            var cnt = faceSizeBuffer.RecentInt;
                            if (cnt < 0)
                                throw new Exception("Could not determine face size");

                            for (var j = 0; j != cnt; ++j)
                                indexBuffer.LoadValue(binaryReader);

                            foreach (var faceBuffer in faceBuffers)
                                faceBuffer.LoadValue(binaryReader);
                        }

                        for (var i = 0; i != num_materials; ++i)
                        {
                            foreach (var buffer in materialBuffers)
                                buffer.LoadValue(binaryReader);
                        }

                    }
                }
                else
                {
                    throw new Exception($"Unrecognized PLY format {fmt}");
                }
            }

            var allBuffers = new List<IPlyBuffer>();
            allBuffers.AddRange(vertexBuffers);
            allBuffers.Add(faceSizeBuffer);
            allBuffers.Add(indexBuffer);
            allBuffers.AddRange(faceBuffers);
            allBuffers.AddRange(materialBuffers);
            return allBuffers;
        }

        public static IArray<T> ToIArray<T>(this IEnumerable<T> self)
            => self.ToList().ToIArray();

        public static IArray<T> ToIArray<T>(this IReadOnlyList<T> self)
            => new Array<T>(self.Count, i => self[i]);

        public static TriangleMesh3D ToMesh(this IReadOnlyList<IPlyBuffer> buffers)
        {
            var xs = buffers.First(b => b.Name == "x");
            var ys = buffers.First(b => b.Name == "y");
            var zs = buffers.First(b => b.Name == "z");

            if (xs == null) throw new Exception("Missing x property");
            if (ys == null) throw new Exception("Missing y property");
            if (zs == null) throw new Exception("Missing z property");

            // TODO: normals / colors / uv

            var vertices = new List<Vector3>();
            for (var i = 0; i != xs.Count; ++i)
                vertices.Add(new Vector3((float)xs.GetDouble(i), (float)ys.GetDouble(i), (float)zs.GetDouble(i)));
            
            var indexBuffer = buffers.First(b => b.Name == "vertex_indices");
            
            var faceSizes = buffers.First(b => b.Name == "face_sizes");
            var numFaces = faceSizes.Count;
            var cur = 0;

            var indices = new List<Integer3>();
            for (var f = 0; f < numFaces; f++)
            {
                var cnt = faceSizes.GetInt(f);
                for (var i = 0; i < cnt - 2; i++)
                {
                    indices.Add(new Integer3(
                    indexBuffer.GetInt(cur + i),
                    indexBuffer.GetInt(cur + i + 1),
                    indexBuffer.GetInt(cur + i + 2)));
                }

                cur += cnt;
            }

            return new TriangleMesh3D(vertices.Select(v => v.Point3D).ToIArray(), indices.ToIArray());
        }   
    }
}
