using System;

namespace Calibrate.Gears
{
    public class TinyIoCContainerAdapter : IContainerAdapter
    {
        public void Register<T>(T value, string name = null) 
            where T : class
        {
            if (!string.IsNullOrEmpty(name))
            {
                TinyIoC.TinyIoCContainer.Current.Register<T>(value, name);
            }
            else
            {
                TinyIoC.TinyIoCContainer.Current.Register<T>(value);
            }
        }

        public object Resolve(Type type, string name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return TinyIoC.TinyIoCContainer.Current.Resolve(type, name);
            }
            else
            {
                return TinyIoC.TinyIoCContainer.Current.Resolve(type);
            }
        }

        public T Resolve<T>(string name = null)
            where T : class
        {
            if (!string.IsNullOrEmpty(name))
            {
                return TinyIoC.TinyIoCContainer.Current.Resolve<T>(name);
            }
            else
            {
                return TinyIoC.TinyIoCContainer.Current.Resolve<T>();
            }
        }
    }
}