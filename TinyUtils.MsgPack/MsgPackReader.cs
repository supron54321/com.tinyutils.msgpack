using Unity.Collections;

namespace TinyMsgPack
{
    public unsafe class MsgPackReader
    {
        private NativeSlice<byte> _buffer;
        private int _offset;

        public MsgPackReader(NativeSlice<byte> slice)
        {
            _buffer = slice;
            _offset = 0;
        }

        public MsgPackReader(NativeArray<byte> array)
        {
            _buffer = array;
            _offset = 0;
        }

        public bool TryReadNil()
        {
            bool isNil = ReadNext() == MsgPackCode.Nil;
            if(!isNil)
                _offset--;
            return isNil;
        }

        public bool ReadArrayHeader(out int length)
        {
            length = 0;
            byte code = ReadNext();

            if (code == MsgPackCode.Nil)
                return false;
            if (code >= MsgPackCode.MinFixArray && code <= MsgPackCode.MaxFixArray)
                length = code - MsgPackCode.MinFixArray;
            else if (code == MsgPackCode.Array16)
                length = ReadRawBigEndian16();
            else if (code == MsgPackCode.Array32)
                length = (int) ReadRawBigEndian32();
            else
                throw new TinyMsgPackDeserializeException($"MsgPackReader - Invalid code {code}. Expected array.");

            return true;
        }

        public bool ReadMapHeader(out int length)
        {
            length = 0;
            byte code = ReadNext();

            if (code == MsgPackCode.Nil)
                return false;
            if (code >= MsgPackCode.MinFixMap && code <= MsgPackCode.MaxFixMap)
                length = code - MsgPackCode.MinFixMap;
            else if (code == MsgPackCode.Map16)
                length = ReadRawBigEndian16();
            else if (code == MsgPackCode.Map32)
                length = (int) ReadRawBigEndian32();
            else
                throw new TinyMsgPackDeserializeException($"MsgPackReader - Invalid code {code}. Expected map.");

            return true;
        }

        public string ReadString()
        {
            byte code = ReadNext();
            if (code == MsgPackCode.Nil)
                return null;

            int length = 0;
            if (code >= MsgPackCode.MinFixStr && code <= MsgPackCode.MaxFixStr)
                length = code - MsgPackCode.MinFixStr;
            else if (code == MsgPackCode.Str8)
                length = ReadRaw8();
            else if (code == MsgPackCode.Str16)
                length = ReadRawBigEndian16();
            else if (code == MsgPackCode.Str32)
                length = (int) ReadRawBigEndian32();
            else
                throw new TinyMsgPackDeserializeException(
                        $"{nameof(ReadString)} - invalid code {code}. Expected string.");
            
            if (length < 0)
                throw new TinyMsgPackDeserializeException(
                    $"{nameof(ReadString)} - invalid string length {length}");

            return StringUtils.Decode(ReadNext(length));
        }

        public byte ReadUInt8()
        {
            byte code = ReadNext();
            if (code <= MsgPackCode.MaxFixInt)
                return code;

            if (code == MsgPackCode.Float32)
            {
                var value = ReadRawBigEndian32();
                return (byte) *(float*) &value;
            }

            if (code == MsgPackCode.Float64)
            {
                var value = ReadRawBigEndian64();
                return (byte) *(double*) &value;
            }

            if (code == MsgPackCode.Int8)
                return ReadRaw8();
            if (code == MsgPackCode.UInt8)
                return ReadRaw8();

            if (code == MsgPackCode.Int16)
                return (byte) ReadRawBigEndian16();
            if (code == MsgPackCode.UInt16)
                return (byte) ReadRawBigEndian16();

            if (code == MsgPackCode.Int32)
                return (byte) ReadRawBigEndian32();
            if (code == MsgPackCode.UInt32)
                return (byte) ReadRawBigEndian32();

            if (code == MsgPackCode.Int64)
                return (byte) ReadRawBigEndian64();
            if (code == MsgPackCode.UInt64)
                return (byte) ReadRawBigEndian64();

            throw new TinyMsgPackDeserializeException($"MsgPackReader - Invalid code {code}. Expected number");
        }


        public ushort ReadUInt16()
        {
            byte code = ReadNext();
            if (code <= MsgPackCode.MaxFixInt)
                return code;

            if (code == MsgPackCode.Float32)
            {
                var value = ReadRawBigEndian32();
                return (ushort) *(float*) &value;
            }

            if (code == MsgPackCode.Float64)
            {
                var value = ReadRawBigEndian64();
                return (ushort) *(double*) &value;
            }

            if (code == MsgPackCode.Int8)
                return ReadRaw8();
            if (code == MsgPackCode.UInt8)
                return ReadRaw8();

            if (code == MsgPackCode.Int16)
                return ReadRawBigEndian16();
            if (code == MsgPackCode.UInt16)
                return ReadRawBigEndian16();

            if (code == MsgPackCode.Int32)
                return (ushort) ReadRawBigEndian32();
            if (code == MsgPackCode.UInt32)
                return (ushort) ReadRawBigEndian32();

            if (code == MsgPackCode.Int64)
                return (ushort) ReadRawBigEndian64();
            if (code == MsgPackCode.UInt64)
                return (ushort) ReadRawBigEndian64();

            throw new TinyMsgPackDeserializeException($"MsgPackReader - Invalid code {code}. Expected number");
        }

        public uint ReadUInt32()
        {
            byte code = ReadNext();
            if (code <= MsgPackCode.MaxFixInt)
                return code;

            if (code == MsgPackCode.Float32)
            {
                var value = ReadRawBigEndian32();
                return (uint) *(float*) &value;
            }

            if (code == MsgPackCode.Float64)
            {
                var value = ReadRawBigEndian64();
                return (uint) *(double*) &value;
            }

            if (code == MsgPackCode.Int8)
                return ReadRaw8();
            if (code == MsgPackCode.UInt8)
                return ReadRaw8();

            if (code == MsgPackCode.Int16)
                return ReadRawBigEndian16();
            if (code == MsgPackCode.UInt16)
                return ReadRawBigEndian16();

            if (code == MsgPackCode.Int32)
                return ReadRawBigEndian32();
            if (code == MsgPackCode.UInt32)
                return ReadRawBigEndian32();

            if (code == MsgPackCode.Int64)
                return (uint) ReadRawBigEndian64();
            if (code == MsgPackCode.UInt64)
                return (uint) ReadRawBigEndian64();

            throw new TinyMsgPackDeserializeException($"MsgPackReader - Invalid code {code}. Expected number");
        }


        public ulong ReadUInt64()
        {
            byte code = ReadNext();
            if (code <= MsgPackCode.MaxFixInt)
                return code;

            if (code == MsgPackCode.Float32)
            {
                var value = ReadRawBigEndian32();
                return (ulong) *(float*) &value;
            }

            if (code == MsgPackCode.Float64)
            {
                var value = ReadRawBigEndian64();
                return (ulong) *(double*) &value;
            }

            if (code == MsgPackCode.Int8)
                return ReadRaw8();
            if (code == MsgPackCode.UInt8)
                return ReadRaw8();

            if (code == MsgPackCode.Int16)
                return ReadRawBigEndian16();
            if (code == MsgPackCode.UInt16)
                return ReadRawBigEndian16();

            if (code == MsgPackCode.Int32)
                return ReadRawBigEndian32();
            if (code == MsgPackCode.UInt32)
                return ReadRawBigEndian32();

            if (code == MsgPackCode.Int64)
                return ReadRawBigEndian64();
            if (code == MsgPackCode.UInt64)
                return ReadRawBigEndian64();

            throw new TinyMsgPackDeserializeException($"MsgPackReader - Invalid code {code}. Expected number");
        }


        public sbyte ReadInt8() => (sbyte) ReadUInt8();
        public short ReadInt16() => (short) ReadUInt16();
        public int ReadInt32() => (int) ReadUInt32();
        public long ReadInt64() => (long) ReadUInt64();

        public float ReadSingle()
        {
            byte code = ReadNext();
            if (code <= MsgPackCode.MaxFixInt)
                return code;

            if (code == MsgPackCode.Float32)
            {
                var value = ReadRawBigEndian32();
                return *(float*) &value;
            }

            if (code == MsgPackCode.Float64)
            {
                var value = ReadRawBigEndian64();
                return (float) *(double*) &value;
            }

            if (code == MsgPackCode.Int8)
                return (sbyte) ReadRaw8();
            if (code == MsgPackCode.UInt8)
                return ReadRaw8();

            if (code == MsgPackCode.Int16)
                return (short) ReadRawBigEndian16();
            if (code == MsgPackCode.UInt16)
                return ReadRawBigEndian16();

            if (code == MsgPackCode.Int32)
                return (int) ReadRawBigEndian32();
            if (code == MsgPackCode.UInt32)
                return ReadRawBigEndian32();

            if (code == MsgPackCode.Int64)
                return (long) ReadRawBigEndian64();
            if (code == MsgPackCode.UInt64)
                return ReadRawBigEndian64();

            throw new TinyMsgPackDeserializeException($"MsgPackReader - Invalid code {code}. Expected number");
        }

        public double ReadDouble()
        {
            byte code = ReadNext();
            if (code <= MsgPackCode.MaxFixInt)
                return code;

            if (code == MsgPackCode.Float32)
            {
                var value = ReadRawBigEndian32();
                return *(float*) &value;
            }

            if (code == MsgPackCode.Float64)
            {
                var value = ReadRawBigEndian64();
                return *(double*) &value;
            }

            if (code == MsgPackCode.Int8)
                return (sbyte) ReadRaw8();
            if (code == MsgPackCode.UInt8)
                return ReadRaw8();

            if (code == MsgPackCode.Int16)
                return (short) ReadRawBigEndian16();
            if (code == MsgPackCode.UInt16)
                return ReadRawBigEndian16();

            if (code == MsgPackCode.Int32)
                return (int) ReadRawBigEndian32();
            if (code == MsgPackCode.UInt32)
                return ReadRawBigEndian32();

            if (code == MsgPackCode.Int64)
                return (long) ReadRawBigEndian64();
            if (code == MsgPackCode.UInt64)
                return ReadRawBigEndian64();

            throw new TinyMsgPackDeserializeException($"MsgPackReader - Invalid code {code}. Expected number");
        }

        public byte ReadRaw8() => ReadNext();

        public ushort ReadRawBigEndian16()
        {
            var data = ReadNext(2);
            return (ushort) ((data[0] << 8)
                             | (data[1]));
        }

        public uint ReadRawBigEndian32()
        {
            var data = ReadNext(4);
            return (uint) ((data[0] << 24)
                           | (data[1] << 16)
                           | (data[2] << 8)
                           | (data[3]));
        }

        public ulong ReadRawBigEndian64()
        {
            var data = ReadNext(8);
            return ((ulong) data[0] << 56)
                   | ((ulong) data[1] << 48)
                   | ((ulong) data[2] << 40)
                   | ((ulong) data[3] << 32)
                   | ((ulong) data[4] << 24)
                   | ((ulong) data[5] << 16)
                   | ((ulong) data[6] << 8)
                   | data[7];
        }

        private byte ReadNext()
        {
            if (_offset + 1 > _buffer.Length)
                throw new TinyMsgPackDeserializeException("MsgPackReader - out of buffer range");
            var ret = _buffer[_offset];
            _offset++;
            return ret;
        }

        private NativeSlice<byte> ReadNext(int length)
        {
            if (_offset + length > _buffer.Length)
                throw new TinyMsgPackDeserializeException("MsgPackReader - out of buffer range");
            var slice = _buffer.Slice(_offset, length);
            _offset += length;
            return slice;
        }
    }
}