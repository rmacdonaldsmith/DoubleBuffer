using BufferController;

namespace DoubleBuffer.Switching
{
    public interface ISwitchingStrategy<T>
    {
        Buffer<T> FrontBuffer { get; }
        Buffer<T> BackBuffer { get; }
        void Switch();
    }
}
