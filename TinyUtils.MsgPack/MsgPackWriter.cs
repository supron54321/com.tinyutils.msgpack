using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace TinyMsgPack
{
    public unsafe struct MsgPackWriter : IDisposable
    {
        private NativeList<byte> _buffer;

        public MsgPackWriter(Allocator allocator)
        {
            _buffer = new NativeList<byte>(64, Allocator.Temp);
        }

        public void Dispose()
        {
            _buffer.Dispose();
        }

        public int GetBufferLength() => _buffer.Length;
        public IntPtr GetUnsafeBufferPtr() => (IntPtr)_buffer.GetUnsafePtr();

        public void WriteArrayHeader(int length)
        {
            if (length <= (MsgPackCode.MaxFixArray - MsgPackCode.MinFixArray))
            {
                WriteRaw8((byte)(length+MsgPackCode.MinFixArray));
            }
            else if (length <= ushort.MaxValue)
            {
                WriteRaw8(MsgPackCode.Array16);
                WriteRawBigEndian16((ushort)length);
            }
            else
            {
                WriteRaw8(MsgPackCode.Array32);
                WriteRawBigEndian32((uint)length);
            }
        }

        public void WriteMapHeader(int length)
        {
            if (length <= (MsgPackCode.MaxFixMap - MsgPackCode.MinFixMap))
            {
                WriteRaw8((byte)(length+MsgPackCode.MinFixMap));
            }
            else if (length <= ushort.MaxValue)
            {
                WriteRaw8(MsgPackCode.Map16);
                WriteRawBigEndian16((ushort)length);
            }
            else
            {
                WriteRaw8(MsgPackCode.Map32);
                WriteRawBigEndian32((uint)length);
            }
        }

        public void WriteString(string str)
        {
            if (str == null)
            {
                WriteNil();
                return;
            }
            int length = StringUtils.GetEncodedLength(str);
            if(length <= (MsgPackCode.MaxFixStr-MsgPackCode.MinFixStr))
                WriteRaw8((byte)(length+MsgPackCode.MinFixStr));
            else if(length < byte.MaxValue)
            {
                WriteRaw8(MsgPackCode.Str8);
                WriteRaw8((byte)length);
            }
            else if (length < ushort.MaxValue)
            {
                WriteRaw8(MsgPackCode.Str16);
                WriteRawBigEndian16((ushort)length);
            }
            else
            {
                WriteRaw8(MsgPackCode.Str32);
                WriteRawBigEndian32((uint)length);
            }
            _buffer.ResizeUninitialized(_buffer.Length+length);
            StringUtils.WriteEncodedString(str, (byte*)_buffer.GetUnsafePtr()+_buffer.Length-length, length);
        }

        public void WriteNil()
        {
            WriteRaw8(MsgPackCode.Nil);
        }
        

        public void WriteInteger(byte value)
        {
            if (value <= MsgPackCode.MaxFixInt)
            {
                WriteRaw8(value);
            }
            else
            {
                WriteRaw8(MsgPackCode.UInt8);
                WriteRaw8(value);
            }
        }
        
        // TODO: Negative Fix Int
        public void WriteInteger(sbyte value)
        {
            if (value >= MsgPackCode.MinFixInt && value <= MsgPackCode.MaxFixInt)
            {
                WriteRaw8((byte)value);
            }
            else
            {
                WriteRaw8(MsgPackCode.Int8);
                WriteRaw8((byte)value);
            }
        }

        public void WriteInteger(ushort value)
        {
            if (value <= MsgPackCode.MaxFixInt)
            {
                WriteRaw8((byte)value);
            }
            else if(value <= byte.MinValue)
            {
                WriteRaw8(MsgPackCode.UInt8);
                WriteRaw8((byte)value);
            }
            else
            {
                WriteRaw8(MsgPackCode.UInt16);
                WriteRawBigEndian16(value);
            }
        }
        
        public void WriteInteger(short value)
        {
            if (value >= MsgPackCode.MaxFixInt && value <= MsgPackCode.MaxFixInt)
            {
                WriteRaw8((byte)value);
            }
            else if(value >= sbyte.MinValue && value <= sbyte.MinValue)
            {
                WriteRaw8(MsgPackCode.Int8);
                WriteRaw8((byte)value);
            }
            else
            {
                WriteRaw8(MsgPackCode.Int16);
                WriteRawBigEndian16((ushort)value);
            }
        }

        public void WriteInteger(uint value)
        {
            if (value <= MsgPackCode.MaxFixInt)
            {
                WriteRaw8((byte)value);
            }
            else if(value <= byte.MinValue)
            {
                WriteRaw8(MsgPackCode.UInt8);
                WriteRaw8((byte)value);
            }
            else if(value <= ushort.MaxValue)
            {
                WriteRaw8(MsgPackCode.UInt16);
                WriteRawBigEndian16((ushort)value);
            }
            else
            {
                WriteRaw8(MsgPackCode.UInt32);
                WriteRawBigEndian32(value);
            }
        }

        public void WriteInteger(int value)
        {
            if (value >= MsgPackCode.MinFixInt && value <= MsgPackCode.MaxFixInt)
            {
                WriteRaw8((byte)value);
            }
            else if(value >= sbyte.MinValue && value <= sbyte.MinValue)
            {
                WriteRaw8(MsgPackCode.Int8);
                WriteRaw8((byte)value);
            }
            else if( value >= short.MinValue && value <= short.MaxValue)
            {
                WriteRaw8(MsgPackCode.Int16);
                WriteRawBigEndian16((ushort)value);
            }
            else
            {
                WriteRaw8(MsgPackCode.Int32);
                WriteRawBigEndian32((uint)value);
            }
        }
        
        

        public void WriteInteger(ulong value)
        {
            if (value <= MsgPackCode.MaxFixInt)
            {
                WriteRaw8((byte)value);
            }
            else if(value <= byte.MinValue)
            {
                WriteRaw8(MsgPackCode.UInt8);
                WriteRaw8((byte)value);
            }
            else if(value <= ushort.MaxValue)
            {
                WriteRaw8(MsgPackCode.UInt16);
                WriteRawBigEndian16((ushort)value);
            }
            else if(value <= uint.MaxValue)
            {
                WriteRaw8(MsgPackCode.UInt32);
                WriteRawBigEndian32((uint)value);
            }
            else
            {
                WriteRaw8(MsgPackCode.UInt64);
                WriteRawBigEndian64(value);
            }
        }

        public void WriteInteger(long value)
        {
            if (value >= MsgPackCode.MinFixInt && value <= MsgPackCode.MaxFixInt)
            {
                WriteRaw8((byte)value);
            }
            else if(value >= sbyte.MinValue && value <= sbyte.MinValue)
            {
                WriteRaw8(MsgPackCode.Int8);
                WriteRaw8((byte)value);
            }
            else if( value >= short.MinValue && value <= short.MaxValue)
            {
                WriteRaw8(MsgPackCode.Int16);
                WriteRawBigEndian16((ushort)value);
            }
            else if(value >= int.MinValue && value <= int.MaxValue)
            {
                WriteRaw8(MsgPackCode.Int32);
                WriteRawBigEndian32((uint)value);
            }
            else
            {
                WriteRaw8(MsgPackCode.Int64);
                WriteRawBigEndian64((ulong)value);
            }
        }
        
        public void WriteSingle(float value)
        {
            WriteRaw8(MsgPackCode.Float32);
            WriteRawBigEndian32(*(uint*)&value);
        }
        public void WriteDouble(double value)
        {
            WriteRaw8(MsgPackCode.Float64);
            WriteRawBigEndian64(*(ulong*)&value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteRaw8(byte value) => _buffer.Add(value);

        public void WriteRawBigEndian16(ushort value)
        {
            byte* data = stackalloc byte[2];
            data[0] = (byte) (value >> 8);
            data[1] = (byte) (value >> 0);
            _buffer.AddRange(data, 2);
        }

        public void WriteRawBigEndian32(uint value)
        {
            byte* data = stackalloc byte[4];
            data[0] = (byte) (value >> 24);
            data[1] = (byte) (value >> 16);
            data[2] = (byte) (value >> 8);
            data[3] = (byte) (value >> 0);
            _buffer.AddRange(data, 4);
        }

        public void WriteRawBigEndian64(ulong value)
        {
            byte* data = stackalloc byte[8];
            data[0] = (byte) (value >> 56);
            data[1] = (byte) (value >> 48);
            data[2] = (byte) (value >> 40);
            data[3] = (byte) (value >> 32);
            data[4] = (byte) (value >> 24);
            data[5] = (byte) (value >> 16);
            data[6] = (byte) (value >> 8);
            data[7] = (byte) (value >> 0);
            _buffer.AddRange(data, 8);
        }

        public NativeArray<byte> ToArray(Allocator allocator) => new NativeArray<byte>(_buffer.AsArray(), allocator);
    }
}