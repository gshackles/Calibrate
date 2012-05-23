using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Calibrate.Gears;
using Calibrate.Injectors;
using TinyIoC;
using Android.Runtime;

namespace Calibrate.Applications
{
    public class CalibrateApplication : Application
    {
        public IInjector Injector { get; private set; }
		public TinyIoCContainer Container { get; private set; }

        public CalibrateApplication(IntPtr handle, JniHandleOwnership transfer) 
			: base(handle, transfer)
        {
            Container = new TinyIoCContainer();
            Injector = GetInjector();
        }

        protected virtual IInjector GetInjector()
        {
            return new CachedInjector();
        }
        
        protected virtual IEnumerable<IGear> GetGears()
        {
            return null;
        }

        private void spinGears()
        {
            foreach (var gear in GetGears() ?? new List<IGear>())
            {
                gear.Spin(Container);
            }
        }
    }
}