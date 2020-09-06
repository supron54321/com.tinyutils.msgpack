using NUnit.Framework;
using Unity.Collections;

namespace TinyMsgPack.Tests
{
    public class RawBigEndianTests
    {
        [Test]
        public void Raw8()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteRaw8(byte.MinValue);
            writer.WriteRaw8(byte.MaxValue);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadRaw8(), Is.EqualTo(byte.MinValue));
            Assert.That(reader.ReadRaw8(), Is.EqualTo(byte.MaxValue));
        }
        
        [Test]
        public void Raw16()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteRawBigEndian16(ushort.MinValue);
            writer.WriteRawBigEndian16(ushort.MaxValue);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadRawBigEndian16(), Is.EqualTo(ushort.MinValue));
            Assert.That(reader.ReadRawBigEndian16(), Is.EqualTo(ushort.MaxValue));
        }
        
        [Test]
        public void Raw32()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteRawBigEndian32(uint.MinValue);
            writer.WriteRawBigEndian32(uint.MaxValue);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadRawBigEndian32(), Is.EqualTo(uint.MinValue));
            Assert.That(reader.ReadRawBigEndian32(), Is.EqualTo(uint.MaxValue));
        }
        
        [Test]
        public void Raw64()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteRawBigEndian64(ulong.MinValue);
            writer.WriteRawBigEndian64(ulong.MaxValue);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadRawBigEndian64(), Is.EqualTo(ulong.MinValue));
            Assert.That(reader.ReadRawBigEndian64(), Is.EqualTo(ulong.MaxValue));
        }
    }
}