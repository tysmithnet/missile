using System;

namespace Missile.TextLauncher
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public class SettingAttribute : Attribute
    {
        protected SettingAttribute()
        {
        }
    }
}