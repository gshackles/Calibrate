using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Preferences;
using Calibrate.Attributes;
using Calibrate.Gears;
using TinyIoC;

namespace Calibrate.Injectors
{
    public abstract class BaseInjector : IInjector
    {
        public void InjectFromContainer(object context, TinyIoCContainer container)
        {
            OnInjecting(context);

            injectByType(context, typeof(InjectAttribute),
                (attribute, type) => container.Resolve(type, ((InjectAttribute)attribute).Name));

            OnInjected();
        }

        public void InjectStrings(ContextWrapper context)
        {
            OnInjecting(context);

            injectByType(context, typeof(InjectStringAttribute),
                (attribute, type) => context.Resources.GetString(((InjectStringAttribute)attribute).ResourceId));

            OnInjected();
        }

        public void InjectViews(Activity activity)
        {
            OnInjecting(activity);

            injectByType(activity, typeof(InjectViewAttribute),
                (attribute, type) => activity.FindViewById(((InjectViewAttribute)attribute).ResourceId));

            OnInjected();
        }

        public void InjectExtras(Activity activity)
        {
            OnInjecting(activity);

            injectByType(activity, typeof(InjectExtraAttribute),
                (attribute, type) => activity.Intent.Extras.Get(((InjectExtraAttribute)attribute).Key));

            OnInjected();
        }

        public void InjectPreferences(Context context)
        {
            OnInjecting(context);

            injectByType(context, typeof(InjectPreferenceAttribute),
                (attribute, type) =>
                {
                    var preferenceAttribute = (InjectPreferenceAttribute)attribute;
                    ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);

                    if (type == typeof(int))
                        return preferences.GetInt(preferenceAttribute.Key, (int)preferenceAttribute.DefaultValue);
                    else if (type == typeof(long))
                        return preferences.GetLong(preferenceAttribute.Key, (long)preferenceAttribute.DefaultValue);
                    else if (type == typeof(float))
                        return preferences.GetFloat(preferenceAttribute.Key, (float)preferenceAttribute.DefaultValue);
                    else if (type == typeof(string))
                        return preferences.GetString(preferenceAttribute.Key, (string)preferenceAttribute.DefaultValue);
                    else if (type == typeof(bool))
                        return preferences.GetBoolean(preferenceAttribute.Key, (bool)preferenceAttribute.DefaultValue);
                    else
                        return null;
                });

            OnInjected();
        }

        protected abstract IEnumerable<InjectionPoint> GetInjectionPoints(object context, Type attributeType);
        protected virtual void OnInjecting(object context) { }
        protected virtual void OnInjected() { }

        private void injectByType(object context, Type attributeType, Func<BaseInjectAttribute, Type, object> getValue)
        {
            foreach (var point in GetInjectionPoints(context, attributeType))
            {
                if (!point.Attribute.CanInjectType(point.FieldInfo.FieldType))
                {
                    throw new InvalidOperationException(string.Format("Cannot use the {0} attribute on type {1}", point.Attribute.GetType(), point.FieldInfo.FieldType));
                }

                point.FieldInfo.SetValue(context, getValue(point.Attribute, point.FieldInfo.FieldType));
            }
        }
    }
}