using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Calibrate.Attributes;

namespace Calibrate.Injectors
{
    public class SimpleInjector : BaseInjector
    {
        protected override IEnumerable<InjectionPoint> GetInjectionPoints(object context, Type attributeType)
        {
            return
                context
                    .GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                    .Select(info => new InjectionPoint
                    {
                        FieldInfo = info,
                        Attribute = info.GetCustomAttributes(attributeType, true).FirstOrDefault() as BaseInjectAttribute
                    })
                    .Where(point => point.Attribute != null && attributeType.IsAssignableFrom(point.Attribute.GetType()));
        }
    }
}