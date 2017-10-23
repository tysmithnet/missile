using System;

namespace Missile.Core
{
    /// <summary>
    ///     Attribute used to indicate a property is a sub settings collection
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SubSettingsAttribute : Attribute
    {
    }
}