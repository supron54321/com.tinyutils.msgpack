using NUnit.Framework;
using Unity.Collections;

namespace TinyMsgPack.Tests
{
    public class IntegerTests
    {
        [Test]
        public void Int8Test()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteInteger(sbyte.MinValue);
            writer.WriteInteger(sbyte.MaxValue);
            writer.WriteInteger((sbyte)0);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadInt8(), Is.EqualTo(sbyte.MinValue));
            Assert.That(reader.ReadInt8(), Is.EqualTo(sbyte.MaxValue));
            Assert.That(reader.ReadInt8(), Is.EqualTo(0));
        }
        
        [Test]
        public void UInt8Test()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteInteger(byte.MinValue);
            writer.WriteInteger(byte.MaxValue);
            writer.WriteInteger((byte)0);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadUInt8(), Is.EqualTo(byte.MinValue));
            Assert.That(reader.ReadUInt8(), Is.EqualTo(byte.MaxValue));
            Assert.That(reader.ReadUInt8(), Is.EqualTo(0));
        }
        [Test]
        public void Int16Test()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteInteger(short.MinValue);
            writer.WriteInteger(short.MaxValue);
            writer.WriteInteger((short)0);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadInt16(), Is.EqualTo(short.MinValue));
            Assert.That(reader.ReadInt16(), Is.EqualTo(short.MaxValue));
            Assert.That(reader.ReadInt16(), Is.EqualTo(0));
        }
        
        [Test]
        public void UInt16Test()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteInteger(ushort.MinValue);
            writer.WriteInteger(ushort.MaxValue);
            writer.WriteInteger((ushort)0);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadUInt16(), Is.EqualTo(ushort.MinValue));
            Assert.That(reader.ReadUInt16(), Is.EqualTo(ushort.MaxValue));
            Assert.That(reader.ReadUInt16(), Is.EqualTo(0));
        }
        
        
        [Test]
        public void Int32Test()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteInteger(int.MinValue);
            writer.WriteInteger(int.MaxValue);
            writer.WriteInteger((int)0);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadInt32(), Is.EqualTo(int.MinValue));
            Assert.That(reader.ReadInt32(), Is.EqualTo(int.MaxValue));
            Assert.That(reader.ReadInt32(), Is.EqualTo(0));
        }
        
        [Test]
        public void UInt32Test()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteInteger(uint.MinValue);
            writer.WriteInteger(uint.MaxValue);
            writer.WriteInteger((uint)0);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadUInt32(), Is.EqualTo(uint.MinValue));
            Assert.That(reader.ReadUInt32(), Is.EqualTo(uint.MaxValue));
            Assert.That(reader.ReadUInt32(), Is.EqualTo(0));
        }
        
        
        [Test]
        public void Int64Test()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteInteger(long.MinValue);
            writer.WriteInteger(long.MaxValue);
            writer.WriteInteger((long)0);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadInt64(), Is.EqualTo(long.MinValue));
            Assert.That(reader.ReadInt64(), Is.EqualTo(long.MaxValue));
            Assert.That(reader.ReadInt64(), Is.EqualTo(0));
        }
        
        [Test]
        public void UInt64Test()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteInteger(ulong.MinValue);
            writer.WriteInteger(ulong.MaxValue);
            writer.WriteInteger((ulong)0);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadUInt64(), Is.EqualTo(ulong.MinValue));
            Assert.That(reader.ReadUInt64(), Is.EqualTo(ulong.MaxValue));
            Assert.That(reader.ReadUInt64(), Is.EqualTo(0));
        }
    }
}