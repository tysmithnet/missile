using System.Collections.Generic;

namespace Missile.Core
{
    /// <summary>
    ///     Represents an object capable of managing ISettings instances
    /// </summary>
    public interface ISettingsRepository
    {
        /// <summary>
        ///     Gets settings matching the specified type parameter
        /// </summary>
        /// <typeparam name="T">The type of settings to get</typeparam>
        /// <returns>ISettings instance matching the requested type</returns>
        T Get<T>() where T : ISettings;

        /// <summary>
        ///     Gets all settings
        /// </summary>
        /// <returns>All settings</returns>
        IEnumerable<ISettings> GetAll();

        /// <summary>
        ///     Saves the settings for the specified type parameter
        /// </summary>
        /// <typeparam name="T">Type of settings to save</typeparam>
        void Save<T>() where T : ISettings;

        /// <summary>
        ///     Saves all settings
        /// </summary>
        void SaveAll();
    }
}