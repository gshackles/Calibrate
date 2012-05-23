using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;

namespace Calibrate.Attributes
{
    public class InjectPreferenceAttribute : BaseInjectAttribute
    {
        private static IEnumerable<Type> _validPreferenceTypes = new List<Type>() { typeof(int), typeof(long), typeof(float), typeof(string), typeof(bool) };

        public string Key { get; set; }
        public object DefaultValue { get; set; }

        public InjectPreferenceAttribute()
        {
        }

        public InjectPreferenceAttribute(string key, object defaultValue) 
        {
            Key = key;
            DefaultValue = defaultValue;
        }

        public override bool CanInjectType(Type type)
        {
            return _validPreferenceTypes.Contains(type);
        }
    }
}