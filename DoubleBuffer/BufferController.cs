using System;
using BufferController;
using DoubleBuffer.Switching;

namespace DoubleBuffer
{
    public class BufferController<T>
    {
        private readonly int _capacity;
        private readonly Action<T> _consumerCallBack;
        private readonly ISwitchingStrategy<T> _switchingStrategy;  

        public BufferController(int capacity, Action<T> consumerCallBack)
        {
            _capacity = capacity;
            _consumerCallBack = consumerCallBack;
            _switchingStrategy = new MonitorSwitchingStrategy<T>(new Buffer<T>(_capacity, _consumerCallBack),
                                                                 new Buffer<T>(_capacity, _consumerCallBack));
        }

        public void Consume()
        {
            if(_switchingStrategy.BackBuffer.HasData)
                _switchingStrategy.BackBuffer.Consume();

            Switch();
        }

        public void Switch()
        {
            _switchingStrategy.Switch();
        }

        public void Add(T data)
        {
            _switchingStrategy.FrontBuffer.Add(data);
        }

        public enum CurrentBuffer
        {
            First,
            Second
        }
    }
}
