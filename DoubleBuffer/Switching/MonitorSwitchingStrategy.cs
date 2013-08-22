using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BufferController;

namespace DoubleBuffer.Switching
{
    public class MonitorSwitchingStrategy<T> : ISwitchingStrategy<T>
    {
        private readonly List<Buffer<T>> _buffers;
        private int _frontBufferIndex = 0;
        private int _backBufferIndex = 1;
        private readonly object _lock = new object();

        public MonitorSwitchingStrategy(Buffer<T> frontBuffer, Buffer<T> backBuffer)
        {
            _buffers = new List<Buffer<T>>(new []{frontBuffer, backBuffer});
        }

        public Buffer<T> FrontBuffer
        {
            get
            {
                lock (_lock)
                {
                    return _buffers[_frontBufferIndex];
                }
            }
        }

        public Buffer<T> BackBuffer
        {
            get
            {
                lock (_lock)
                {
                    return _buffers[_backBufferIndex];
                }
            }
        }

        public void Switch()
        {
            lock (_lock)
            {
                var tempIndex = _frontBufferIndex;
                _frontBufferIndex = _backBufferIndex;
                _backBufferIndex = tempIndex;
            }
        }
    }
}
