using System;
using System.Linq;
using System.Reflection;

namespace Missile.TextLauncher
{
    public class PropertyFieldAdapter
    {
        private readonly FieldInfo _fieldInfo;
        private readonly object _instance;
        private readonly PropertyInfo _propertyInfo;

        public PropertyFieldAdapter(MemberInfo memberInfo, object instance)
        {
            MemberInfo = memberInfo;
            _propertyInfo = memberInfo as PropertyInfo;
            _fieldInfo = memberInfo as FieldInfo;
            _instance = instance;
            if (_instance == null)
                throw new ArgumentNullException(
                    $"{nameof(_instance)} cannot be null because it is required for getting/setting values");
            if (_propertyInfo == null && _fieldInfo == null)
                throw new ArgumentException($"{nameof(memberInfo)} must be PropertyInfo or FieldInfo");
        }

        public MemberInfo MemberInfo { get; }

        public bool IsSubSection =>
            MemberInfo.CustomAttributes.Any(a => a.AttributeType == typeof(SubSettingsAttribute));

        public bool IsSetting => MemberInfo.CustomAttributes.Any(a => a.AttributeType == typeof(SettingAttribute));

        // todo: convert to properties
        public void SetValue(object value)
        {
            if (_propertyInfo != null)
                _propertyInfo.SetValue(_instance, value);
            else
                _fieldInfo.SetValue(_instance, value);
        }

        public object GetValue()
        {
            return _propertyInfo != null
                ? _propertyInfo.GetMethod.Invoke(_instance, new object[0])
                : _fieldInfo.GetValue(_instance);
        }

        public Type GetMemberType()
        {
            return _propertyInfo != null ? _propertyInfo.PropertyType : _fieldInfo.FieldType;
        }
    }
}