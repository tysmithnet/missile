using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class RegisteredDestination
    {
        public string Name { get; internal set; }
        public Type SourceType { get; internal set; }
        public object DestinationInstance { get; internal set; }
        public MethodInfo ProcessAsyncMethodInfo { get; internal set; }

        public Task Process(object arg)
        {
            return (Task) ProcessAsyncMethodInfo.Invoke(DestinationInstance, new[] {arg});
        }
    }
}