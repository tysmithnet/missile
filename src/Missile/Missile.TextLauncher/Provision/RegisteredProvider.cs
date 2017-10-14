using System;
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