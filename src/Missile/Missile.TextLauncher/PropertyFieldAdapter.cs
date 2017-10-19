using System;
using System.Reflection;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     A common abstraction over properties and fields
    /// </summary>
    public class PropertyFieldAdapter
    {
        /// <summary>
        ///     The field information if the underlying type is a field
        /// </summary>
        protected internal readonly FieldInfo FieldInfo;

        /// <summary>
        ///     The instance
        /// </summary>
        protected internal readonly object Instance;

        /// <summary>
        ///     The property information if the underlying member is property
        /// </summary>
        protected internal readonly PropertyInfo PropertyInfo;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyFieldAdapter" /> class.
        /// </summary>
        /// <param name="memberInfo">The member information, either field or property</param>
        /// <param name="instance">The instance for which the property or field is attached</param>
        /// <exception cref="ArgumentNullException">Instance is null</exception>
        /// <exception cref="ArgumentException">The specified member is neither a property nor a field</exception>
        public PropertyFieldAdapter(MemberInfo memberInfo, object instance)
        {
            MemberInfo = memberInfo;
            PropertyInfo = memberInfo as PropertyInfo;
            FieldInfo = memberInfo as FieldInfo;
            Instance = instance;
            if (Instance == null)
                throw new ArgumentNullException(
                    $"{nameof(Instance)} cannot be null because it is required for getting/setting values");
            if (PropertyInfo == null && FieldInfo == null)
                throw new ArgumentException($"{nameof(memberInfo)} must be PropertyInfo or FieldInfo");
        }

        /// <summary>
        ///     Gets the member information
        /// </summary>
        /// <value>
        ///     The member information
        /// </value>
        public MemberInfo MemberInfo { get; }

        // todo: convert to properties
        public void SetValue(object value)
        {
            if (PropertyInfo != null)
                PropertyInfo.SetValue(Instance, value);
            else
                FieldInfo.SetValue(Instance, value);
        }

        /// <summary>
        ///     Gets the value of this property or field
        /// </summary>
        /// <returns>The value of this property or field</returns>
        public object GetValue()
        {
            return PropertyInfo != null
                ? PropertyInfo.GetMethod.Invoke(Instance, new object[0])
                : FieldInfo.GetValue(Instance);
        }

        /// <summary>
        ///     Gets the type of the member
        /// </summary>
        /// <returns>The type of the member</returns>
        public Type GetMemberType()
        {
            return PropertyInfo != null ? PropertyInfo.PropertyType : FieldInfo.FieldType;
        }
    }
}