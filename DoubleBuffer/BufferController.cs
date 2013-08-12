using System;
using System.Threading;
using BufferController;

namespace DoubleBuffer
{
    public class BufferController<T>
    {
        private readonly int _capacity;
        private readonly Action<T> _consumerCallBack;
        private readonly Buffer<T> _firstBuffer;
        private readonly Buffer<T> _secondBuffer;
        private Buffer<T> _frontBufferPointer;
        private CurrentBuffer _frontBuffer;
        private readonly object _producerLock = new object(); 

        public BufferController(int capacity, Action<T> consumerCallBack)
        {
            _capacity = capacity;
            _consumerCallBack = consumerCallBack;
            _firstBuffer = new Buffer<T>(_capacity, _consumerCallBack);
            _secondBuffer = new Buffer<T>(_capacity, _consumerCallBack);
            _frontBuffer = CurrentBuffer.First;
            _frontBufferPointer = _firstBuffer;
        }

        public void Consume()
        {
            var frontBuffer = GetFrontBuffer();
            if(frontBuffer.HasData)
                frontBuffer.Consume();

            Switch();
        }

        public void Switch()
        {
            if(_frontBuffer == CurrentBuffer.First)
                Interlocked.Exchange<Buffer<T>>(ref _frontBufferPointer, _secondBuffer);
            else
            {
                Interlocked.Exchange<Buffer<T>>(ref _frontBufferPointer, _firstBuffer);
            }
        }

        public void Add(T data)
        {
            //lock the producer while we switch the buffer
            lock (_producerLock)
            {
                GetFrontBuffer().Add(data);
            }
        }

        public enum CurrentBuffer
        {
            First,
            Second
        }

        private Buffer<T> GetFrontBuffer()
        {
            _frontBufferPointer = CurrentBuffer.First ? _firstBuffer : _secondBuffer;
            return _frontBuffer == CurrentBuffer.First ? _firstBuffer : _secondBuffer;
        }
    }
}
