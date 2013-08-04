using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DoubleBuffer.UnitTests
{
    [TestFixture]
    public class ConsumingTests
    {

        [Test]
        public void should_publish_data_when_added()
        {
            var doubleBuffer = new DoubleBuffer(1001);
            const int writeCount = 1000;
            var publishedCounter = 0;
            doubleBuffer.ConsumeDelegate(o => publishedCounter++);

            for (int data = 0; data < writeCount; data++)
            {
                doubleBuffer.Add(data);
            }

            Assert.AreEqual(writeCount, publishedCounter);
        }
    }
}
