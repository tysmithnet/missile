using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Missile.Core.FileSystem;

namespace Missile.TextLauncher
{
    // todo: don't throw if bad property, gracefully degrade
    // todo: need custom property editors
    // todo: composite pattern groups
    // todo: allow setting to choose its property editor
    /// <inheritdoc />
    /// <summary>
    ///     Default implementation of ISettingsRepository
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ISettingsRepository" />
    [Export(typeof(ISettingsRepository))]
    public class SettingsRepository : ISettingsRepository
    {
        /// <summary>
        ///     Loaded flag
        /// </summary>
        private bool _isLoaded;

        /// <summary>
        ///     Gets or sets all settings
        /// </summary>
        /// <value>
        ///     All settings
        /// </value>
        [ImportMany]
        protected internal ISettings[] AllSettings { get; set; }

        /// <summary>
        ///     Gets or sets the file system.
        /// </summary>
        /// <value>
        ///     The file system.
        /// </value>
        [Import]
        protected internal IFileSystem FileSystem { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Saves the settings for the specified type parameter
        /// </summary>
        /// <typeparam name="T">Type of settings to save</typeparam>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The requested settings could not be found</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     The requested settings do not implement
        ///     ISerializable or could not be serialized
        /// </exception>
        public void Save<T>() where T : ISettings
        {
            if (!_isLoaded)
            {
                LoadFromFiles();
                _isLoaded = true;
            }
            var first = AllSettings.OfType<T>().FirstOrDefault();
            if (first == null)
                throw new ArgumentOutOfRangeException($"Cannot find requested setting: {typeof(T)}");
            var fileName = first.GetType().FullName + ".config";
            if (!first.GetType().IsSerializable)
                throw new SerializationException(
                    $"{typeof(T).FullName} is not serializable and therefore cannot be saved");
            try
            {
                using (var stream = FileSystem.OpenFile(fileName, FileMode.Truncate, FileAccess.Write, FileShare.None))
                {
                    var serializer = new XmlSerializer(first.GetType());
                    serializer.Serialize(stream, first);
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets settings matching the specified type parameter
        /// </summary>
        /// <typeparam name="T">The type of settings to get</typeparam>
        /// <returns>
        ///     ISettings instance matching the requested type
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The requested settings could not be found</exception>
        public T Get<T>() where T : ISettings
        {
            if (!_isLoaded)
            {
                LoadFromFiles();
                _isLoaded = true;
            }

            var first = AllSettings.OfType<T>().FirstOrDefault();
            if (first == null)
                throw new ArgumentOutOfRangeException($"Cannot find requested setting: {typeof(T)}");
            return first;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets all settings
        /// </summary>
        /// <returns>
        ///     All settings
        /// </returns>
        public IEnumerable<ISettings> GetAll()
        {
            if (_isLoaded) return AllSettings;
            LoadFromFiles();
            _isLoaded = true;
            return AllSettings;
        }

        /// <summary>
        ///     Loads settings from disk
        /// </summary>
        protected internal void LoadFromFiles()
        {
            foreach (var settings in AllSettings)
            {
                var fileName = settings.GetType().FullName + ".config";
                if (!settings.GetType().IsSerializable) continue;
                try
                {
                    using (var stream = new FileStream(fileName, FileMode.Open))
                    {
                        var serializer = new XmlSerializer(settings.GetType());
                        var deserializedSettings = (ISettings) serializer.Deserialize(stream);
                        CopySettings(deserializedSettings, settings);
                    }
                }
                catch (FileNotFoundException)
                {
                }
            }
        }

        /// <summary>
        ///     Copies the settings from one settings object to another
        /// </summary>
        /// <param name="sourceSettings">The source settings</param>
        /// <param name="destSettings">The dest settings</param>
        protected internal static void CopySettings(ISettings sourceSettings, ISettings destSettings)
        {
            if (sourceSettings.GetType() != destSettings.GetType())
                throw new ArgumentException(
                    "Cannot copy settings because the source and destination are of different types");
            var sourceAdapters = sourceSettings.GetType().GetMembers().Where(m => m is PropertyInfo || m is FieldInfo)
                .Select(x => new PropertyFieldAdapter(x, sourceSettings));
            var destAdapters = destSettings.GetType().GetMembers().Where(m => m is PropertyInfo || m is FieldInfo)
                .Select(x => new PropertyFieldAdapter(x, destSettings));

            var zip = from lhs in sourceAdapters
                join rhs in destAdapters
                    on lhs.MemberInfo equals rhs.MemberInfo
                select new
                {
                    Source = lhs,
                    Dest = rhs
                };
            foreach (var pair in zip)
                pair.Dest.SetValue(pair.Source.GetValue());
        }
    }
}