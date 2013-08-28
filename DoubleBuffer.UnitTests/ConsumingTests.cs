using System.Threading;
using DoubleBuffer;
using DoubleBuffer.UnitTests;
using NUnit.Framework;

namespace BufferController.UnitTests
{
    [TestFixture]
    public class ConsumingTests
    {
        private BufferController<Event<long>> _doubleBuffer;

        [Test]
        public void should_publish_data_when_added()
        {
            const int writeCount = 999;
            var consumedValue = 0L;
            var mre = new ManualResetEvent(false);
            var consumeHandler = new ConsumeEventHandler(writeCount, mre);
            _doubleBuffer = new BufferController<Event<long>>(1000, evnt =>
                {
                    consumedValue = evnt.Value;
                    consumeHandler.OnNext(evnt);
                });

            new Thread(() => _doubleBuffer.Start()).Start();

            for (int data = 0; data <= writeCount; data++)
            {
                _doubleBuffer.Add(new Event<long>(data));
            }

            mre.WaitOne();
            Assert.AreEqual(writeCount, consumeHandler.CurrentValue);
        }
    }
}
