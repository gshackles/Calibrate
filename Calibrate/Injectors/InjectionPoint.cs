using System.Reflection;
using Calibrate.Attributes;

namespace Calibrate.Injectors
{
    public class InjectionPoint
    {
        public FieldInfo FieldInfo { get; set; }
        public BaseInjectAttribute Attribute { get; set; }
    }
}