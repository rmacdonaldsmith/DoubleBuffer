using BufferController;
using DoubleBuffer.Switching;
using NUnit.Framework;

namespace DoubleBuffer.UnitTests
{
    [TestFixture]
    public class InterlockedSwitchingStrategyTests
    {
        [Test]
        public void BuffersSwitched()
        {
            const int capacity = 100;
            var buffer1 = new Buffer<int>(capacity, i => {});
            var buffer2 = new Buffer<int>(capacity, i => {});
            ISwitchingStrategy<int> switchingStrategy = new InterlockedSwitchingStrategy<int>(new []{buffer1, buffer2});

            Assert.AreSame(buffer1, switchingStrategy.FrontBuffer);
            Assert.AreSame(buffer2, switchingStrategy.BackBuffer);

            switchingStrategy.Switch();

            Assert.AreSame(buffer2, switchingStrategy.FrontBuffer);
            Assert.AreSame(buffer1, switchingStrategy.BackBuffer);
        }
    }
}
