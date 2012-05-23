using TinyIoC;

namespace Calibrate.Gears
{
    public interface IGear
    {
        void Spin(TinyIoCContainer container);
    }
}