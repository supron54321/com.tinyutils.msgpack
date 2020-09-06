using NUnit.Framework;
using Unity.Collections;

namespace TinyMsgPack.Tests
{
    public class ArrayAndMap
    {
        [Test]
        public void TestFixedArrayHeader()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteArrayHeader(0);
            writer.WriteArrayHeader(0xF);

            var asArray = writer.ToArray(Allocator.Temp);
            Assert.That(asArray.Length, Is.EqualTo(2));    // such small numbers should be stored as fixed numbers
            
            MsgPackReader reader = new MsgPackReader(asArray);
            Assert.That(reader.ReadArrayHeader(out var len1), Is.True);
            Assert.That(len1, Is.EqualTo(0));
            
            Assert.That(reader.ReadArrayHeader(out var len2), Is.True);
            Assert.That(len2, Is.EqualTo(0xF));
        }
        
        [Test]
        public void TestArrayHeader16()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteArrayHeader(ushort.MaxValue);

            var asArray = writer.ToArray(Allocator.Temp);
            Assert.That(asArray.Length, Is.EqualTo(3));    // should be written as 3 bytes (code + short len)
            
            MsgPackReader reader = new MsgPackReader(asArray);
            Assert.That(reader.ReadArrayHeader(out var len), Is.True);
            Assert.That(len, Is.EqualTo(ushort.MaxValue));
        }
        
        [Test]
        public void TestArrayHeader32()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteArrayHeader(int.MaxValue);

            var asArray = writer.ToArray(Allocator.Temp);
            Assert.That(asArray.Length, Is.EqualTo(5));    // should be written as 5 bytes (code + int len)
            
            MsgPackReader reader = new MsgPackReader(asArray);
            Assert.That(reader.ReadArrayHeader(out var len), Is.True);
            Assert.That(len, Is.EqualTo(int.MaxValue));
        }
        
        
        [Test]
        public void TestNullArray()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteNil();

            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadArrayHeader(out var len), Is.False);
        }
        
        
        
        [Test]
        public void TestFixedMapHeader()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteMapHeader(0);
            writer.WriteMapHeader(0xF);

            var asArray = writer.ToArray(Allocator.Temp);
            Assert.That(asArray.Length, Is.EqualTo(2));    // such small numbers should be stored as fixed numbers
            
            MsgPackReader reader = new MsgPackReader(asArray);
            Assert.That(reader.ReadMapHeader(out var len1), Is.True);
            Assert.That(len1, Is.EqualTo(0));
            
            Assert.That(reader.ReadMapHeader(out var len2), Is.True);
            Assert.That(len2, Is.EqualTo(0xF));
        }
        
        [Test]
        public void TestMapHeader16()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteMapHeader(ushort.MaxValue);

            var asArray = writer.ToArray(Allocator.Temp);
            Assert.That(asArray.Length, Is.EqualTo(3));    // should be written as 3 bytes (code + short len)
            
            MsgPackReader reader = new MsgPackReader(asArray);
            Assert.That(reader.ReadMapHeader(out var len), Is.True);
            Assert.That(len, Is.EqualTo(ushort.MaxValue));
        }
        
        [Test]
        public void TestMapHeader32()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteMapHeader(int.MaxValue);

            var asArray = writer.ToArray(Allocator.Temp);
            Assert.That(asArray.Length, Is.EqualTo(5));    // should be written as 5 bytes (code + int len)
            
            MsgPackReader reader = new MsgPackReader(asArray);
            Assert.That(reader.ReadMapHeader(out var len), Is.True);
            Assert.That(len, Is.EqualTo(int.MaxValue));
        }
        
        
        [Test]
        public void TestNullMap()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteNil();

            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadMapHeader(out var len), Is.False);
        }
    }
}