using System;
using System.Reflection;

namespace Missile.TextLauncher.Interpretation
{
    public sealed class RegisteredProvider
    {
        public string Name { get; internal set; }
        public Type DestinationType { get; internal set; }
        public object ProviderInstance { get; internal set; }
        public MethodInfo ProvideMethodInfo { get; internal set; }

        public object Provide()
        {
            return ProvideMethodInfo.Invoke(ProviderInstance, new object[0]);
        }
    }
}