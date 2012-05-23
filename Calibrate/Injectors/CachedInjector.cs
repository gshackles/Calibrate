using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Calibrate.Attributes;

namespace Calibrate.Injectors
{
    public class CachedInjector : BaseInjector
    {
        private static List<Type> _injectionTypes = new List<Type>() { typeof(InjectViewAttribute), typeof(InjectStringAttribute), typeof(InjectExtraAttribute), 
                                                                       typeof(InjectAttribute), typeof(InjectPreferenceAttribute) };
        private Dictionary<Type, Dictionary<Type, IEnumerable<InjectionPoint>>> _injectionPoints;

        public CachedInjector()
        {
            _injectionPoints = new Dictionary<Type, Dictionary<Type, IEnumerable<InjectionPoint>>>();
        }

        protected override void OnInjecting(object context)
        {
            scanTypeAndCacheInjectionPoints(context.GetType());
        }

        protected override IEnumerable<InjectionPoint> GetInjectionPoints(object context, Type attributeType)
        {
            return _injectionPoints[context.GetType()][attributeType];
        }

        private void scanTypeAndCacheInjectionPoints(Type type)
        {
            if (_injectionPoints.ContainsKey(type)) return;

            var objectsToInject =
                type
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                    .Select(info => new InjectionPoint
                    {
                        FieldInfo = info,
                        Attribute = info.GetCustomAttributes(typeof(BaseInjectAttribute), true).FirstOrDefault() as BaseInjectAttribute
                    })
                    .Where(field => field.Attribute != null);

            var injectionPoints = new Dictionary<Type, IEnumerable<InjectionPoint>>();

            foreach (var injectionType in _injectionTypes)
            {
                injectionPoints.Add(injectionType,
                                    objectsToInject
                                        .Where(obj => injectionType.IsAssignableFrom(obj.Attribute.GetType()))
                                        .ToList());
            }

            _injectionPoints.Add(type, injectionPoints);
        }
    }
}