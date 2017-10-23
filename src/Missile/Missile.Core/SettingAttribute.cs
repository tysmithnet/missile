using System;

namespace Missile.Core
{
    /// <summary>
    ///     Indicates a property or field is a setting member
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SettingAttribute : Attribute
    {
        // todo: allow for custom property editory control
    }
}