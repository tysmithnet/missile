using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public sealed class RegisteredDestination
    {
        private Destination<object> destination;

        internal RegisteredDestination()
        {
        }

        public RegisteredDestination(Destination<object> destination)
        {
            Name = destination.Name;
            DestinationInstance = destination;
            var itr = destination.GetType().BaseType;
            while (itr != null)
            {
                if (itr.GetGenericTypeDefinition() == typeof(Destination<>))
                    break;
                itr = itr.BaseType;
            }
            Debug.Assert(itr != null, nameof(itr) + " != null");
            SourceType = itr.GenericTypeArguments[0];
            ProcessAsyncMethodInfo = itr.GetMethod("ProcessAsync");
        }

        public string Name { get; internal set; }
        public Type SourceType { get; internal set; }
        public Destination<object> DestinationInstance { get; internal set; }
        public MethodInfo ProcessAsyncMethodInfo { get; internal set; }

        public Task Process(object arg)
        {
            return (Task) ProcessAsyncMethodInfo.Invoke(DestinationInstance, new[] {arg});
        }
    }
}