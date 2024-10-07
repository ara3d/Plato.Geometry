using System;
using System.IO;

namespace Plato.Geometry.IO
{
    // This is an endian aware binary reader class
    public class BinaryReader : IDisposable
    {
        private readonly System.IO.BinaryReader _reader;
        private readonly bool _needSwap;
        private readonly bool _leaveOpen;

        public BinaryReader(Stream input, bool isLittleEndian, bool leaveStreamOpen = false)
        {
            _reader = new System.IO.BinaryReader(input);
            _needSwap = BitConverter.IsLittleEndian != isLittleEndian;
            _leaveOpen = leaveStreamOpen;
        }

        public BinaryReader(System.IO.BinaryReader reader, bool isLittleEndian)
        {
            _reader = reader;
            _needSwap = BitConverter.IsLittleEndian != isLittleEndian;
            _leaveOpen = true;
        }

        public uint ReadUInt32()
            => _needSwap ? SwapBytes(_reader.ReadUInt32()) : _reader.ReadUInt32();

        public int ReadInt32()
            => _needSwap ? SwapBytes(_reader.ReadInt32()) : _reader.ReadInt32();

        public ushort ReadUInt16()
            => _needSwap ? SwapBytes(_reader.ReadUInt16()) : _reader.ReadUInt16();

        public short ReadInt16()
            => _needSwap ? SwapBytes(_reader.ReadInt16()) : _reader.ReadInt16();

        public byte ReadByte()
            => _reader.ReadByte();

        public float ReadSingle()
            => _needSwap ? SwapBytes(_reader.ReadSingle()) : _reader.ReadSingle();

        public double ReadDouble()
            => _needSwap ? SwapBytes(_reader.ReadDouble()) : _reader.ReadDouble();

        private static uint SwapBytes(uint x)
            => ((x & 0x000000FF) << 24) |
               ((x & 0x0000FF00) << 8) |
               ((x & 0x00FF0000) >> 8) |
               ((x & 0xFF000000) >> 24);

        private static int SwapBytes(int x)
            => (int)SwapBytes((uint)x);

        private static ushort SwapBytes(ushort x)
            => (ushort)(((x & 0x00FF) << 8) | ((x & 0xFF00) >> 8));

        private static short SwapBytes(short x)
            => (short)SwapBytes((ushort)x);

        private static float SwapBytes(float x)
        {
            var bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        private static double SwapBytes(double x)
        {
            var bytes = BitConverter.GetBytes(x);
            Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        public void Dispose()
        {
            if (!_leaveOpen)
                _reader.Dispose();
        }
    }
}