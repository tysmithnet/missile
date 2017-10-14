using System;
using System.Collections.Generic;
using System.Reflection;

namespace Missile.TextLauncher.Provision
{
    /// <summary>
    ///     Represents a provider that has been registered for use
    /// </summary>
    public class RegisteredProvider
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisteredProvider" /> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="iProviderInterface">The i provider interface.</param>
        public RegisteredProvider(IProvider instance, Type iProviderInterface)
        {
            ProviderInstance = instance;
            var namePropertyInfo = iProviderInterface.GetProperty("Name");
            Name = (string) namePropertyInfo.GetValue(instance);
            ProvideMethodInfo = iProviderInterface.GetMethod("Provide");
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisteredProvider" /> class.
        /// </summary>
        protected internal RegisteredProvider()
        {
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; internal set; }

        /// <summary>
        ///     Gets the provider instance.
        /// </summary>
        /// <value>
        ///     The provider instance.
        /// </value>
        public IProvider ProviderInstance { get; internal set; }

        /// <summary>
        ///     Gets the provide method information.
        /// </summary>
        /// <value>
        ///     The provide method information.
        /// </value>
        public MethodInfo ProvideMethodInfo { get; internal set; }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is RegisteredProvider provider &&
                   Name == provider.Name &&
                   EqualityComparer<IProvider>.Default.Equals(ProviderInstance, provider.ProviderInstance) &&
                   EqualityComparer<MethodInfo>.Default.Equals(ProvideMethodInfo, provider.ProvideMethodInfo);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 493648596;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<IProvider>.Default.GetHashCode(ProviderInstance);
            hashCode = hashCode * -1521134295 + EqualityComparer<MethodInfo>.Default.GetHashCode(ProvideMethodInfo);
            return hashCode;
        }

        /// <summary>
        ///     Provides the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public virtual object Provide(string[] args)
        {
            return ProvideMethodInfo.Invoke(ProviderInstance, new object[] {args});
        }
    }
}