using System.Text;
using NUnit.Framework;
using Unity.Collections;

namespace TinyMsgPack.Tests
{
    public class StringSerialization
    {
        [Test]
        public void StringFixedLen()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            var testString = GetRandomizedAsciiCharactersString(4);
            writer.WriteString(testString);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadString(), Is.EqualTo(testString));
        }
        
        [Test]
        public void StringLen8()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            var testString = GetRandomizedAsciiCharactersString(sbyte.MaxValue);
            writer.WriteString(testString);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadString(), Is.EqualTo(testString));
        }
        [Test]
        public void StringLen16()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            var testString = GetRandomizedAsciiCharactersString(short.MaxValue);
            writer.WriteString(testString);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadString(), Is.EqualTo(testString));
        }
        
        [Test]
        public void StringLen32()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            var testString = GetRandomizedAsciiCharactersString(ushort.MaxValue+1);
            writer.WriteString(testString);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadString(), Is.EqualTo(testString));
        }
        
        [Test]
        public void NullString()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteNil();
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadString(), Is.Null);
        }
        
        [Test]
        public void LargeUnicodeString()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            var testString = GetRandomizedUnicodeCharactersString(ushort.MaxValue+1);
            writer.WriteString(testString);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadString(), Is.EqualTo(testString));
        }

        string GetRandomizedAsciiCharactersString(int len)
        {
            string templateString = "qwertyuiopASDFGHJKL";
            return GetRandomizedString(len, templateString);
        }

        private static string GetRandomizedString(int len, string templateString)
        {
            StringBuilder builder = new StringBuilder(len);
            for (int i = 0; i < builder.Length; i++)
            {
                builder.Append(templateString[i % templateString.Length]);
            }

            return builder.ToString();
        }


        string GetRandomizedUnicodeCharactersString(int len)
        {
            string templateString = "☄☈★☔☚☢⛄";
            StringBuilder builder = new StringBuilder(len);
            for (int i = 0; i < builder.Length; i++)
            {
                builder.Append(templateString[i % templateString.Length]);
            }

            return builder.ToString();
        }
    }
}