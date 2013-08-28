using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using BufferController;

namespace DoubleBuffer.Switching
{
    public class InterlockedSwitchingStrategy<T> : ISwitchingStrategy<T>
    {
        private readonly List<Buffer<T>> _buffers;
        private int _frontBufferIndex = 0;

        public InterlockedSwitchingStrategy(IEnumerable<Buffer<T>> buffers)
        {
            _buffers = new List<Buffer<T>>(buffers);
        }

        private int BackBufferIndex
        {
            get { return _frontBufferIndex == 0 ? 1 : 0; }
        }

        public Buffer<T> FrontBuffer
        {
            get { return _buffers[_frontBufferIndex]; }
        }

        public Buffer<T> BackBuffer
        {
            get
            {
                return _buffers[BackBufferIndex];
            }
        }

        public void Switch()
        {
            if (_frontBufferIndex == 0)
                Interlocked.Increment(ref _frontBufferIndex);
            else
                Interlocked.Decrement(ref _frontBufferIndex);

            //Console.WriteLine("Front Buffer: " + _frontBufferIndex);
        }
    }
}
