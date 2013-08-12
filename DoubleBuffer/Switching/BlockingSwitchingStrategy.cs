using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BufferController;

namespace DoubleBuffer.Switching
{
    public class BlockingSwitchingStrategy<T> : ISwitchingStrategy<T>
    {
        private Buffer<T> _frontBuffer;
        private Buffer<T> _backBuffer;
        private readonly object _lock = new object();

        public BlockingSwitchingStrategy(Buffer<T> frontBuffer, Buffer<T> backBuffer)
        {
            _frontBuffer = frontBuffer;
            _backBuffer = backBuffer;
        }

        public Buffer<T> FrontBuffer
        {
            get
            {
                lock (_lock)
                {
                    return _frontBuffer;
                }
            }
        }

        public Buffer<T> BackBuffer
        {
            get
            {
                lock (_lock)
                {
                    return _backBuffer;
                }
            }
        }

        public void Switch()
        {
            lock (_lock)
            {
                
            }
        }
    }
}
