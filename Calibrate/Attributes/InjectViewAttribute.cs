using System;
using Android.Views;

namespace Calibrate.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class InjectViewAttribute : BaseInjectAttribute
    {
        public int ResourceId { get; set; }

        public InjectViewAttribute()
        {
        }

        public InjectViewAttribute(int resourceId)
        {
            ResourceId = resourceId;
        }

        public override bool CanInjectType(Type type)
        {
            return type.IsSubclassOf(typeof(View));
        }
    }
}