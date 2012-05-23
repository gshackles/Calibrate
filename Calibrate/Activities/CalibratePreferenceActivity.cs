using Android.OS;
using Android.Preferences;
using Calibrate.Applications;
using Calibrate.Injectors;

namespace Calibrate.Activities
{
    public abstract class CalibratePreferenceActivity : PreferenceActivity
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

        public override void AddPreferencesFromResource(int preferencesResId)
        {
            base.AddPreferencesFromResource(preferencesResId);

            Injector.InjectViews(this);
        }
    }
}