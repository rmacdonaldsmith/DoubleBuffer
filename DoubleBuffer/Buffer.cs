using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BufferController
{
    public sealed class Buffer<T>
    {
        private readonly Action<T> _consumerCallback;
        private readonly T[] _buffer;
        private int _capacityUsed = 0;

        public Buffer(int capacity, Action<T> consumerCallback)
        {
            _consumerCallback = consumerCallback;
            _buffer = new T[capacity];
        }

        public int CapacityUsed
        {
            get { return _capacityUsed; }
        }

        public bool HasData
        {
            get { return _capacityUsed != 0; }
        }

        public void Add(T data)
        {
            _buffer[_capacityUsed] = data;
            _capacityUsed++;
        }

        public void Consume()
        {
            for (int i = 0; i < _capacityUsed; i++)
            {
                _consumerCallback(_buffer[i]);
            }

            _capacityUsed = 0;
        }
    }
}
