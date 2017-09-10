using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public sealed class RegisteredDestination
    {                                            
        internal RegisteredDestination()
        {
        }

        public RegisteredDestination(IDestination instance, Type interfaceType)
        {
            DestinationInstance = instance;
            PropertyInfo propertyInfo = interfaceType.GetProperty("Name");
            Name = (string) propertyInfo.GetValue(instance);
            SourceType = interfaceType.GenericTypeArguments[0];
            ProcessAsyncMethodInfo = interfaceType.GetMethod("ProcessAsync");
        }

        public string Name { get; internal set; }
        public Type SourceType { get; internal set; }
        public IDestination DestinationInstance { get; internal set; }
        public MethodInfo ProcessAsyncMethodInfo { get; internal set; }

        public Task Process(object arg)
        {
            return (Task) ProcessAsyncMethodInfo.Invoke(DestinationInstance, new[] {arg});
        }
    }
}