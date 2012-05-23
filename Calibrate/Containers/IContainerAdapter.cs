using System;

namespace Calibrate.Gears
{
    public interface IContainerAdapter
    {
        object Resolve(Type type, string name = null);
        T Resolve<T>(string name = null) where T : class;
    }
}
