using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoubleBuffer;
using NUnit.Framework;

namespace BufferController.UnitTests
{
    [TestFixture]
    public class ConsumingTests
    {

        [Test]
        public void should_publish_data_when_added()
        {
            var publishedCounter = 0; 
            var doubleBuffer = new BufferController<int>(1001, _ => publishedCounter++);
            const int writeCount = 1000;

            for (int data = 0; data < writeCount; data++)
            {
                doubleBuffer.Add(data);
            }

            Assert.AreEqual(writeCount, publishedCounter);
        }
    }
}
