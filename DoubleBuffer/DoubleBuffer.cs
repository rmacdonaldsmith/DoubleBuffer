using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleBuffer
{
    public class DoubleBuffer
    {
        private readonly int _capacity;
        private readonly Action<object> _consumerCallBack;
        private readonly object[] _firstBuffer;
        private readonly object[] _secondBuffer;
        private CurrentBuffer _currentBuffer;
        private readonly object _switchingLock = new object(); 

        public DoubleBuffer(int capacity, Action<object> consumerCallBack)
        {
            _capacity = capacity;
            _consumerCallBack = consumerCallBack;
            _firstBuffer = new object[_capacity];
            _secondBuffer = new object[_capacity];
            _currentBuffer = CurrentBuffer.First;
        }

        public void Consume()
        {
            //loop over buffer and publish the data
            var currentBuffer = GetCurrentBufferRef();
            for (int i = 0; i < 1000; i++)
            {
                _consumerCallBack(currentBuffer[i]);
            }

            Switch();
        }

        public void Switch()
        {
            //lock the producer thread and switch the buffers
            //so that the cumsumer can drain what the producer has added
            lock (_switchingLock)
            {
                
            }
        }

        public void Add(object data)
        {
            //lock on the switching lock so that the producer can be locked
            //while we switch the buffer
            lock (_switchingLock)
            {
                GetCurrentBufferRef()[0] = data;
                //Add(data, _currentBuffer);
            }
        }

        private void Add(object data, CurrentBuffer buffer)
        {
            if (buffer == CurrentBuffer.First)
            {
                _firstBuffer[0] = data;
            }
            else
            {
                _secondBuffer[0] = data;
            }
        }

        public enum CurrentBuffer
        {
            First,
            Second
        }

        private object[] GetCurrentBufferRef()
        {
            return _currentBuffer == CurrentBuffer.First ? _firstBuffer : _secondBuffer;
        }
    }
}
