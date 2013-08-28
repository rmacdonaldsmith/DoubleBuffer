using System.Threading;

namespace DoubleBuffer.UnitTests
{
    public sealed class Event<T>
    {
        private readonly T _value;

        public Event(T value)
        {
            _value = value;
        }

        public T Value
        {
            get { return _value; }
        }
    }

    public class ConsumeEventHandler
    {
        private long _expected;
        private ManualResetEvent _resetEvent;
        private long _currentValue;

        public ConsumeEventHandler(long expected, ManualResetEvent resetEvent)
        {
            _expected = expected;
            _resetEvent = resetEvent;
        }

        public long CurrentValue
        {
            get { return _currentValue; }
        }

        public void OnNext(Event<long> evnt)
        {
            _currentValue = evnt.Value;
            if (evnt.Value == _expected)
                _resetEvent.Set();
        }
    }
}
