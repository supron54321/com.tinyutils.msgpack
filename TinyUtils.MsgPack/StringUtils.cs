using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace TinyMsgPack
{
    public static unsafe class StringUtils
    {
        static UTF8Encoding _encoding = new UTF8Encoding();

        public static int GetEncodedLength(string str)
        {
            return _encoding.GetByteCount(str);
        }

        public static void WriteEncodedString(string str, byte* dstBuffer, int encodedLen)
        {
            fixed (char* strPtr = str)
            {
                _encoding.GetBytes(strPtr, str.Length, dstBuffer, encodedLen);
            }
        }
        
        public static string Decode(NativeSlice<byte> slice)
        {
            return _encoding.GetString((byte*)slice.GetUnsafePtr(), slice.Length);
        }
    }
}