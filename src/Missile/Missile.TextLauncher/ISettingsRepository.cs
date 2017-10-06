using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Missile.TextLauncher
{
    public interface ISettingsRepository
    {
        T Get<T>() where T : ISettings;
        IEnumerable<ISettings> GetAll();
        void Save<T>() where T : ISettings;
    }

    // todo: don't throw if bad property, gracefully degrade
    // todo: need custom property editors
    // todo: composite pattern groups
    // todo: allow setting to choose its property editor
    [Export(typeof(ISettingsRepository))]
    public class SettingsRepository : ISettingsRepository
    {
        private bool _isLoaded;

        [ImportMany]
        protected internal ISettings[] AllSettings { get; set; }

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
                throw new ArgumentException($"{typeof(T).FullName} is not serializable and therefore cannot be saved");
            try
            {
                using (var stream = new FileStream(fileName, FileMode.Open))
                {
                    var serializer = new XmlSerializer(first.GetType());
                    serializer.Serialize(stream, first);
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

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

        public IEnumerable<ISettings> GetAll()
        {
            if (!_isLoaded)
            {
                LoadFromFiles();
                _isLoaded = true;
            }
            return AllSettings;
        }

        private void LoadFromFiles()
        {
            foreach (var settings in AllSettings)
            {
                var fileName = settings.GetType().FullName + ".config";
                if (settings.GetType().IsSerializable)
                    try
                    {
                        using (var stream = new FileStream(fileName, FileMode.Open))
                        {
                            var serializer = new XmlSerializer(settings.GetType());
                            var deserializedSettings = serializer.Deserialize(stream);
                            CopySettings(deserializedSettings, settings);
                        }
                    }
                    catch (FileNotFoundException)
                    {
                    }
            }
        }

        private void CopySettings(object sourceSettings, ISettings destSettings)
        {
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