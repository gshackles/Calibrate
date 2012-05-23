using Android.App;
using Android.OS;
using Calibrate.Applications;
using Calibrate.Injectors;

namespace Calibrate.Activities
{
    public abstract class CalibrateActivity : Activity
    {
        public IInjector Injector { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Injector = GetInjector();
            Injector.InjectStrings(this);
            Injector.InjectExtras(this);
            Injector.InjectFromContainer(this, ((CalibrateApplication)Application).Container);
        }

        protected override void OnStart()
        {
            base.OnStart();

            Injector.InjectPreferences(this);
        }

        protected virtual IInjector GetInjector()
        {
            return ((CalibrateApplication)Application).Injector;
        }

        public override void SetContentView(int layoutResID)
        {
            base.SetContentView(layoutResID);

            Injector.InjectViews(this);
        }
    }
}