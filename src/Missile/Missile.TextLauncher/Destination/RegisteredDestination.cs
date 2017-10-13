using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
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
        /// <param name="interfaceType">Type of observalbe that this destination expects e.g. IDestination<string></param>
        public RegisteredDestination(IDestination instance, Type interfaceType)
        {
            DestinationInstance = instance;
            var propertyInfo = interfaceType.GetProperty("Name");
            Name = (string) propertyInfo.GetValue(instance);
            SourceType = interfaceType.GenericTypeArguments[0];
            ProcessAsyncMethodInfo = interfaceType.GetMethod("ProcessAsync");
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
        /// <param name="cancellationToken"></param>
        /// <returns>Task representing when the destination has finished processing</returns>
        public async Task ProcessAsync(object arg, CancellationToken cancellationToken)
        {
            await (Task) ProcessAsyncMethodInfo.Invoke(DestinationInstance, new[] {arg, cancellationToken});
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is RegisteredDestination destination &&
                   Name == destination.Name &&
                   EqualityComparer<Type>.Default.Equals(SourceType, destination.SourceType) &&
                   EqualityComparer<IDestination>.Default.Equals(DestinationInstance,
                       destination.DestinationInstance) &&
                   EqualityComparer<MethodInfo>.Default.Equals(ProcessAsyncMethodInfo,
                       destination.ProcessAsyncMethodInfo);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 16259721;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(SourceType);
            hashCode = hashCode * -1521134295 + EqualityComparer<IDestination>.Default.GetHashCode(DestinationInstance);
            hashCode = hashCode * -1521134295 +
                       EqualityComparer<MethodInfo>.Default.GetHashCode(ProcessAsyncMethodInfo);
            return hashCode;
        }
    }
}