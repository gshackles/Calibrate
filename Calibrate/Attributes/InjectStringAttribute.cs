using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Calibrate.Attributes
{
    public class InjectStringAttribute : BaseInjectAttribute
    {
        public int ResourceId { get; set; }

        public InjectStringAttribute()
        {
        }

        public InjectStringAttribute(int resourceId)
        {
            ResourceId = resourceId;
        }

        public override bool CanInjectType(Type type)
        {
            return type == typeof(string);
        }
    }
}