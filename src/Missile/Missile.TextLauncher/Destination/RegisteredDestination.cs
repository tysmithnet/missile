using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Destination
{
    /// <summary>
    ///     Represents a destination that has been registered with the
    /// </summary>
    public sealed class RegisteredDestination
    {
        /// <summary>
        ///     Initializes a new instance of RegistereDestination
        /// </summary>
        internal RegisteredDestination()
        {
        }

        /// <summary>
        ///     Initializes a new instance of RegisteredDestination using a provided instance and source type
        /// </summary>
        /// <param name="instance">Destination instance to register</param>
        /// <param name="sourcetype">Type of observalbe that this destination expects e.g. IObservable<string> -> string</param>
        public RegisteredDestination(IDestination instance, Type sourcetype)
        {
            DestinationInstance = instance;
            var propertyInfo = sourcetype.GetProperty("Name");
            Name = (string) propertyInfo.GetValue(instance);
            SourceType = sourcetype.GenericTypeArguments[0];
            ProcessAsyncMethodInfo = sourcetype.GetMethod("ProcessAsync");
        }

        /// <summary>
        ///     Name of this registered destination
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     Generic type of observalble that was fed to this destination
        /// </summary>
        public Type SourceType { get; internal set; }

        /// <summary>
        ///     Instance of the destination to use
        /// </summary>
        public IDestination DestinationInstance { get; internal set; }

        /// <summary>
        ///     Convenience MethodInfo for the process method of the destination
        /// </summary>
        public MethodInfo ProcessAsyncMethodInfo { get; internal set; }

        /// <summary>
        ///     Convenience method for this destination
        /// </summary>
        /// <param name="arg">What to pass to the destination</param>
        /// <returns>Task representing when the destination has finished processing</returns>
        public Task Process(object arg)
        {
            return (Task) ProcessAsyncMethodInfo.Invoke(DestinationInstance, new[] {arg});
        }
    }
}