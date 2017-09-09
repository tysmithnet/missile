using System;
using System.Diagnostics;
using System.Reflection;

namespace Missile.TextLauncher
{
    public sealed class RegisteredProvider
    {
        public RegisteredProvider(Provider<object> provider)
        {
            Name = provider.Name;
            ProviderInstance = provider;
            var itr = provider.GetType().BaseType;
            while (itr != null)
            {
                if (itr.GetGenericTypeDefinition() == typeof(Provider<>))
                    break;
                itr = itr.BaseType;
            }

            Debug.Assert(itr != null, nameof(itr) + " != null");
            DestinationType = itr.GenericTypeArguments[0];
            ProvideMethodInfo = itr.GetMethod("Provide");
        }

        internal RegisteredProvider()
        {
        }

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