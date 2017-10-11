namespace Missile.TextLauncher.Provision
{
    /// <summary>
    ///     An object capable of managing the lifecycle of Providers
    /// </summary>
    public interface IProviderRepository
    {
        /// <summary>
        ///     Gets a RegisteredProvider by name
        /// </summary>
        /// <param name="name">The name of the registered provider</param>
        /// <returns>The RegisteredProvider with the specified name</returns>
        RegisteredProvider Get(string name);

        /// <summary>
        ///     Adds a RegisteredProvider to this repository
        /// </summary>
        /// <param name="provider">The RegisteredProvider to add</param>
        void Add(RegisteredProvider provider);
    }
}