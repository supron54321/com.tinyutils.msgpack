using NUnit.Framework;
using Unity.Collections;

namespace TinyMsgPack.Tests
{
    public class FloatingPointTests
    {
        [Test]
        public void SingleTest()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteSingle(float.MinValue);
            writer.WriteSingle(float.MaxValue);
            writer.WriteSingle(float.Epsilon);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadSingle(), Is.EqualTo(float.MinValue));
            Assert.That(reader.ReadSingle(), Is.EqualTo(float.MaxValue));
            Assert.That(reader.ReadSingle(), Is.EqualTo(float.Epsilon));
        }
        
        [Test]
        public void DoubleTest()
        {
            MsgPackWriter writer = new MsgPackWriter(Allocator.Temp);
            writer.WriteDouble(double.MinValue);
            writer.WriteDouble(double.MaxValue);
            writer.WriteDouble(double.Epsilon);
            
            MsgPackReader reader = new MsgPackReader(writer.ToArray(Allocator.Temp));
            Assert.That(reader.ReadDouble(), Is.EqualTo(double.MinValue));
            Assert.That(reader.ReadDouble(), Is.EqualTo(double.MaxValue));
            Assert.That(reader.ReadDouble(), Is.EqualTo(double.Epsilon));
        }
    }
}