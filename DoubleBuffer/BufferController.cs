using System;
using System.Collections.Generic;
using BufferController;
using DoubleBuffer.Switching;

namespace DoubleBuffer
{
    public class BufferController<T>
    {
        private bool _run = false;
        private readonly int _capacity;
        private readonly Action<T> _consumerCallBack;
        private readonly ISwitchingStrategy<T> _switchingStrategy;  

        public BufferController(int capacity, Action<T> consumerCallBack)
        {
            _capacity = capacity;
            _consumerCallBack = consumerCallBack;
            _switchingStrategy = new InterlockedSwitchingStrategy<T>(new[]
                {
                    new Buffer<T>(_capacity, _consumerCallBack),
                    new Buffer<T>(_capacity, _consumerCallBack)
                });
        }

        public void Start()
        {
            _run = true;
            while (_run)
            {
                if (_switchingStrategy.BackBuffer.HasData)
                    _switchingStrategy.BackBuffer.Consume();

                Switch();
            }
        }

        public void Stop()
        {
            _run = false;
        }

        private void Switch()
        {
            _switchingStrategy.Switch();
        }

        public void Add(T data)
        {
            _switchingStrategy.FrontBuffer.Add(data);
        }
    }
}
