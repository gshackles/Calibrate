using System;

namespace Calibrate.Attributes
{
    public class InjectExtraAttribute : BaseInjectAttribute
    {
        public string Key { get; set; }

        public InjectExtraAttribute()
        {
        }

        public InjectExtraAttribute(string key)
        {
            Key = key;
        }

        public override bool CanInjectType(Type type)
        {
            return true;
        }
    }
}