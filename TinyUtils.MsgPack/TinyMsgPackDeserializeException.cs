using System;

namespace TinyMsgPack
{
    public class TinyMsgPackDeserializeException : InvalidOperationException
    {
        public TinyMsgPackDeserializeException(string msg) : base(msg)
        {
            
        }
    }
}