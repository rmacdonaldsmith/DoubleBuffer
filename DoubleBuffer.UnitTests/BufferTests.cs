using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BufferController.UnitTests
{
    [TestFixture]
    public class BufferTests
    {
        [Test]
        public void should_callback_when_done_consuming()
        {
            var numberOfElements = 1000;
            var consumedCount = 0;
            var buffer = new Buffer<int>(1000, i => consumedCount++);

            for (int i = 0; i < numberOfElements; i++)
            {
                buffer.Add(i);
            }

            var usedCount = buffer.CapacityUsed;

            Assert.AreEqual(usedCount, numberOfElements);
            Assert.IsTrue(buffer.HasData);

            buffer.Consume();

            Assert.IsFalse(buffer.HasData);
            Assert.AreEqual(consumedCount, numberOfElements);
        }


    }
}
