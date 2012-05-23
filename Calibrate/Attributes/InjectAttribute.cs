using System;

namespace Calibrate.Attributes
{
    public class InjectAttribute : BaseInjectAttribute
    {
        public string Name { get; set; }

        public InjectAttribute()
        {
        }

        public InjectAttribute(string name)
        {
            Name = name;
        }

        public override bool CanInjectType(Type type)
        {
            return true;
        }
    }
}