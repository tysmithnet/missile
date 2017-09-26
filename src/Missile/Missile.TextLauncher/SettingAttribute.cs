using System;

namespace Missile.TextLauncher
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]  
    public class SettingAttribute : Attribute
    {   
        // todo: allow for custom property editory control
        public SettingAttribute()
        {
        
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SubSettingsAttribute : Attribute
    {

    }
}