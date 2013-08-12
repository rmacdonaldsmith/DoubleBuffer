using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BufferController;

namespace DoubleBuffer.Switching
{
    public class BusySpinWaitStrategy<T> : ISwitchingStrategy<T>
    {
        private Buffer<T> _frontBuffer;
        private Buffer<T> _backBuffer;

        public BusySpinWaitStrategy(Buffer<T> frontBuffer, Buffer<T> backBuffer)
        {
            _frontBuffer = frontBuffer;
            _backBuffer = backBuffer;
        }

        public Buffer<T> FrontBuffer
        {
            get { return _frontBuffer; }
        }

        public Buffer<T> BackBuffer
        {
            get { return _backBuffer; }
        }

        public void Switch()
        {

        }
    }
}
