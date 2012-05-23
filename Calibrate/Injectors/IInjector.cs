using Android.App;
using Android.Content;
using Calibrate.Gears;
using TinyIoC;

namespace Calibrate.Injectors
{
    public interface IInjector
    {
        void InjectFromContainer(object context, TinyIoCContainer container);
        void InjectStrings(ContextWrapper context);
        void InjectViews(Activity activity);
        void InjectExtras(Activity activity);
        void InjectPreferences(Context context);
    }
}
